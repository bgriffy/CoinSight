namespace CoinConstraint.Client.Infrastructure.Extensions;

public static class StringExtension
{
    /// <summary>
    /// Truncates the string by the selected value and adds an ellipses
    /// </summary>
    public static string Truncate(this string value, int maxChars)
    {
        return value.Length <= maxChars ? value : $"{value.Substring(0, maxChars)}...";
    }
}
