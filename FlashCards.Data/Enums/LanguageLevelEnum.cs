using System.ComponentModel;

namespace FlashCards.Data.Enums
{
    public enum LanguageLevelEnum : int
    {
        [Description("A1")]
        A1 = 0,
        [Description("A2")]
        A2 = 1,
        [Description("B1")]
        B1 = 2,
        [Description("B2")]
        B2 = 3,
        [Description("B2/C1")]
        B2_C1 = 4,
        [Description("C1")]
        C1 = 5,
        [Description("C2")]
        C2 = 6,
        [Description("Not specified")]
        NotSpecified = 7
    }
}
