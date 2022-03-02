using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>
public class LevelManager : MonoBehaviour
{
    #region Fields

    // Singleton
    private static LevelManager _instance;
    private LevelSettings _settings;

    // Level timer
    private Timer _timer;

    // Player level data
    private int _money;
    private int _ordersLost;
    private int _ordersCompleted;
    private List<Candle> _candlesCreated;

    [Header("Level Objects")]
    [SerializeField] OrderBoard _orderBoard;
    [SerializeField] Grid _grid;

    #endregion

    #region Properties

    /// <summary>
    /// Exposed singleton instance
    /// </summary>
    public static LevelManager Instance
    {
        get { return _instance; }
    }

    public int OrdersLost
    {
        get { return _ordersLost; }
    }

    public int OrdersCompleted
    {
        get { return _ordersCompleted; }
    }

    public int OrderIngredientLimit
    {
        get { return _settings.orderIngredientLimit; }
    }

    public float OrderMinTime
    {
        get { return _settings.minOrderTime; }
    }

    public float OrderMaxTime
    {
        get { return _settings.maxOrderTime; }
    }

    #endregion

    #region Methods

    /// <summary>
    /// 
    /// </summary>
    private void Awake() 
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        _instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    /// <summary>
    /// Description of what's going on
    /// </summary>
    void Start()
    {
        EventManager.StartListening(EventName.OrderTimeOut, HandleLoseOrder);
        EventManager.StartListening(EventName.OrderComplete, HandleCompleteOrder);
        EventManager.StartListening(EventName.TogglePause, HandleTogglePause);
    }

    /// <summary>
    /// Description of what's going on
    /// </summary>
    void Update()
    {
        
    }

    public void InitLevel(LevelSettings settings)
    {
        // Reset player data
        _money = 0;
        _ordersLost = 0;
        _ordersCompleted = 0;
        _candlesCreated = new List<Candle>();

        // Level settings
        _settings = settings;

        // Setup and start timer
        _timer = gameObject.AddComponent<Timer>();
        _timer.Duration = _settings.levelTimeLimit;

        _grid = GameObject.FindGameObjectWithTag("Grid").GetComponent<Grid>();
        _orderBoard = GameObject.FindGameObjectWithTag("OrderBoard").GetComponent<OrderBoard>();

        // Setup order board, candle making area
        _orderBoard.UpdateBoard(true);
        _grid.GenerateGrid(5);
    }

    private void HandleLoseOrder(Dictionary<string, object> msg) 
    {
        _ordersLost++;
        Debug.Log($"Order Lost :S \nCount: {_ordersLost}");
    }

    private void HandleCompleteOrder(Dictionary<string, object> msg) 
    {
        _ordersCompleted++;
        Debug.Log($"Order Completed :D \nCount: {_ordersCompleted}");
    }

    void HandleTogglePause(Dictionary<string, object> msg)
    {
        _timer.TogglePause((bool) msg["pause"]);
    }

    #endregion
}
