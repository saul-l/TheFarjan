using UnityEngine;
using System.Globalization;

public static class UIUtils
{
    public static string ResourceString(float amount)
    {
        amount /= 10;
        return amount.ToString("F1", CultureInfo.InvariantCulture);
    }
}
