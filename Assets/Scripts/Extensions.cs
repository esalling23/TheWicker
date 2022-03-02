using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;

/// <summary>
/// Extension functions
/// </summary>
static class Extensions
{
    #region Fields
    #endregion

    #region Properties
    #endregion

    #region Methods

    /// <summary>
    /// Converts PascalCase string into a sentence
    /// </summary>
    /// <param name="str">PascalCase word to turn into a sentence</param>
    /// <returns>Modified string</returns>
    static string ToSentenceCase(this string str)
    {
        return Regex.Replace(str, "[a-z][A-Z]", m => $"{m.Value[0]} {char.ToLower(m.Value[1])}");
    }

    #endregion
}
