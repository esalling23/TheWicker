using System.Diagnostics.Tracing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    #region Fields

    #endregion

    #region Properties

    #endregion

    #region Methods

    /// <summary>
    /// Return to level
    /// </summary>
    public void HandleContinueButtonClick() 
    {
        EventManager.TriggerEvent(EventName.TogglePause, new Dictionary<string, object> {
            { "pause", false }
        });
        Destroy(this.gameObject);
    }

    #endregion
}
