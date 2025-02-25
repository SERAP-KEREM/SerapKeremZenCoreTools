using UnityEngine;
using UnityEngine.UI;
using Zenject;
using TriInspector;
using TMPro;

namespace SerapKeremZenCoreTools._Game.TimeSystem
{
    /// <summary>
    /// A test class for the TimeManager to verify its functionality in a test scene.
    /// </summary>
    public class TimeManagerTest : MonoBehaviour
    {
        #region Variables

        [Group("UI References")]
        [Required]
        [SerializeField] private TextMeshProUGUI _timerText;

        [Group("UI References/Buttons")]
        [Required]
        [SerializeField] private Button _startButton;

        [Required]
        [SerializeField] private Button _stopButton;

        [Required]
        [SerializeField] private Button _resetButton;

        [Required]
        [SerializeField] private Button _addTimeButton;

        [Required]
        [SerializeField] private Button _subtractTimeButton;

        [Required]
        [SerializeField] private Button _freezeButton;

        [Required]
        [SerializeField] private Button _pauseGameButton;

        [Required]
        [SerializeField] private Button _resumeGameButton;

        private TimeManager _timeManager;

        #endregion

        #region Initialization

        /// <summary>
        /// Constructor method for dependency injection.
        /// </summary>
        /// <param name="timeManager">The TimeManager instance to be tested.</param>
        [Inject]
        private void Construct(TimeManager timeManager)
        {
            _timeManager = timeManager;
        }

        /// <summary>
        /// Initializes the test by setting up UI and subscribing to events.
        /// </summary>
        private void Start()
        {
            SetupUI();
            _timeManager.OnTimerUpdated += UpdateTimerDisplay;
            _timeManager.OnTimeFinished += HandleTimeFinished;
        }

        #endregion

        #region UI Setup

        /// <summary>
        /// Sets up button listeners and initializes the timer display.
        /// </summary>
        private void SetupUI()
        {
            _startButton.onClick.AddListener(_timeManager.StartTimer);
            _stopButton.onClick.AddListener(_timeManager.StopTimer);
            _resetButton.onClick.AddListener(_timeManager.ResetTimer);
            _addTimeButton.onClick.AddListener(() => _timeManager.AddTime(10f));
            _subtractTimeButton.onClick.AddListener(() => _timeManager.SubtractTime(10f));
            _freezeButton.onClick.AddListener(() => _timeManager.FreezeTimeForDuration(3f));
            _pauseGameButton.onClick.AddListener(_timeManager.PauseGame);
            _resumeGameButton.onClick.AddListener(_timeManager.ResumeGame);

            UpdateTimerDisplay(_timeManager.GetCurrentTime(), _timeManager.GetMaxTime());
        }

        #endregion

        #region Event Handlers

        /// <summary>
        /// Updates the timer display with the current time and highlights critical time in red.
        /// </summary>
        /// <param name="currentTime">The current time remaining on the timer.</param>
        /// <param name="maxTime">The maximum time allowed for the timer.</param>
        private void UpdateTimerDisplay(float currentTime, float maxTime)
        {
            int minutes = Mathf.FloorToInt(currentTime / 60);
            int seconds = Mathf.FloorToInt(currentTime % 60);
            string timeText = $"{minutes:00}:{seconds:00}";

            if (_timeManager.IsCriticalTime())
            {
                timeText = $"<color=red>{timeText}</color>";
            }

            _timerText.text = timeText;
        }

        /// <summary>
        /// Handles the event when the timer reaches zero.
        /// </summary>
        private void HandleTimeFinished()
        {
            Debug.Log("Time's up!");
            _timerText.text = "<color=red>00:00</color>";
        }

        #endregion

        #region Cleanup

        /// <summary>
        /// Cleans up event subscriptions and button listeners when the object is destroyed.
        /// </summary>
        private void OnDestroy()
        {
            if (_timeManager != null)
            {
                _timeManager.OnTimerUpdated -= UpdateTimerDisplay;
                _timeManager.OnTimeFinished -= HandleTimeFinished;
            }

            if (_startButton) _startButton.onClick.RemoveListener(_timeManager.StartTimer);
            if (_stopButton) _stopButton.onClick.RemoveListener(_timeManager.StopTimer);
            if (_resetButton) _resetButton.onClick.RemoveListener(_timeManager.ResetTimer);
            if (_addTimeButton) _addTimeButton.onClick.RemoveListener(() => _timeManager.AddTime(10f));
            if (_subtractTimeButton) _subtractTimeButton.onClick.RemoveListener(() => _timeManager.SubtractTime(10f));
            if (_freezeButton) _freezeButton.onClick.RemoveListener(() => _timeManager.FreezeTimeForDuration(3f));
            if (_pauseGameButton) _pauseGameButton.onClick.RemoveListener(_timeManager.PauseGame);
            if (_resumeGameButton) _resumeGameButton.onClick.RemoveListener(_timeManager.ResumeGame);
        }

        #endregion
    }
}