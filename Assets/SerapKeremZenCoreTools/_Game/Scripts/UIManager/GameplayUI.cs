using UnityEngine;
using UnityEngine.UI;
using TMPro;
using TriInspector;
using Zenject;

namespace SerapKeremZenCoreTools._Game.UISystem
{
    /// <summary>
    /// Manages the Gameplay UI panel, including elements like level text and settings button.
    /// </summary>
    public class GameplayUI : MonoBehaviour
    {
        #region Variables

        [Group("UI Elements")]
        [SerializeField]
        [Tooltip("The text displaying the current level.")]
        private TextMeshProUGUI _levelText;

        [SerializeField]
        [Tooltip("The button used to open the settings panel.")]
        private Button _settingsButton;

        private UIManager _uiManager;

        #endregion

        #region Initialization

        /// <summary>
        /// Constructor method for dependency injection.
        /// </summary>
        /// <param name="uiManager">The UIManager instance to handle UI-related functionality.</param>
        [Inject]
        private void Construct(UIManager uiManager)
        {
            _uiManager = uiManager;
        }

        /// <summary>
        /// Initializes the component by setting up UI event listeners.
        /// </summary>
        private void Start()
        {
            InitializeUI();
        }

        /// <summary>
        /// Sets up event listeners for the UI elements.
        /// </summary>
        private void InitializeUI()
        {
            if (_settingsButton != null)
            {
                _settingsButton.onClick.AddListener(OnSettingsButtonClicked);
            }
            else
            {
                Debug.LogError("[GameplayUI] Settings Button is not assigned.");
            }
        }

        #endregion

        #region Button Handlers

        /// <summary>
        /// Handles the click event for the "Settings" button.
        /// Opens the settings panel using the UIManager.
        /// </summary>
        private void OnSettingsButtonClicked()
        {
            _uiManager.ShowSettingsPanel();
        }

        #endregion

        #region Cleanup

        /// <summary>
        /// Cleans up event listeners when the object is destroyed.
        /// </summary>
        private void OnDestroy()
        {
            if (_settingsButton != null)
            {
                _settingsButton.onClick.RemoveListener(OnSettingsButtonClicked);
            }
        }

        #endregion
    }
}