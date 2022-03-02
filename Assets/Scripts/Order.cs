using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Order : MonoBehaviour
{
    #region Fields

    // Timer support
    float _timeLimit;
    Timer _timer;
    [SerializeField] TextMeshProUGUI _timerText;

    // Request support
    IngredientName[] _ingredientRequests;
    IngredientImage _requestImage;
    [SerializeField] Transform _requestContainer;

    #endregion

    #region Properties

    /// <summary>
    /// Description of this exposed property
    /// </summary>
    public IngredientName[] IngredientRequests
    {
        get { return _ingredientRequests; }
    }

    #endregion

    #region Methods

    public Order Init(float timeMin, float timeMax)
    {
        // Setup timer
        // Use Random.Range for random float
        _timeLimit = UnityEngine.Random.Range(timeMin, timeMax);
        _timer = gameObject.AddComponent<Timer>();
        _timer.Duration = _timeLimit;
        
        // set random type request
        System.Random rand = new System.Random();
        Array values = Enum.GetValues(typeof(IngredientName));
        int randIndex = rand.Next(0, values.Length);
        // int randCount = rand.Next(0, LevelManager.Instance.Order);
        // Temporarily support only 1 ingredient request
        _ingredientRequests = new IngredientName[] { (IngredientName) values.GetValue(randIndex) };

        IngredientName temp = _ingredientRequests[0];

        // Create ingredient image
        IngredientImage ingredientImage = Resources.Load<IngredientImage>("Prefabs/IngredientImage");
        _requestImage = Instantiate(ingredientImage, _requestContainer);
        _requestImage.Init(temp);

        return this;
    }

    /// <summary>
    /// Description of what's going on
    /// </summary>
    void Start()
    {
        EventManager.StartListening(EventName.TogglePause, HandleTogglePause);

        _timerText.text = "";
    }

    /// <summary>
    /// Description of what's going on
    /// </summary>
    void Update()
    {
        if (_timer.Running)
        {
            string newTime = FormatUtils.FormatTimer(_timer.TimeLeft);
            _timerText.text = newTime;
        } 
        else if (_timer.Finished) 
        {
            // Clear timer, destroy self
            _timer.Stop();
            Destroy(this.gameObject);

            EventManager.TriggerEvent(EventName.OrderTimeOut, new Dictionary<string, object> {
                { "order", this },
            });
        }
    }

    public void StartOrder() 
    {
        _timer.Run();
    }

    void HandleTogglePause(Dictionary<string, object> msg)
    {
        _timer.TogglePause((bool) msg["pause"]);
    }

    #endregion
}
