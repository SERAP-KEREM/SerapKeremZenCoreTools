using UnityEngine;
using UnityEngine.UI;
using TriInspector;
using Zenject;
using SerapKeremZenCoreTools._Game.PauseSystem;

namespace SerapKeremZenCoreTools._Game.UISystem
{
    /// <summary>
    /// Manages the Fail UI panel, including a button for restarting the level.
    /// </summary>
    public class FailUI : MonoBehaviour
    {
        #region Variables

        [Group("UI Elements")]
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
            if (_restartButton != null)
            {
                _restartButton.onClick.AddListener(OnRestartClicked);
            }
            else
            {
                Debug.LogError("[FailUI] Restart Button is not assigned.");
            }
        }

        #endregion

        #region Button Handlers

        /// <summary>
        /// Handles the click event for the "Restart" button.
        /// Resumes the game and hides the Fail UI panel.
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
            if (_restartButton != null)
            {
                _restartButton.onClick.RemoveListener(OnRestartClicked);
            }
        }

        #endregion
    }
}