using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A timer
/// </summary>
public class Timer : MonoBehaviour
{
	#region Fields

	// timer duration
	float _totalSeconds = 0;

	// timer execution
	float _elapsedSeconds = 0;
	bool _running = false;

	// support for Finished property
	bool _started = false;

	#endregion

	#region Properties

	/// <summary>
	/// Sets the duration of the timer
	/// The duration can only be set if the timer isn't currently running
	/// </summary>
	/// <value>duration</value>
	public float Duration
  {
		set
    {
			if (!_running)
      {
				_totalSeconds = value;
			}
		}
	}

  /// <summary>
	/// Gets time left on the timer
	/// </summary>
	public float TimeLeft
  {
		get
    {
      if (Finished) 
      {
        return 0;
      }
      return _totalSeconds - _elapsedSeconds;
		}
	}

	/// <summary>
	/// Gets whether or not the timer has finished running
	/// This property returns false if the timer has never been started
	/// </summary>
	/// <value>true if finished; otherwise, false.</value>
	public bool Finished
  {
		get { return _started && !_running; } 
	}

	/// <summary>
	/// Gets whether or not the timer is currently running
	/// </summary>
	/// <value>true if running; otherwise, false.</value>
	public bool Running
  {
		get { return _running; }
	}

	/// <summary>
	/// Gets whether the timer has started
	/// </summary>
	/// <value></value>
	public bool Started 
	{
		get { return _started; }
	}

  #endregion

  #region Methods

  /// <summary>
  /// Update is called once per frame
  /// </summary>
  void Update()
  {	
		// update timer and check for finished
		if (_running)
    {
			_elapsedSeconds += Time.deltaTime;
			if (_elapsedSeconds >= _totalSeconds)
      {
				_running = false;
			}
		}
	}

	/// <summary>
	/// Runs the timer
	/// Because a timer of 0 duration doesn't really make sense,
	/// the timer only runs if the total seconds is larger than 0
	/// This also makes sure the consumer of the class has actually 
	/// set the duration to something higher than 0
	/// </summary>
	public void Run()
  {	
		// only run with valid duration
		if (_totalSeconds > 0)
    {
			_started = true;
			_running = true;
      _elapsedSeconds = 0;
		}
	}

  /// <summary>
  /// Stops the timer
  /// </summary>
  public void Stop()
  {
    _started = false;
    _running = false;
    _elapsedSeconds = 0;
  }

  /// <summary>
  /// Pauses the timer
  /// </summary>
  public void TogglePause(bool pause)
  {
    _running = !pause;
  }

	#endregion
}