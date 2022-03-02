using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candlemaker : MonoBehaviour
{
    #region Fields

    GameObject[] _ingredientSlots;
    List<Ingredient> _candleIngredients;
    int _slotCount;

    [SerializeField] GameObject _slotPrefab;
    [SerializeField] Transform _slotContainer;

    #endregion

    #region Properties

    /// <summary>
    /// Description of this exposed property
    /// </summary>
    // public bool ExampleProp
    // {
    //     get { return exampleField; }
    // }

    #endregion

    #region Methods

    /// <summary>
    /// Description of what's going on
    /// </summary>
    void Start()
    {
        EventManager.StartListening(EventName.UseIngredient, HandleUseIngredient);

    }

    /// <summary>
    /// Description of what's going on
    /// </summary>
    void Update()
    {
        
    }

    /// <summary>
    /// Setup for a level
    /// </summary>
    /// <param name="slotCount"># of ingredients required for a candle in this level</param>
    public void Setup(int slotCount = 4)
    {
        _slotCount = 4;
        ClearSlots();

        _ingredientSlots = new GameObject[slotCount];
        for (int i = 0; i < _slotCount; i++)
        {
            _ingredientSlots[i] = Instantiate(_slotPrefab, _slotContainer);
        }
    }

    void ClearSlots()
    {
        foreach (GameObject item in _ingredientSlots)
        {
            Ingredient slotIngredient = item.GetComponentInChildren<Ingredient>();
            Destroy(slotIngredient.gameObject);
        }

        _candleIngredients = new List<Ingredient>();
    }

    /// <summary>
    /// Listener for UseIngredient event. Adds ingredient to candle. If slots are full, creates candle, clears slots. 
    /// </summary>
    /// <param name="msg">Should have a key "ingredient" with the Ingredient object to add.</param>
    void HandleUseIngredient(Dictionary<string, object> msg)
    {
        Ingredient usedIngredient = (Ingredient) msg["ingredient"];
        // add ingredient to this candle
        _candleIngredients.Add(usedIngredient);

        Transform slot = _ingredientSlots[_candleIngredients.Count - 1].transform;
        // To do - animate the ingredient from the grid to the slot
        Ingredient slotIngredient = Instantiate(Resources.Load<Ingredient>("Prefabs/Ingredient"), slot);
        slotIngredient.Init(usedIngredient.Name, usedIngredient.Value);

        Debug.Log(_candleIngredients);
        if (_candleIngredients.Count >= _slotCount)
        {
            Candle candle = new Candle(_candleIngredients.ToArray());
            EventManager.TriggerEvent(EventName.CreateCandle, new Dictionary<string, object> {
                { "candle", candle }
            });
            ClearSlots();
        }
    }
    #endregion
}
