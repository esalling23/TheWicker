using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// Stores relevant data about each ingredient type
/// </summary>
public struct IngredientData
{
    /// <summary>
    /// The image that represents this ingredient across the grid, in orders, etc.s
    /// </summary>
    public Sprite image;

    public IngredientData(Sprite img)
    {
        image = img;
    }
}

/// <summary>
/// Handles data loading & tracking, screen management, more
/// </summary>
public class GameManager : MonoBehaviour
{
    #region Fields

    // Singleton
    private static GameManager _instance;

    // Ingredients and their data
    private Dictionary<IngredientName, IngredientData> _allIngredients;

    // Current data
    private int _currentLevel;
    private int _money;

    // Levels
    [Header("Level(s) Settings - in ascending order")]
    [SerializeField] private LevelSettings[] _levels;

    [Header("Ingredient Value Colors - in ascending order")]
    [SerializeField] private Color[] _valueColors;

    #endregion

    #region Properties

    /// <summary>
    /// Exposed singleton instance
    /// </summary>
    public static GameManager Instance
    {
        get { return _instance; }
    }

    /// <summary>
    /// All available ingredients
    /// </summary>
    public Dictionary<IngredientName, IngredientData> AllIngredients
    {
        get { return _allIngredients; }
    }

    /// <summary>
    /// The LevelSettings object that represents the current level
    /// </summary>
    /// <value></value>
    public LevelSettings CurrentLevel
    {
        get { return _levels[_currentLevel]; }
    }

    public Color[] ValueColors
    {
        get { return _valueColors; }
    }

    #endregion

    #region Methods

    /// <summary>
    /// 
    /// </summary>
    private void Awake() 
    {
        // Initialize Utils
        ScreenUtils.Initialize();

        // Singleton management
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
        _allIngredients = new Dictionary<IngredientName, IngredientData>();
        // Initialize dictionary of ingredient names & their relevant data
        foreach (IngredientName name in Enum.GetValues(typeof(IngredientName)))
        {
            string strName = Enum.GetName(typeof(IngredientName), name);
            _allIngredients.Add(name, new IngredientData(Resources.Load<Sprite>($"Images/Ingredients/{strName}")));
        }

        EventManager.StartListening(EventName.StartLevel, HandleStartLevel);
        EventManager.StartListening(EventName.QuitLevel, HandleQuitLevel);

        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("SimpleCandlemaking"))
        {
            // if we load right into the candlemaking scene, just start up lvl 0
            StartLevel(0);
        }
    }

    /// <summary>
    /// Description of what's going on
    /// </summary>
    void Update()
    {
        
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="msg"></param>
    void HandleQuitLevel(Dictionary<string, object> msg)
    {
        ChangeScreen(ScreenName.MainMenu);
    }

    /// <summary>
    /// Handles start level event. Changes to level scene, initializes level manager.
    /// </summary>
    /// <param name="msg">Should contain a key "level" with an integer value that 
    /// corresponds to an index in the LevelSettings array.</param>
    void HandleStartLevel(Dictionary<string, object> msg)
    {
        ChangeScreen(ScreenName.Level);

        StartCoroutine(WaitForLevelLoad(() => {
            StartLevel((int) msg["level"]);
        }));
    }

    void StartLevel(int level)
    {
        _currentLevel = level;
        LevelManager.Instance.InitLevel(CurrentLevel);
    }

    /// <summary>
    /// Changes screen to reflect a provided screen. Mostly scene changes. 
    /// </summary>
    /// <param name="scene">ScreenName value of the screen to go to</param>
    void ChangeScreen(ScreenName scene)
    {
        switch (scene) 
        {
            case ScreenName.MainMenu:
                SceneManager.LoadScene("LevelSelection");
                break;
            
            case ScreenName.Level:
                SceneManager.LoadScene("SimpleCandlemaking");
                break;
        }
    }

    IEnumerator WaitForLevelLoad(Action callback)
    {
        int sceneNumber = SceneManager.GetSceneByName("SimpleCandlemaking").buildIndex;
        while (SceneManager.GetActiveScene().buildIndex != sceneNumber)
        {
            yield return null;
        }
 
        // Do anything after proper scene has been loaded
        if (SceneManager.GetActiveScene().buildIndex == sceneNumber)
        {
            Debug.Log(SceneManager.GetActiveScene().buildIndex);
            callback();
        }
    }
    #endregion
}
