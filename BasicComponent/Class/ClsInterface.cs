

namespace BasicComponent
{
    public class ClsInterface
    {
        public enum EnumViewStatus
        {
            ViewMode,
            EditMode
        }

        public enum EnumLockStatus
        {

            ReadOnly,
            EditAble
        }

        public enum EnumBasicComponentType
        {
            Grid = 1,
            TextBox = 2,
            ComboBox = 3,
            CheckBox = 4,
            RadioBox = 5,
            CheckedListBox = 6,
            ComboDate = 7,
            ComboGrid = 8,
            MaskTextBox = 9,
            UserControl = 10,
            panel = 11
        }


        public enum ValidateType : byte
        {
            IsNullOrEmpty,
            ItemSelected,
            MaskFull
        }

        public enum EnumInputType
        {
            Strings = 1,
            Ints = 2,
            Decimal = 3,
            Jint = 4, // فقط عدد
            Custom = 5 // عدد به همراه /-
        }

        public enum InputLanguage
        {
            Ir = 1,
            Other = 2
        }

        public interface IBasicComponent
        {
            string BLObjectName { get; set; }
            string FieldName { get; set; }
            string GetMethodName { get; set; }
            bool IsRequierd { get; set; }

            void ClearValue();
            EnumViewStatus ViewStatus { get; set; }
            EnumLockStatus LockStatus { get; set; }
            EnumBasicComponentType BasicComponentType { get; }
            ValidateType ValidateType { get; set; }



        }

    }
}
