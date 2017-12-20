using System;
using System.ComponentModel;

namespace HandHistories.Objects.GameDescription
{
    public enum PokerFormat : byte
    {
        [Description("Cash game")]
        CashGame = 0,

        [Description("Sit and Go")]
        SitAndGo = 1,

        [Description("Tournament")]
        MultiTableTournament = 2,

        [Description("Unknown")]
        Unknown = 7
    }

    public static class AttributesHelperExtension
    {
        public static string ToDescription(this Enum value)
        {
            var da = (DescriptionAttribute[])(value.GetType().GetField(value.ToString())).GetCustomAttributes(typeof(DescriptionAttribute), false);
            return da.Length > 0 ? da[0].Description : value.ToString();
        }
    }
}