using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// An ingredient to be used for candlemaking
/// </summary>
public class Ingredient : MonoBehaviour
{
    #region Fields

    private IngredientName _name;
    private int _value;
    // background color indicates value
    private Image _background; 
    IngredientImage _ingredientImage;

    #endregion

    #region Properties

    /// <summary>
    /// Description of this exposed property
    /// </summary>
    public string DisplayName 
    {
        get { return Enum.GetName(typeof(IngredientName), _name); }
    }

    /// <summary>
    /// The value score of this ingredient 
    /// </summary>
    /// <value></value>
    public int Value 
    {
        get { return _value; }
    }

    /// <summary>
    /// The type/name of this ingredient
    /// </summary>
    /// <value></value>
    public IngredientName Name
    {
        get { return _name; }
    }
    
    #endregion

    #region Methods

    public void Init(IngredientName name, int value) 
    {
        _name = name;
        _value = value;

        // Instantiate a child image of the ingredient
        _ingredientImage = Instantiate(Resources.Load<IngredientImage>("Prefabs/IngredientImage"), transform);
        _ingredientImage.Init(_name);

        Debug.Log(GetComponent<RectTransform>().offsetMin);
        
        // Update the background of this component for the color value indicator
        _background = GetComponent<Image>();
        _background.color = GameManager.Instance.ValueColors[value];

    }

    #endregion
}
