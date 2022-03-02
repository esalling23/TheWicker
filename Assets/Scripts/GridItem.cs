using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// Represens a spot in the grid
/// </summary>
public class GridItem : MonoBehaviour, IPointerDownHandler
{
    #region Fields

    bool _selected = false;
    Ingredient _ingredient;
    Vector2 _gridPosition;
    Image _background;
    [SerializeField] Transform _itemContainer;

    #endregion

    #region Properties

    public bool Selected
    {
        get { return _selected; }
    }

    public int Row
    {
        get { return (int) _gridPosition.x; }
    }

    public int Column
    {
        get { return (int) _gridPosition.y; }
    }

    public Ingredient Ingredient
    {
        get { return _ingredient; }
    }

    #endregion

    #region Methods

    /// <summary>
    /// GridItem holds an ingredient and tracks it's own position in the grid
    /// </summary>
    public void Init(Ingredient ingredient, Vector2 gridPos)
    {
        // Initiaize vars
        _selected = false;
        _gridPosition = gridPos;
        // Setup new ingredient
        SetupIngredient(ingredient);

        // Setup background
        _background = GetComponent<Image>();
        _background.color = Color.clear;

        BoxCollider2D boxCollider = GetComponent<BoxCollider2D>();
        boxCollider.size = new Vector2(150, 150);

        EventManager.StartListening(EventName.SelectGridItem, HandleItemSelected);
    }

    public void Start() {
        
    }

    private void HandleItemSelected (Dictionary<string, object> msg) 
    {
        bool isNotMe = (GridItem) msg["item"] != this;
        if (Selected && isNotMe) 
        {
            _selected = false;
            _background.color = Color.clear;
        }
    }

    public void SetupIngredient(Ingredient newIngredient)
    {
        _selected = false;
        _ingredient = newIngredient;
        // Make the ingredient a child of this gridItem
        _ingredient.transform.SetParent(transform);
        // Reset ingredient "position"
        RectTransform ingredientRt = _ingredient.GetComponent<RectTransform>();
        ingredientRt.offsetMax = Vector2.zero;
        ingredientRt.offsetMin = Vector2.zero;
    }

    public void OnPointerDown(PointerEventData eventData) 
    {
        Debug.Log(eventData);
        Debug.Log("Click on grid item");
        if (Selected)
        {
            _background.color = Color.clear;
            EventManager.TriggerEvent(EventName.UseIngredient, new Dictionary<string, object> {
                { "ingredient", _ingredient }
            });
        }
        else 
        {
            _selected = true;
            _background.color = Color.green;
            EventManager.TriggerEvent(EventName.SelectGridItem, new Dictionary<string, object> {
                { "item", this }
            });
        }
    }

    #endregion
}
