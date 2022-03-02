using System;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    #region Fields

    [SerializeField] GridItem _gridItemPrefab;

    // Track ingredients in the grid
    List<GridItem> _gridItems;
    GridItem _selectedItem;

    // An n x n grid
    int _gridSize = 5;

    #endregion

    #region Properties

    public List<GridItem> GridItems
    {
        get { return _gridItems; }
    }

    #endregion

    #region Methods

    /// <summary>
    /// Description of what's going on
    /// </summary>
    void Start()
    {
        EventManager.StartListening(EventName.UseIngredient, HandleUseGridItem);
        EventManager.StartListening(EventName.SelectGridItem, HandleSelectGridItem);
    }

    /// <summary>
    /// Description of what's going on
    /// </summary>
    void Update()
    {
        
    }

    void Setup()
    {

    }

    /// <summary>
    /// Generates random ingredient
    /// </summary>
    /// <returns>new instantiated GridItem object</returns>
    private Ingredient GenerateIngredient() 
    {
        System.Random rand = new System.Random();
        // Random ingredient type
        Array ingredientTypes = Enum.GetValues(typeof(IngredientName));
        int randIngredientIndex = rand.Next(ingredientTypes.Length);
        IngredientName randIngredientType = (IngredientName) ingredientTypes.GetValue(randIngredientIndex);
        // Get a random value based on available value colors
        int randValue = rand.Next(GameManager.Instance.ValueColors.Length);
        // Instantiate new ingredient
        Ingredient newIngredient = Instantiate(Resources.Load<Ingredient>("Prefabs/Ingredient"), Vector2.zero, Quaternion.identity);
        newIngredient.Init(randIngredientType, randValue);

        Debug.Log(newIngredient.GetComponent<RectTransform>().offsetMin);

        return newIngredient;
    }

    /// <summary>
    /// Generates an N x N sized grid of random ingredients
    /// </summary>
    public void GenerateGrid(int gridSize)
    {
        _gridSize = gridSize;
        _gridItems = new List<GridItem>();
        // generate initial grid of ingredients
        for (int row = 0; row < _gridSize; row++)
        {
            for (int col = 0; col < _gridSize; col++)
            {
                Ingredient newIngredient = GenerateIngredient();
                // Create grid item that holds the random ingredient
                GridItem newGridItem = Instantiate(_gridItemPrefab, Vector2.zero, Quaternion.identity, transform);
                newGridItem.Init(newIngredient, new Vector2(row, col));

                _gridItems.Add(newGridItem);
            }
        }
    }

    void HandleSelectGridItem(Dictionary<string, object> msg) 
    {
        _selectedItem = (GridItem) msg["item"];
    }

    void CheckMatches(GridItem[] items)
    {
        // CheckMatches: 
        // check a set of items - so [2, 7, 12, 17]
        // if they have 2 of the same ingredient on either side - top, bottom, right, left
        // then remove the matches & update the item (2, 7, 12, or 17) 
        // a match w/ 3 different values will avg them
        // a match w/ 2 of the same value will use that value
        // a match w/ 3 of the same value will level up value to next highest
    }

    /// <summary>
    /// When an ingredient is used, remove the selected grid item's ingredient and get replacement
    /// </summary>
    /// <param name="msg">Should contain a key "ingredient" with the ingredient </param>
    void HandleUseGridItem(Dictionary<string, object> msg)
    {
        // Temp, just replace the item 
        _selectedItem.SetupIngredient(GenerateIngredient());
        _selectedItem = null;

        // To do 
        // shift around ingredients
        // it's a list of GridItems

        // ShiftGrid: 
        // All grid items above _selected should be shifted down
        // X = used ingredient
        // 0  1  2  3  4
        // 5  6  7  8  9 
        // 10 11 12 13 14
        // 15 16 X 18 19
        // 20 21 22 23 24
        // so the ingredient in slot 12 would move to 17
        // 7 would move to 12
        // 2 to 7
        // then a new ingredient would appear in slot 2

        // start with selected
        // if (_selected.)
        
    }
    #endregion
}
