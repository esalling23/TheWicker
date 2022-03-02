using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelGUI : MonoBehaviour
{
    #region Fields

    [SerializeField] PauseMenu _pauseMenu;

    #endregion

    #region Properties


    #endregion

    #region Methods

    /// <summary>
    /// Description of what's going on
    /// </summary>
    public void HandleExitButtonClick()
    {
        // to do: pause & confirm prompt? 
        EventManager.TriggerEvent(EventName.QuitLevel, null);
    }

    /// <summary>
    /// Description of what's going on
    /// </summary>
    public void HandlePauseButtonClick()
    {
        EventManager.TriggerEvent(EventName.TogglePause, new Dictionary<string, object> {
            { "pause", true }
        });
        Instantiate(_pauseMenu, transform);
    }
    #endregion
}
