using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    #region Fields

    #endregion

    #region Properties

    #endregion

    #region Methods

    /// <summary>
    /// Trigger level start
    /// </summary>
    public void HandleLevelButtonClick(int level) 
    {
        EventManager.TriggerEvent(EventName.StartLevel, new Dictionary<string, object> {
            { "level", level }
        });
    }

    #endregion
}
