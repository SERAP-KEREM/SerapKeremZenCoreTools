using UnityEngine;
using TriInspector;
using Zenject;
using SerapKeremZenCoreTools._Game.PauseSystem;

namespace SerapKeremZenCoreTools._Game.UISystem
{
    /// <summary>
    /// Manages the UI system by controlling the visibility of different panels.
    /// </summary>
    public class UIManager : MonoBehaviour
    {
        #region Variables

        private GameplayUI _gameplayUI;
        private SettingsUI _settingsUI;
        private FailUI _failUI;
        private WinUI _winUI;
        private PauseManager _pauseManager;

        #endregion

        #region Initialization

        /// <summary>
        /// Constructor method for dependency injection.
        /// </summary>
        /// <param name="gameplayUI">The GameplayUI instance to manage.</param>
        /// <param name="settingsUI">The SettingsUI instance to manage.</param>
        /// <param name="failUI">The FailUI instance to manage.</param>
        /// <param name="winUI">The WinUI instance to manage.</param>
        /// <param name="pauseManager">The PauseManager instance to handle game pause/resume functionality.</param>
        [Inject]
        private void Construct(
            GameplayUI gameplayUI,
            SettingsUI settingsUI,
            FailUI failUI,
            WinUI winUI,
            PauseManager pauseManager)
        {
            _gameplayUI = gameplayUI;
            _settingsUI = settingsUI;
            _failUI = failUI;
            _winUI = winUI;
            _pauseManager = pauseManager;
        }

        /// <summary>
        /// Initializes the component by showing the default gameplay UI.
        /// </summary>
        private void Start()
        {
            ShowGameplayUI();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Shows the gameplay UI and hides all other panels.
        /// </summary>
        public void ShowGameplayUI()
        {
            HideAllPanels();
            _gameplayUI.gameObject.SetActive(true);
        }

        /// <summary>
        /// Shows the settings panel and pauses the game.
        /// </summary>
        public void ShowSettingsPanel()
        {
            _pauseManager.PauseGame();
            _settingsUI.gameObject.SetActive(true);
        }

        /// <summary>
        /// Shows the fail panel and pauses the game.
        /// </summary>
        public void ShowFailPanel()
        {
            _pauseManager.PauseGame();
            _failUI.gameObject.SetActive(true);
        }

        /// <summary>
        /// Shows the win panel and pauses the game.
        /// </summary>
        public void ShowWinPanel()
        {
            _pauseManager.PauseGame();
            _winUI.gameObject.SetActive(true);
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Hides all UI panels.
        /// </summary>
        private void HideAllPanels()
        {
            if (_gameplayUI != null) _gameplayUI.gameObject.SetActive(false);
            if (_settingsUI != null) _settingsUI.gameObject.SetActive(false);
            if (_failUI != null) _failUI.gameObject.SetActive(false);
            if (_winUI != null) _winUI.gameObject.SetActive(false);
        }

        #endregion
    }
}