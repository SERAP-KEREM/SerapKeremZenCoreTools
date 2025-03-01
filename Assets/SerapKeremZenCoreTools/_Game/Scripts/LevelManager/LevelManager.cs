using UnityEngine;
using Zenject;
using UnityEngine.Events;
using SerapKeremZenCoreTools._Game.UISystem;
using SerapKeremZenCoreTools._Game.AudioSystem;

namespace SerapKeremZenCoreTools._Game.LevelSystem
{
    /// <summary>
    /// Manages the game's level lifecycle, including starting, completing, and failing levels.
    /// </summary>
    public class LevelManager : MonoBehaviour
    {
        #region Variables

        private UIManager _uiManager;
        private AudioManager _audioManager;

        /// <summary>
        /// Event triggered when the level starts.
        /// </summary>
        public UnityEvent OnLevelStarted = new UnityEvent();

        /// <summary>
        /// Event triggered when the level is completed successfully.
        /// </summary>
        public UnityEvent OnLevelCompleted = new UnityEvent();

        /// <summary>
        /// Event triggered when the level fails.
        /// </summary>
        public UnityEvent OnLevelFailed = new UnityEvent();

        [SerializeField]
        [Tooltip("The name of the background music to play during the level.")]
        private string _backgroundMusic = "BackgroundMusic";

        [SerializeField]
        [Tooltip("The name of the sound to play when the level is completed.")]
        private string _levelComplete = "LevelComplete";

        [SerializeField]
        [Tooltip("The name of the sound to play when the level fails.")]
        private string _levelFail = "LevelFail";

        #endregion

        #region Initialization

        /// <summary>
        /// Constructor method for dependency injection.
        /// </summary>
        /// <param name="uiManager">The UIManager instance to handle UI-related functionality.</param>
        /// <param name="audioManager">The AudioManager instance to handle audio-related functionality.</param>
        [Inject]
        private void Construct(UIManager uiManager, AudioManager audioManager)
        {
            _uiManager = uiManager;
            _audioManager = audioManager;
        }

        /// <summary>
        /// Initializes the component by starting the level.
        /// </summary>
        private void Start()
        {
            StartLevel();
        }

        /// <summary>
        /// Starts the level by showing the gameplay UI and playing the background music.
        /// </summary>
        public void StartLevel()
        {
            _uiManager.ShowGameplayUI();
            _audioManager.PlaySound(_backgroundMusic);
            OnLevelStarted?.Invoke();
        }

        #endregion

        #region Level Management

        /// <summary>
        /// Completes the level by showing the win panel and playing the level complete sound.
        /// </summary>
        public void CompleteLevel()
        {
            _audioManager.PlaySound(_levelComplete);
            _uiManager.ShowWinPanel();
            OnLevelCompleted?.Invoke();
        }

        /// <summary>
        /// Fails the level by showing the fail panel and playing the level fail sound.
        /// </summary>
        public void FailLevel()
        {
            _audioManager.PlaySound(_levelFail);
            _uiManager.ShowFailPanel();
            OnLevelFailed?.Invoke();
        }

        /// <summary>
        /// Restarts the level by calling the StartLevel method.
        /// </summary>
        public void RestartLevel()
        {
            StartLevel();
        }

        #endregion

        #region Cleanup

        /// <summary>
        /// Cleans up event listeners when the object is destroyed.
        /// </summary>
        private void OnDestroy()
        {
            OnLevelStarted.RemoveAllListeners();
            OnLevelCompleted.RemoveAllListeners();
            OnLevelFailed.RemoveAllListeners();
        }

        #endregion
    }
}