using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Candle : MonoBehaviour
{
    #region Fields

    int _value;
    Ingredient[] _ingredients;

    #endregion

    #region Properties

    /// <summary>
    /// Description of this exposed property
    /// </summary>
    public Ingredient[] Ingredients
    {
        get { return _ingredients; }
    }

    public int Value 
    {
        get { return _value; }
    }

    #endregion

    #region Methods

    public Candle(Ingredient[] ingredients) 
    {
        _value = 0;
        _ingredients = ingredients;
        // configure candle based on ingredients
        foreach (Ingredient ingredient in _ingredients)
        {
            _value += ingredient.Value;
        }

        string pointsDebug = $"Value: {_value.ToString()}";
        string typeDebug = $"Types: {String.Join("", _ingredients.Select(s => s.DisplayName))}";
        Debug.Log($"Candle Created!\n{pointsDebug}\n{typeDebug}\n");
    }

    #endregion
}
