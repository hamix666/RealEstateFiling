using Common;
using Guna.UI2.WinForms;
using RealEstateFilingWin.Forms;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace RealEstateFilingWin.Utility
{
    public static class EfFormBinder
    {
        private static readonly ConcurrentDictionary<Type, List<(PropertyInfo prop, string ctrlName)>> _mapCache =
            new ConcurrentDictionary<Type, List<(PropertyInfo prop, string ctrlName)>>();

        private static readonly ConcurrentDictionary<string, List<Control>> _controlCache =
            new ConcurrentDictionary<string, List<Control>>();

        #region --- Load / Save ---

        public static T Load<T>(DbContext db, object key, Control parent) where T : class
        {
            var model = db.Set<T>().Find(key);
            if (model != null)
                ModelToControls(model, parent);
            return model;
        }
        public static T LoadWithDetails<T>(DbContext db, object key, Control parent, params string[] includes)
    where T : class
        {
            IQueryable<T> query = db.Set<T>();

            // شامل کردن Navigation Propertyها
            foreach (var include in includes)
                query = query.Include(include);

            // پیدا کردن کلید اصلی
            var keyProp = typeof(T).GetProperties()
                .FirstOrDefault(p => p.Name.Equals("ID", StringComparison.OrdinalIgnoreCase));

            if (keyProp == null)
                throw new Exception("مدل فاقد فیلد کلید (ID) است.");

            // ✅ ساخت Expression برای مقایسه کلید
            var parameter = System.Linq.Expressions.Expression.Parameter(typeof(T), "x");
            var property = System.Linq.Expressions.Expression.Property(parameter, keyProp.Name);
            var constant = System.Linq.Expressions.Expression.Constant(key);
            var equal = System.Linq.Expressions.Expression.Equal(property, constant);
            var lambda = System.Linq.Expressions.Expression.Lambda<Func<T, bool>>(equal, parameter);

            // اجرای کوئری
            var model = query.AsNoTracking().FirstOrDefault(lambda);

            if (model != null)
                ModelToControls(model, parent);

            return model;
        }



        public static void Save<T>(DbContext db, Control parent, object keyValue = null) where T : class, new()
        {
            var set = db.Set<T>();
            T model = keyValue != null ? set.Find(keyValue) ?? new T() : new T();
            ControlsToModel(model, parent);

            var entry = db.Entry(model);
            if (entry.State == EntityState.Detached)
            {
                var keyProp = typeof(T).GetProperties()
                    .FirstOrDefault(p => p.Name.Equals("Id", StringComparison.OrdinalIgnoreCase)
                                      || p.Name.EndsWith("Id", StringComparison.OrdinalIgnoreCase));

                bool isNew = true;

                if (keyProp != null)
                {
                    var keyVal = keyProp.GetValue(model);
                    if (keyVal != null && !keyVal.Equals(Activator.CreateInstance(keyProp.PropertyType)))
                    {
                        var existing = set.Find(keyVal);
                        if (existing != null)
                        {
                            db.Entry(existing).CurrentValues.SetValues(model);
                            isNew = false;
                        }
                    }
                }

                if (isNew)
                    set.Add(model);
            }

            db.SaveChanges();
        }

        public static T SaveFull<T>(DbContext db, Control parent, object keyValue = null)
    where T : Models.ModelBaseFull, new()
        {
            var set = db.Set<T>();
            T model = keyValue != null ? set.Find(keyValue) ?? new T() : new T();
            ControlsToModel(model, parent);

            var entry = db.Entry(model);
            if (entry.State == EntityState.Detached)
            {
                var keyProp = typeof(T).GetProperties()
                    .FirstOrDefault(p => p.Name.Equals("ID", StringComparison.OrdinalIgnoreCase));

                bool isNew = true;

                if (keyProp != null)
                {
                    var keyVal = keyProp.GetValue(model);
                    if (keyVal != null && !keyVal.Equals(Activator.CreateInstance(keyProp.PropertyType)))
                    {
                        var existing = set.Find(keyVal);
                        if (existing != null)
                        {
                            model.CreationDate = existing.CreationDate;
                            model.ModificationDate = DateTime.Now;

                            // پراپرتی‌های ساده
                            db.Entry(existing).CurrentValues.SetValues(model);

                            // ⚡ حالا مجموعه‌ها را نیز همگام‌سازی می‌کنیم
                            SyncChildCollections(db, existing, model);

                            model = existing; // مرجع نهایی
                            isNew = false;
                        }
                    }
                }

                if (isNew)
                {
                    model.CreationDate = DateTime.Now;
                    set.Add(model);

                    // اگر مدل جدید است، کل ICollection را هم اضافه می‌کنیم
                    SyncChildCollections(db, null, model);
                }
            }

            db.SaveChanges();
            return model;
        }
        public static T SaveFull<T>(DbContext db, T model) where T : Models.ModelBaseFull, new()
        {
            var set = db.Set<T>();

            var keyProp = typeof(T).GetProperties()
                .FirstOrDefault(p => p.Name.Equals("ID", StringComparison.OrdinalIgnoreCase));

            bool isNew = true;

            if (keyProp != null)
            {
                var keyVal = keyProp.GetValue(model);
                if (keyVal != null && !keyVal.Equals(Activator.CreateInstance(keyProp.PropertyType)))
                {
                    var existing = set.Find(keyVal);
                    if (existing != null)
                    {
                                               
                        model.UserIDModifier = ClsBaseSetting.UserID;
                        model.UserIDCreator = existing.UserIDCreator;
                        model.CreationDate = existing.CreationDate;
                        model.ModificationDate = DateTime.Now;
                        db.Entry(existing).CurrentValues.SetValues(model);
                        SyncChildCollections(db, existing, model);
                        model = existing;
                        isNew = false;
                    }
                }
            }

            if (isNew)
            {
                model.UserIDCreator = ClsBaseSetting.UserID;
                model.CreationDate = DateTime.Now;
                set.Add(model);
                SyncChildCollections(db, null, model);
            }

            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {

                FrmMsgBox.Show(ex.Message);
            }
            
            return model;
        }

        private static void SyncChildCollections(DbContext db, object existingEntity, object newEntity)
        {
            var entityType = newEntity.GetType();
            var collectionProps = entityType.GetProperties()
                .Where(p => typeof(System.Collections.IEnumerable).IsAssignableFrom(p.PropertyType)
                         && p.PropertyType != typeof(string)
                         && p.PropertyType.IsGenericType
                         && p.PropertyType.GetGenericTypeDefinition() == typeof(ICollection<>));

            foreach (var prop in collectionProps)
            {
                var childType = prop.PropertyType.GetGenericArguments()[0];
                var newItems = (IEnumerable<object>)prop.GetValue(newEntity) ?? new List<object>();
                var childSet = db.Set(childType);

                if (existingEntity == null)
                {
                    // اگر entity تازه است → فقط Add می‌کنیم
                    foreach (var item in newItems)
                        childSet.Add(item);
                }
                else
                {
                    var existingItems = (IEnumerable<object>)prop.GetValue(existingEntity) ?? new List<object>();
                    var existingList = existingItems.Cast<object>().ToList();

                    var keyProp = childType.GetProperties()
                        .FirstOrDefault(p => p.Name.Equals("ID", StringComparison.OrdinalIgnoreCase));

                    // حذف آیتم‌هایی که حذف شده‌اند
                    foreach (var oldItem in existingList)
                    {
                        var oldKey = keyProp.GetValue(oldItem);
                        if (!newItems.Any(n => Equals(keyProp.GetValue(n), oldKey)))
                            childSet.Remove(oldItem);
                    }

                    // افزودن یا به‌روزرسانی آیتم‌ها
                    foreach (var newItem in newItems)
                    {
                        var keyVal = keyProp.GetValue(newItem);
                        if (keyVal == null || Convert.ToInt64(keyVal) == 0)
                        {
                            childSet.Add(newItem);
                        }
                        else
                        {
                            var existingItem = existingList
                                .FirstOrDefault(x => Equals(keyProp.GetValue(x), keyVal));
                            if (existingItem != null)
                            {
                                db.Entry(existingItem).CurrentValues.SetValues(newItem);
                            }
                            else
                            {
                                childSet.Add(newItem);
                            }
                        }
                    }
                }
            }
        }

        #endregion

        #region --- Model <-> Controls ---

        public static void ModelToControls<T>(T model, Control parent)
        {
            if (model == null) return;

            var map = GetMap<T>(parent);
            var controls = SafeControlDictionary(GetAllControls(parent));

            foreach (var (prop, ctrlName) in map)
            {
                if (!controls.TryGetValue(ctrlName, out var ctrl)) continue;
                var value = prop.GetValue(model);

                if (ctrl is Guna2TextBox txt)
                    txt.Text = value?.ToString() ?? "";
                if (ctrl is MaskedTextBox mtxt)
                    mtxt.Text = value.ToShamsiDate()?.ToString() ?? null;
                if (ctrl is Label lbl)
                    lbl.Text = value?.ToString() ?? "";
                else if (ctrl is Guna2ComboBox cmb)
                {
                    if (value != null)
                    {
                        try { cmb.SelectedValue = value; }
                        catch { cmb.Text = value.ToString(); }
                    }
                }
                else if (ctrl is Guna2CheckBox chk)
                    chk.Checked = value is bool b && b;

                else if (ctrl is DateTimePicker dtp && value is DateTime dt)
                    dtp.Value = dt;

                else if (ctrl is Guna2RadioButton rdo)
                    rdo.Checked = value is bool b2 && b2;
            }
        }

        public static void ControlsToModel<T>(T model, Control parent)
        {
            if (model == null) return;

            var map = GetMap<T>(parent);
            var controls = SafeControlDictionary(GetAllControls(parent));

            foreach (var (prop, ctrlName) in map)
            {
                if (!controls.TryGetValue(ctrlName, out var ctrl)) continue;
                object value = null;

                if (ctrl is Guna2TextBox txt)
                    value = ConvertValue(txt.Text, prop.PropertyType);
                else if (ctrl is MaskedTextBox mtxt && mtxt.Text!= "    /  /")
                    value = ConvertValue(mtxt.Text, prop.PropertyType);

                else if (ctrl is Guna2ComboBox cmb)
                    value = ConvertValue(cmb.SelectedValue ?? cmb.Text, prop.PropertyType);

                else if (ctrl is Guna2CheckBox chk)
                    value = chk.Checked;

                else if (ctrl is DateTimePicker dtp)
                    value = dtp.Value;

                else if (ctrl is Guna2RadioButton rdo)
                    value = rdo.Checked;

                if (value != null)
                    prop.SetValue(model, value);
            }
        }

        #endregion

        #region --- Mapping Cache ---

        private static List<(PropertyInfo prop, string ctrlName)> GetMap<T>(Control parent)
        {
            var type = typeof(T);
            if (_mapCache.TryGetValue(type, out var cached))
                return cached;

            var list = new List<(PropertyInfo prop, string ctrlName)>();
            foreach (var prop in type.GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                list.Add((prop, "txt" + prop.Name));
                list.Add((prop, "cmb" + prop.Name));
                list.Add((prop, "chk" + prop.Name));
                list.Add((prop, "rdo" + prop.Name));
                list.Add((prop, "dtp" + prop.Name));
            }

            _mapCache[type] = list;
            return list;
        }

        private static IEnumerable<Control> GetAllControls(Control parent)
        {
            // کلید یکتا بر اساس Instance فرم ساخته می‌شود (نه فقط نام آن)
            string key = parent.GetType().FullName + "_" + parent.GetHashCode();

            if (_controlCache.TryGetValue(key, out var cached))
                return cached;

            var list = new List<Control>();
            void Collect(Control c)
            {
                foreach (Control child in c.Controls)
                {
                    list.Add(child);
                    Collect(child);
                }
            }
            Collect(parent);

            _controlCache[key] = list;
            return list;
        }

        private static Dictionary<string, Control> SafeControlDictionary(IEnumerable<Control> controls)
        {
            // فقط کنترل‌هایی که Name دارن رو اضافه می‌کنیم
            return controls
                .Where(c => !string.IsNullOrWhiteSpace(c.Name))
                .GroupBy(c => c.Name)
                .ToDictionary(g => g.Key, g => g.First());
        }

        #endregion

        #region --- Convert Helper ---

        private static object ConvertValue(object value, Type targetType)
        {
            if (value == null) return null;

            var str = value.ToString().Trim();
            if (string.IsNullOrEmpty(str)) return null;

            var underlyingType = Nullable.GetUnderlyingType(targetType) ?? targetType;

            try
            {
                if (underlyingType.IsEnum)
                    return Enum.Parse(underlyingType, str, true);

                if (underlyingType == typeof(bool))
                {
                    if (str == "1" || str.Equals("true", StringComparison.OrdinalIgnoreCase) || str == "on") return true;
                    if (str == "0" || str.Equals("false", StringComparison.OrdinalIgnoreCase) || str == "off") return false;
                    return false;
                }

                if (underlyingType == typeof(DateTime))
                    return str.ToMiladiDate();
                    //return DateTime.TryParse(str, out var dt) ? dt : null;

                return Convert.ChangeType(str, underlyingType);
            }
            catch
            {
                return null;
            }
        }

        #endregion

        #region --- Preload Cache ---

        public static void PreloadMap<T>(Control parent)
        {
            _ = GetMap<T>(parent);
            _ = GetAllControls(parent);
        }

        #endregion


        public static void ClearCacheForForm(Control parent)
        {
            string key = parent.GetType().FullName + "_" + parent.GetHashCode();
            _controlCache.TryRemove(key, out _);
        }

    }
}
