using UnityEngine;
using UnityEngine.UI;
using TriInspector;
using Zenject;
using SerapKeremZenCoreTools._Game.PauseSystem;

namespace SerapKeremZenCoreTools._Game.UISystem
{
    /// <summary>
    /// Manages the Win UI panel, including buttons for restarting or proceeding to the next level.
    /// </summary>
    public class WinUI : MonoBehaviour
    {
        #region Variables

        [Group("UI Elements")]
        [SerializeField]
        [Tooltip("The button used to proceed to the next level.")]
        private Button _nextLevelButton;

        [SerializeField]
        [Tooltip("The button used to restart the current level.")]
        private Button _restartButton;

        private PauseManager _pauseManager;

        #endregion

        #region Initialization

        /// <summary>
        /// Constructor method for dependency injection.
        /// </summary>
        /// <param name="pauseManager">The PauseManager instance to handle game pause/resume functionality.</param>
        [Inject]
        private void Construct(PauseManager pauseManager)
        {
            _pauseManager = pauseManager;
        }

        /// <summary>
        /// Initializes the component by setting up UI event listeners.
        /// </summary>
        private void Start()
        {
            InitializeUI();
        }

        /// <summary>
        /// Sets up event listeners for the UI buttons.
        /// </summary>
        private void InitializeUI()
        {
            if (_nextLevelButton != null)
            {
                _nextLevelButton.onClick.AddListener(OnNextLevelClicked);
            }
            else
            {
                Debug.LogError("[WinUI] Next Level Button is not assigned.");
            }

            if (_restartButton != null)
            {
                _restartButton.onClick.AddListener(OnRestartClicked);
            }
            else
            {
                Debug.LogError("[WinUI] Restart Button is not assigned.");
            }
        }

        #endregion

        #region Button Handlers

        /// <summary>
        /// Handles the click event for the "Next Level" button.
        /// Resumes the game and hides the Win UI panel.
        /// </summary>
        private void OnNextLevelClicked()
        {
            _pauseManager.ResumeGame();
            gameObject.SetActive(false);
        }

        /// <summary>
        /// Handles the click event for the "Restart" button.
        /// Resumes the game and hides the Win UI panel.
        /// </summary>
        private void OnRestartClicked()
        {
            _pauseManager.ResumeGame();
            gameObject.SetActive(false);
        }

        #endregion

        #region Cleanup

        /// <summary>
        /// Cleans up event listeners when the object is destroyed.
        /// </summary>
        private void OnDestroy()
        {
            if (_nextLevelButton != null)
            {
                _nextLevelButton.onClick.RemoveListener(OnNextLevelClicked);
            }

            if (_restartButton != null)
            {
                _restartButton.onClick.RemoveListener(OnRestartClicked);
            }
        }

        #endregion
    }
}