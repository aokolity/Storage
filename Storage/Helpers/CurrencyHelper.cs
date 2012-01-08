using System.Globalization;

namespace Storage.Helpers
{
    public static class CurrencyHelper
    {
        public static string FormatCurrency(decimal value)
        {
            NumberFormatInfo nfi = CultureInfo.CurrentCulture.NumberFormat;
            
            nfi = (NumberFormatInfo)nfi.Clone();
            nfi.CurrencySymbol = "";

            return string.Format(nfi, "{0:c}", value);
        }

        public static string GetCurrentCultureName()
        {
            return CultureInfo.CurrentCulture.Name;
        }
    }
}