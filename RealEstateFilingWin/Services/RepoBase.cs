using Common;
using DevComponents.Editors;
using RealEstateFiling.Services;
using RealEstateFilingWin.DTO;
using RealEstateFilingWin.Utility;
using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace RealEstateFilingWin.Services
{
    
    public abstract class RepoBase<T> where T : Models.ModelBase
    {
        protected RealEstateContext _Contexct { get; set; }

        public RepoBase(RealEstateContext repositoryContext)
        {
            _Contexct = repositoryContext;
        }

        public T FindByID(long ID)
        {

            return _Contexct.Set<T>().Where(x => x.ID == ID).FirstOrDefault();
        }

        public IQueryable<T> FindAll() => _Contexct.Set<T>().AsNoTracking();

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression) =>
            _Contexct.Set<T>().Where(expression).AsNoTracking();

        public OperationResult Create(T entity)
        {
            try
            {
                
                _Contexct.Set<T>().Add(entity);
                _Contexct.SaveChanges();
                return OperationResult.Success();
            }
            catch (Exception ex)
            {
                return OperationResult.Error(ex.Message);
            }
        }

        public OperationResult Update(T entity)
        {
            try
            {
                

                var trackedEntity = _Contexct.Set<T>().Local.FirstOrDefault(e => e.ID == entity.ID);
                if (trackedEntity != null)
                {
                    _Contexct.Entry(trackedEntity).CurrentValues.SetValues(entity);
                }
                else
                {
                    var dbEntity = _Contexct.Set<T>().FirstOrDefault(w => w.ID == entity.ID);
                    if (dbEntity != null)
                    {
                        _Contexct.Entry(dbEntity).CurrentValues.SetValues(entity);
                    }
                    else
                    {
                        _Contexct.Set<T>().Add(entity); // اگر جدید بود
                    }
                }

                _Contexct.SaveChanges();
                return OperationResult.Success();
            }
            catch (Exception ex)
            {
                return OperationResult.Error(ex.Message);
            }

        }

        public OperationResult Delete(T entity)
        {
            try
            {

                var dbSet = _Contexct.Set<T>();

                // گرفتن کلید اصلی موجودیت
                

                // پیدا کردن رکورد از دیتابیس
                var existingEntity = dbSet.Find(entity.ID);
                if (existingEntity == null)
                    return OperationResult.NotFound();

                // حذف رکورد
                dbSet.Remove(existingEntity);

                // ذخیره تغییرات به صورت همزمان
                _Contexct.SaveChanges();

                return OperationResult.Success();
            }
            catch (Exception ex)
            {
                OperationResult op = new OperationResult();
                op.Status = OperationResult.OpreationResultStatus.Error;
                if (ex.InnerException.ToString().StartsWith("The DELETE statement conflicted with the REFERENCE"))
                {


                    op.Message = "به دلیل استفاده از این مقدار انجام عملیات وجود ندارد";

                }
                else
                {
                    op.Message = ex.Message;
                }
                return op;
            }
        }


        //public OperationResult AddLoge(long UserID, OperationType operationType, DateTime dateTime)
        //{
            
        //    return OperationResult.Success();
        //}


    }

    public class RepoFullBase<T>  where T : Models.ModelBaseFull
    {
        protected RealEstateContext _Contexct { get; set; }

        public RepoFullBase(RealEstateContext repositoryContext)
        {
            _Contexct = repositoryContext;
        }

        public RepoFullBase()
        {
        }

        public T FindByID(long ID)
        {

            return _Contexct.Set<T>().Where(x => x.ID == ID).FirstOrDefault();
        }

        public IQueryable<T> FindAll() => _Contexct.Set<T>().AsNoTracking();

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression) =>
            _Contexct.Set<T>().Where(expression).AsNoTracking();

        public OperationResult Create(T entity)
        {
            try
            {
                entity.UserIDCreator = ClsBaseSetting.UserID;
                entity.CreationDate = DateTime.Now;
                _Contexct.Set<T>().Add(entity);
                _Contexct.SaveChanges();
                return OperationResult.Success();
            }
            catch (Exception ex)
            {
                return OperationResult.Error(ex.Message);
            }
        }

        public OperationResult Update(T entity)
        {
            try
            {
                entity.ModificationDate = DateTime.Now;
                entity.UserIDModifier = ClsBaseSetting.UserID;
                var trackedEntity = _Contexct.Set<T>().Local.FirstOrDefault(e => e.ID == entity.ID);
                if (trackedEntity != null)
                {
                    _Contexct.Entry(trackedEntity).CurrentValues.SetValues(entity);
                }
                else
                {
                    var dbEntity = _Contexct.Set<T>().FirstOrDefault(w => w.ID == entity.ID);
                    if (dbEntity != null)
                    {
                        _Contexct.Entry(dbEntity).CurrentValues.SetValues(entity);
                    }
                    else
                    {
                        _Contexct.Set<T>().Add(entity); // اگر جدید بود
                    }
                }

                _Contexct.SaveChanges();
                return OperationResult.Success();
            }
            catch (Exception ex)
            {
                return OperationResult.Error(ex.Message);
            }

        }

        public OperationResult Delete(T entity)
        {
            try
            {

                var dbSet = _Contexct.Set<T>();

                

                // پیدا کردن رکورد از دیتابیس
                var existingEntity = dbSet.Find(entity.ID);
                if (existingEntity == null)
                    return OperationResult.NotFound();

                // حذف رکورد
                dbSet.Remove(existingEntity);

                // ذخیره تغییرات به صورت همزمان
                _Contexct.SaveChanges();

                return OperationResult.Success();
            }
            catch (Exception ex)
            {
                OperationResult op = new OperationResult();
                op.Status = OperationResult.OpreationResultStatus.Error;
                if (ex.InnerException.ToString().StartsWith("The DELETE statement conflicted with the REFERENCE"))
                {


                    op.Message = "به دلیل استفاده از این مقدار انجام عملیات وجود ندارد";

                }
                else
                {
                    op.Message = ex.Message;
                }
                return op;
            }
        }
        
        public OpratorsName GetOperatorName(long CreateID,long? ModifierID)
        {
            OpratorsName opratorsName = new OpratorsName();

            var query = _Contexct.Users_List.ToList();
            opratorsName.CreatorName = query.Where(x => x.ID == CreateID).FirstOrDefault().Name;
            if(ModifierID!=null)
            opratorsName.ModifierName = query.Where(x => x.ID == ModifierID).FirstOrDefault().Name;

            return opratorsName;
        }
        //public OperationResult AddLoge(long UserID, OperationType operationType, DateTime dateTime)
        //{

        //    return OperationResult.Success();
        //}


    }

}
