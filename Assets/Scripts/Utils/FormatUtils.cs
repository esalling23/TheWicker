using System;

/// <summary>
/// Provides screen utilities
/// </summary>
public static class FormatUtils
{
    #region Fields

    #endregion

    #region Properties

    #endregion

    #region Methods

    /// <summary>
    /// Formats seconds time to m:ss format
    /// </summary>
    public static string FormatTimer(float s)
    {
        TimeSpan t = TimeSpan.FromSeconds(s);
        return string.Format("{0:D2}:{1:D2}", 
                        t.Minutes, 
                        t.Seconds);
    }

    // public static string GetEnumString<T>(int enumValue)
    // {

    // }

    #endregion
}