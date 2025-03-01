using UnityEngine;
using UnityEngine.UI;
using TMPro;
using TriInspector;
using Zenject;
using SerapKeremZenCoreTools._Game.AudioSystem;
using SerapKeremZenCoreTools._Game.PauseSystem;
using SerapKeremZenCoreTools._Game.SaveLoadSystem;

namespace SerapKeremZenCoreTools._Game.UISystem
{
    /// <summary>
    /// Manages the Settings UI panel, including buttons for resuming, restarting, and adjusting music volume.
    /// </summary>
    public class SettingsUI : MonoBehaviour
    {
        #region Variables

        [Group("UI Elements")]
        [SerializeField]
        [Tooltip("The button used to resume the game.")]
        private Button _resumeButton;

        [SerializeField]
        [Tooltip("The button used to restart the current level.")]
        private Button _restartButton;

        [SerializeField]
        [Tooltip("The button used to close the settings panel.")]
        private Button _closeButton;

        [SerializeField]
        [Tooltip("The slider used to adjust the music volume.")]
        private Slider _musicSlider;

        [SerializeField]
        [Tooltip("The text displaying the current music volume percentage.")]
        private TextMeshProUGUI _musicValueText;

        private AudioManager _audioManager;
        private PauseManager _pauseManager;
        private SaveManager _saveManager;
        private LoadManager _loadManager;

        private const string MUSIC_VOLUME_KEY = "MusicVolume";
        private const float DEFAULT_VOLUME = 1f;

        #endregion

        #region Initialization

        /// <summary>
        /// Constructor method for dependency injection.
        /// </summary>
        /// <param name="audioManager">The AudioManager instance to handle audio-related functionality.</param>
        /// <param name="pauseManager">The PauseManager instance to handle game pause/resume functionality.</param>
        /// <param name="saveManager">The SaveManager instance to handle saving data.</param>
        /// <param name="loadManager">The LoadManager instance to handle loading data.</param>
        [Inject]
        private void Construct(
            AudioManager audioManager,
            PauseManager pauseManager,
            SaveManager saveManager,
            LoadManager loadManager)
        {
            _audioManager = audioManager;
            _pauseManager = pauseManager;
            _saveManager = saveManager;
            _loadManager = loadManager;
        }

        /// <summary>
        /// Initializes the component by setting up UI event listeners and loading saved settings.
        /// </summary>
        private void Start()
        {
            InitializeUI();
        }

        /// <summary>
        /// Sets up event listeners for the UI elements and loads saved music volume settings.
        /// </summary>
        private void InitializeUI()
        {
            if (_resumeButton != null)
            {
                _resumeButton.onClick.AddListener(OnResumeClicked);
            }
            else
            {
                Debug.LogError("[SettingsUI] Resume Button is not assigned.");
            }

            if (_restartButton != null)
            {
                _restartButton.onClick.AddListener(OnRestartClicked);
            }
            else
            {
                Debug.LogError("[SettingsUI] Restart Button is not assigned.");
            }

            if (_closeButton != null)
            {
                _closeButton.onClick.AddListener(OnCloseClicked);
            }
            else
            {
                Debug.LogError("[SettingsUI] Close Button is not assigned.");
            }

            if (_musicSlider != null)
            {
                _musicSlider.onValueChanged.AddListener(OnMusicVolumeChanged);
            }
            else
            {
                Debug.LogError("[SettingsUI] Music Slider is not assigned.");
            }

            // Load saved music volume or use default value
            float savedVolume = _loadManager.LoadData(MUSIC_VOLUME_KEY, DEFAULT_VOLUME);
            _musicSlider.value = savedVolume;
            _audioManager.SetVolume("BackgroundMusic", savedVolume);
            UpdateMusicUI();
        }

        #endregion

        #region Button Handlers

        /// <summary>
        /// Handles the click event for the "Resume" button.
        /// Resumes the game and hides the settings panel.
        /// </summary>
        private void OnResumeClicked()
        {
            _pauseManager.ResumeGame();
            gameObject.SetActive(false);
        }

        /// <summary>
        /// Handles the click event for the "Restart" button.
        /// Resumes the game and hides the settings panel.
        /// </summary>
        private void OnRestartClicked()
        {
            _pauseManager.ResumeGame();
            gameObject.SetActive(false);
        }

        /// <summary>
        /// Handles the click event for the "Close" button.
        /// Resumes the game and hides the settings panel.
        /// </summary>
        private void OnCloseClicked()
        {
            _pauseManager.ResumeGame();
            gameObject.SetActive(false);
        }

        #endregion

        #region Music Volume Handling

        /// <summary>
        /// Handles changes to the music volume slider.
        /// Updates the audio volume and saves the new value.
        /// </summary>
        /// <param name="value">The new music volume value.</param>
        private void OnMusicVolumeChanged(float value)
        {
            _audioManager.SetVolume("BackgroundMusic", value);
            _saveManager.SaveData(MUSIC_VOLUME_KEY, value);
            UpdateMusicUI();
        }

        /// <summary>
        /// Updates the music volume text to reflect the current slider value.
        /// </summary>
        private void UpdateMusicUI()
        {
            if (_musicValueText != null)
            {
                _musicValueText.text = $"{(int)(_musicSlider.value * 100)}%";
            }
            else
            {
                Debug.LogError("[SettingsUI] Music Value Text is not assigned.");
            }
        }

        #endregion

        #region Cleanup

        /// <summary>
        /// Cleans up event listeners when the object is destroyed.
        /// </summary>
        private void OnDestroy()
        {
            if (_resumeButton != null)
            {
                _resumeButton.onClick.RemoveListener(OnResumeClicked);
            }

            if (_restartButton != null)
            {
                _restartButton.onClick.RemoveListener(OnRestartClicked);
            }

            if (_closeButton != null)
            {
                _closeButton.onClick.RemoveListener(OnCloseClicked);
            }

            if (_musicSlider != null)
            {
                _musicSlider.onValueChanged.RemoveListener(OnMusicVolumeChanged);
            }
        }

        #endregion
    }
}