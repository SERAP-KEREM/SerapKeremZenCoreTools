using UnityEngine;
using UnityEngine.Events;
using Zenject;
using TriInspector;
using System.Collections;

namespace SerapKeremZenCoreTools._Game.TimeSystem
{
    /// <summary>
    /// Manages time-related functionality such as timers, pausing, and freezing time.
    /// </summary>
    public class TimeManager : MonoBehaviour
    {
        #region Variables

        [Header("TimeManager Parameters")]
        [SerializeField, Tooltip("Initial time for the level in seconds.")]
        private float _levelTime = 180f;

        [SerializeField, Tooltip("Maximum possible time in seconds.")]
        private float _maxPossibleTime = 300f;

        [SerializeField, Tooltip("The time threshold considered critical (e.g., low time warning).")]
        private float _criticalTimeThreshold = 15f;

        [ShowInInspector, ReadOnly]
        private float _currentLevelTime;

        [ShowInInspector, ReadOnly]
        private bool _isTimerRunning;

        private bool _isGamePaused;
        private Coroutine _freezeCoroutine;

        #endregion

        #region Events

        /// <summary>
        /// Invoked when the timer is updated. Provides current time and max time.
        /// </summary>
        public UnityAction<float, float> OnTimerUpdated;

        /// <summary>
        /// Invoked when the timer reaches zero.
        /// </summary>
        public UnityAction OnTimeFinished;

        #endregion

        #region Initialization

        /// <summary>
        /// Initializes the timer with default values.
        /// </summary>
        private void Start()
        {
            ResetTimer();
        }

        /// <summary>
        /// Updates the timer every frame if it's running and the game is not paused.
        /// </summary>
        private void Update()
        {
            if (!_isTimerRunning || _isGamePaused) return;

            _currentLevelTime -= Time.deltaTime;

            if (_currentLevelTime <= 0)
            {
                _currentLevelTime = 0;
                StopTimer();
                OnTimeFinished?.Invoke();
            }
            else
            {
                OnTimerUpdated?.Invoke(_currentLevelTime, _levelTime);
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Starts the timer.
        /// </summary>
        public void StartTimer()
        {
            _isTimerRunning = true;
        }

        /// <summary>
        /// Stops the timer.
        /// </summary>
        public void StopTimer()
        {
            _isTimerRunning = false;
        }

        /// <summary>
        /// Resets the timer to its initial value.
        /// </summary>
        public void ResetTimer()
        {
            _currentLevelTime = _levelTime;
            _isTimerRunning = false;
            OnTimerUpdated?.Invoke(_currentLevelTime, _levelTime);
        }

        /// <summary>
        /// Adds time to the current timer value without exceeding the maximum possible time.
        /// </summary>
        /// <param name="timeToAdd">The amount of time to add.</param>
        public void AddTime(float timeToAdd)
        {
            _currentLevelTime = Mathf.Min(_currentLevelTime + timeToAdd, _maxPossibleTime);
            OnTimerUpdated?.Invoke(_currentLevelTime, _levelTime);
        }

        /// <summary>
        /// Subtracts time from the current timer value without going below zero.
        /// </summary>
        /// <param name="timeToSubtract">The amount of time to subtract.</param>
        public void SubtractTime(float timeToSubtract)
        {
            _currentLevelTime = Mathf.Max(_currentLevelTime - timeToSubtract, 0);

            if (_currentLevelTime <= 0)
            {
                StopTimer();
                OnTimeFinished?.Invoke();
            }

            OnTimerUpdated?.Invoke(_currentLevelTime, _levelTime);
        }

        /// <summary>
        /// Freezes the timer for a specified duration.
        /// </summary>
        /// <param name="duration">The duration to freeze the timer in seconds.</param>
        public void FreezeTimeForDuration(float duration)
        {
            if (_freezeCoroutine != null)
            {
                StopCoroutine(_freezeCoroutine);
            }

            _freezeCoroutine = StartCoroutine(FreezeTimeCoroutine(duration));
        }

        /// <summary>
        /// Pauses the game by setting the time scale to zero.
        /// </summary>
        public void PauseGame()
        {
            if (_isGamePaused) return;

            Time.timeScale = 0f;
            _isGamePaused = true;
        }

        /// <summary>
        /// Resumes the game by restoring the time scale to one.
        /// </summary>
        public void ResumeGame()
        {
            if (!_isGamePaused) return;

            Time.timeScale = 1f;
            _isGamePaused = false;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Coroutine to freeze the timer for a specified duration.
        /// </summary>
        /// <param name="duration">The duration to freeze the timer in seconds.</param>
        /// <returns>An IEnumerator for the coroutine.</returns>
        private IEnumerator FreezeTimeCoroutine(float duration)
        {
            bool wasRunning = _isTimerRunning;
            _isTimerRunning = false;

            yield return new WaitForSecondsRealtime(duration);

            _isTimerRunning = wasRunning;
            _freezeCoroutine = null;
        }

        #endregion

        #region Utility Methods

        /// <summary>
        /// Checks if the current time is below the critical threshold.
        /// </summary>
        /// <returns>True if the current time is critical; otherwise, false.</returns>
        public bool IsCriticalTime() => _currentLevelTime <= _criticalTimeThreshold;

        /// <summary>
        /// Gets the current time remaining on the timer.
        /// </summary>
        /// <returns>The current time in seconds.</returns>
        public float GetCurrentTime() => _currentLevelTime;

        /// <summary>
        /// Gets the maximum time allowed for the timer.
        /// </summary>
        /// <returns>The maximum time in seconds.</returns>
        public float GetMaxTime() => _levelTime;

        /// <summary>
        /// Checks if the timer is currently running.
        /// </summary>
        /// <returns>True if the timer is running; otherwise, false.</returns>
        public bool IsTimerRunning() => _isTimerRunning;

        /// <summary>
        /// Checks if the game is currently paused.
        /// </summary>
        /// <returns>True if the game is paused; otherwise, false.</returns>
        public bool IsGamePaused() => _isGamePaused;

        #endregion
    }
}