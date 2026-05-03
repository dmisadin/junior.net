using System.ComponentModel;

namespace AbySalto.Junior.Domain.Enums
{
    public enum Currency
    {
        [Description("Euro")]
        EUR = 0,

        [Description("US Dollar")]
        USD = 1,

        [Description("British Pound")]
        GBP = 2,

        [Description("Croatian Kuna")]
        HRK = 3
    }
}
