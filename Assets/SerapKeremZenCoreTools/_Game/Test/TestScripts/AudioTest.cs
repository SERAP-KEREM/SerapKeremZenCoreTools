using UnityEngine;
using UnityEngine.UI;
using Zenject;
using SerapKeremZenCoreTools._Game.AudioSystem;
using TriInspector;
using TMPro;

namespace SerapKeremZenCoreTools._Game.Tests
{
    /// <summary>
    /// A test class for the AudioManager to verify its functionality in a test scene.
    /// </summary>
    public class AudioTest : MonoBehaviour
    {
        #region Variables

        [SerializeField, Required]
        private Button _playButton;

        [SerializeField, Required]
        private Button _stopButton;

        [SerializeField, Required]
        private Button _muteButton;

        [SerializeField, Required]
        private TextMeshProUGUI _muteButtonText;

        [SerializeField]
        private string _testAudioName = "TestSound";

        private AudioManager _audioManager;

        #endregion

        #region Initialization

        /// <summary>
        /// Constructor method for dependency injection.
        /// </summary>
        /// <param name="audioManager">The AudioManager instance to be tested.</param>
        [Inject]
        private void Construct(AudioManager audioManager)
        {
            _audioManager = audioManager;
        }

        /// <summary>
        /// Initializes the test by setting up buttons and updating UI.
        /// </summary>
        private void Start()
        {
            SetupButtons();
            UpdateMuteButtonText();
        }

        #endregion

        #region Button Setup

        /// <summary>
        /// Sets up button listeners for play, stop, and mute functionality.
        /// </summary>
        private void SetupButtons()
        {
            _playButton.onClick.AddListener(PlayTestSound);
            _stopButton.onClick.AddListener(StopTestSound);
            _muteButton.onClick.AddListener(ToggleMute);
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Plays the test sound using the AudioManager.
        /// </summary>
        private void PlayTestSound()
        {
            _audioManager.PlaySound(_testAudioName);
        }

        /// <summary>
        /// Stops the test sound using the AudioManager.
        /// </summary>
        private void StopTestSound()
        {
            _audioManager.StopSound(_testAudioName);
        }

        /// <summary>
        /// Toggles the mute state of the AudioManager and updates the UI.
        /// </summary>
        private void ToggleMute()
        {
            _audioManager.IsAudioMuted = !_audioManager.IsAudioMuted;
            UpdateMuteButtonText();
        }

        #endregion

        #region UI Updates

        /// <summary>
        /// Updates the mute button text based on the current mute state.
        /// </summary>
        private void UpdateMuteButtonText()
        {
            _muteButtonText.text = _audioManager.IsAudioMuted ? "Unmute" : "Mute";
        }

        #endregion

        #region Cleanup

        /// <summary>
        /// Cleans up button listeners when the object is destroyed.
        /// </summary>
        private void OnDestroy()
        {
            if (_playButton != null) _playButton.onClick.RemoveListener(PlayTestSound);
            if (_stopButton != null) _stopButton.onClick.RemoveListener(StopTestSound);
            if (_muteButton != null) _muteButton.onClick.RemoveListener(ToggleMute);
        }

        #endregion
    }
}