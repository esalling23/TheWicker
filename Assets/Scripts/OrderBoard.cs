using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class OrderBoard : MonoBehaviour
{
    #region Fields

    // Order support
    // A given number of orders will be visible
    // but only one will be the order the candle will
    // be made for
    [SerializeField] int _maxOrders = 3;
    List<Order> _visibleOrders = new List<Order>();
    Order _currentOrder;
    [SerializeField] Order _orderPrefab;
    [SerializeField] Transform _orderContainer;

    #endregion

    #region Properties

    /// <summary>
    /// Description of this exposed property
    /// </summary>
    public List<Order> VisibleOrders
    {
        get { return _visibleOrders; }
    }

    public Order ActiveOrder
    {
        get { return  _visibleOrders[0]; }
    }

    #endregion

    #region Methods

    /// <summary>
    /// Description of what's going on
    /// </summary>
    void Start()
    {
        EventManager.StartListening(EventName.CreateCandle, HandleCreateCandle);
        EventManager.StartListening(EventName.OrderTimeOut, HandleOrderTimeOut);
    }

    /// <summary>
    /// Description of what's going on
    /// </summary>
    void Update()
    {
        
    }

    /// <summary>
    /// Updates orders on the order board
    /// </summary>
    /// <param name="reset">Option to reset all orders</param>
    public void UpdateBoard(bool reset) 
    {
        if (reset)
        {
            _visibleOrders = new List<Order>();
        }

        if (_visibleOrders.Count < _maxOrders)
        {
            int newOrderCount = _maxOrders - _visibleOrders.Count;
            for (int i = 0; i < newOrderCount; i++)
            {
                Order o = Instantiate(_orderPrefab, _orderContainer);
                o.Init(LevelManager.Instance.OrderMinTime, LevelManager.Instance.OrderMaxTime);
                _visibleOrders.Add(o);
            }
        }

        ActiveOrder.StartOrder();
    }

    private void HandleOrderTimeOut(Dictionary<string, object> msg) 
    {
        // Remove the first order, which is the active order & the only
        // possible order that could've timed out
        _visibleOrders.RemoveAt(0);
        // Orders destroy themselves
        // Replace orders when they're removed
        UpdateBoard(false);
    }

    private void HandleCreateCandle(Dictionary<string, object> msg)
    {
        int score = 0;
        Candle candle = (Candle) msg["candle"];
        // bool completesOrder = false;
        // to do - calculate match score
        // foreach (Ingredient ingredient in candle.Ingredients)
        // {
            // if (_requestIngredientsingredient.name)
        // }
        score = 100;
        // Remove order
        _visibleOrders.RemoveAt(0);

        EventManager.TriggerEvent(EventName.OrderComplete, new Dictionary<string, object> {
            { "score", score }
        });

    }

    #endregion
}
