using UnityEngine;
using UnityEngine.UI;
using TriInspector;
using Zenject;
using SerapKeremZenCoreTools._Game.UISystem;
using SerapKeremZenCoreTools._Game.LevelSystem;

namespace SerapKeremZenCoreTools.Tests
{
    /// <summary>
    /// A tester class for simulating win and fail scenarios in the game.
    /// </summary>
    public class GameSystemsTester : MonoBehaviour
    {
        #region Variables

        [Group("Test Buttons")]
        [SerializeField]
        [Tooltip("The button used to simulate a win scenario.")]
        private Button winButton;

        [SerializeField]
        [Tooltip("The button used to simulate a fail scenario.")]
        private Button failButton;

        private LevelManager _levelManager;

        #endregion

        #region Initialization

        /// <summary>
        /// Constructor method for dependency injection.
        /// </summary>
        /// <param name="uiManager">The UIManager instance to handle UI-related functionality.</param>
        [Inject]
        private void Construct(UIManager uiManager, LevelManager levelManager)
        {
            _levelManager = levelManager;
        }

        /// <summary>
        /// Initializes the component by setting up button event listeners.
        /// </summary>
        private void Start()
        {
            InitializeButtons();
        }

        /// <summary>
        /// Sets up event listeners for the test buttons.
        /// </summary>
        private void InitializeButtons()
        {
            if (winButton != null)
            {
                winButton.onClick.AddListener(OnWinButtonClicked);
            }
            else
            {
                Debug.LogError("[GameSystemsTester] Win Button is not assigned.");
            }

            if (failButton != null)
            {
                failButton.onClick.AddListener(OnFailButtonClicked);
            }
            else
            {
                Debug.LogError("[GameSystemsTester] Fail Button is not assigned.");
            }
        }

        #endregion

        #region Button Handlers

        /// <summary>
        /// Handles the click event for the "Win" button.
        /// Displays the win panel using the UIManager.
        /// </summary>
        private void OnWinButtonClicked()
        {
            _levelManager.CompleteLevel();
        }

        /// <summary>
        /// Handles the click event for the "Fail" button.
        /// Displays the fail panel using the UIManager.
        /// </summary>
        private void OnFailButtonClicked()
        {
            _levelManager.FailLevel();
        }

        #endregion

        #region Cleanup

        /// <summary>
        /// Cleans up event listeners when the object is destroyed.
        /// </summary>
        private void OnDestroy()
        {
            if (winButton != null)
            {
                winButton.onClick.RemoveListener(OnWinButtonClicked);
            }

            if (failButton != null)
            {
                failButton.onClick.RemoveListener(OnFailButtonClicked);
            }
        }

        #endregion
    }
}