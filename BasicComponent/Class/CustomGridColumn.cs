namespace BasicComponent
{

    public enum CustomColumnType : byte
    {
        DefaultDecimal,
        BaseCurrencyDecimal,
        MustBeWithoutFormat,

        N0,
        Qty,
        Percent,

        DateShamsi,
        DateMiladi,
        DateHijri,

        ImageAndTxt
    }

    public class CustomGridColumn
    {
        public string Key { get; set; }
        public bool Selectable { get; set; } = true;
        public CustomColumnType Type { get; set; } = CustomColumnType.MustBeWithoutFormat;

    }
}
