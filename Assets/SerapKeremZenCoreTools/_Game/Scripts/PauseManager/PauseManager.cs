using UnityEngine;
using UnityEngine.Events;
using Zenject;
using SerapKeremZenCoreTools._Game.TimeSystem;
using SerapKeremZenCoreTools._Game.UISystem;

namespace SerapKeremZenCoreTools._Game.PauseSystem
{
    /// <summary>
    /// Manages the game's pause and resume functionality.
    /// </summary>
    public class PauseManager : MonoBehaviour
    {
        #region Variables

        private TimeManager _timeManager;
        private UIManager _uiManager;
        private bool _isPaused;

        /// <summary>
        /// Event triggered when the pause state changes.
        /// </summary>
        public UnityEvent<bool> OnPauseStateChanged = new UnityEvent<bool>();

        #endregion

        #region Initialization

        /// <summary>
        /// Constructor method for dependency injection.
        /// </summary>
        /// <param name="timeManager">The TimeManager instance to handle time-related functionality.</param>
        /// <param name="uiManager">The UIManager instance to handle UI-related functionality.</param>
        [Inject]
        private void Construct(TimeManager timeManager, UIManager uiManager)
        {
            _timeManager = timeManager;
            _uiManager = uiManager;
        }

        /// <summary>
        /// Initializes the component by listening for the Escape key press.
        /// </summary>
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                TogglePause();
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Pauses the game and triggers the pause state change event.
        /// </summary>
        public void PauseGame()
        {
            if (_isPaused) return;

            _isPaused = true;
            _timeManager.PauseTime();
            OnPauseStateChanged.Invoke(_isPaused);
            ShowPauseMenu();
        }

        /// <summary>
        /// Resumes the game and triggers the pause state change event.
        /// </summary>
        public void ResumeGame()
        {
            if (!_isPaused) return;

            _isPaused = false;
            _timeManager.ResumeTime();
            OnPauseStateChanged.Invoke(_isPaused);
            HidePauseMenu();
        }

        /// <summary>
        /// Toggles the game's pause state between paused and resumed.
        /// </summary>
        public void TogglePause()
        {
            if (_isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }

        /// <summary>
        /// Checks whether the game is currently paused.
        /// </summary>
        /// <returns>True if the game is paused; otherwise, false.</returns>
        public bool IsGamePaused()
        {
            return _isPaused;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Shows the pause menu by displaying the settings panel.
        /// </summary>
        private void ShowPauseMenu()
        {
            _uiManager.ShowSettingsPanel();
        }

        /// <summary>
        /// Hides the pause menu by returning to the gameplay UI.
        /// </summary>
        private void HidePauseMenu()
        {
            _uiManager.ShowGameplayUI();
        }

        #endregion

        #region Cleanup

        /// <summary>
        /// Cleans up event listeners when the object is destroyed.
        /// </summary>
        private void OnDestroy()
        {
            OnPauseStateChanged.RemoveAllListeners();
        }

        #endregion
    }
}