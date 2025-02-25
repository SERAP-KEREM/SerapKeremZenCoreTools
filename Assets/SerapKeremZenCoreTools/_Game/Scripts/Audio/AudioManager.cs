using UnityEngine;
using System.Collections.Generic;
using TriInspector;
using Zenject;
using System.Linq;

namespace SerapKeremZenCoreTools._Game.AudioSystem
{
    /// <summary>
    /// Manages audio playback, including playing, stopping, and muting sounds.
    /// </summary>
    public class AudioManager : MonoBehaviour, IInitializable
    {
        #region Variables

        [SerializeField, Required]
        [Group("Audio Settings")]
        private List<Audio> _audioList = new();

        [Group("Audio Settings")]
        [SerializeField]
        private int _maxAudioSources = 10;

        [Group("Audio Settings")]
        [ShowInInspector, ReadOnly]
        private bool _isAudioMuted;

        private List<AudioSource> _audioSources;
        private const string AUDIO_MUTE_KEY = "AudioMuted";

        #endregion

        #region Initialization

        /// <summary>
        /// Constructor method for dependency injection.
        /// </summary>
        [Inject]
        private void Construct()
        {
            Initialize();
        }

        /// <summary>
        /// Initializes the AudioManager by setting up audio sources and loading mute state.
        /// </summary>
        public void Initialize()
        {
            _audioSources = new List<AudioSource>();
            _isAudioMuted = PlayerPrefs.GetInt(AUDIO_MUTE_KEY, 0) == 1;

            // Create initial audio sources
            for (int i = 0; i < _maxAudioSources; i++)
            {
                CreateNewAudioSource();
            }
        }

        /// <summary>
        /// Creates a new AudioSource and adds it to the pool.
        /// </summary>
        /// <returns>The newly created AudioSource.</returns>
        private AudioSource CreateNewAudioSource()
        {
            var audioSource = gameObject.AddComponent<AudioSource>();
            _audioSources.Add(audioSource);
            return audioSource;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Plays a sound by its name.
        /// </summary>
        /// <param name="audioName">The name of the audio to play.</param>
        public void PlaySound(string audioName)
        {
            var audio = _audioList.FirstOrDefault(a => a.Name == audioName);
            if (audio == null)
            {
                Debug.LogWarning($"Audio with name {audioName} not found!");
                return;
            }

            var availableSource = GetAvailableAudioSource();
            if (availableSource == null)
                return;

            var clip = audio.GetRandomClip();
            if (clip == null)
                return;

            availableSource.clip = clip;
            availableSource.volume = audio.Volume * (IsAudioMuted ? 0 : 1);
            availableSource.pitch = audio.Pitch;
            availableSource.loop = audio.Loop;
            availableSource.Play();
        }

        /// <summary>
        /// Stops a specific sound by its name.
        /// </summary>
        /// <param name="audioName">The name of the audio to stop.</param>
        public void StopSound(string audioName)
        {
            var playingSources = _audioSources.Where(source =>
                source.isPlaying && source.clip != null &&
                _audioList.Any(audio =>
                    audio.Name == audioName &&
                    audio.Clips.Contains(source.clip)));

            foreach (var source in playingSources)
            {
                source.Stop();
            }
        }

        /// <summary>
        /// Stops all currently playing sounds.
        /// </summary>
        public void StopAllSounds()
        {
            foreach (var source in _audioSources)
            {
                source.Stop();
            }
        }

        /// <summary>
        /// Sets the volume of a specific sound.
        /// </summary>
        /// <param name="audioName">The name of the audio.</param>
        /// <param name="volume">The new volume level (0 to 1).</param>
        public void SetVolume(string audioName, float volume)
        {
            var audio = _audioList.FirstOrDefault(a => a.Name == audioName);
            if (audio == null)
                return;

            var playingSources = _audioSources.Where(source =>
                source.isPlaying && source.clip != null &&
                audio.Clips.Contains(source.clip));

            foreach (var source in playingSources)
            {
                source.volume = volume * (IsAudioMuted ? 0 : 1);
            }
        }

        #endregion

        #region Mute Management

        /// <summary>
        /// Gets or sets whether the audio is muted.
        /// </summary>
        public bool IsAudioMuted
        {
            get => _isAudioMuted;
            set
            {
                _isAudioMuted = value;
                PlayerPrefs.SetInt(AUDIO_MUTE_KEY, _isAudioMuted ? 1 : 0);
                PlayerPrefs.Save();

                // Update all playing audio sources
                foreach (var source in _audioSources)
                {
                    if (source.isPlaying)
                    {
                        source.volume = source.volume * (_isAudioMuted ? 0 : 1);
                    }
                }
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Finds an available AudioSource from the pool.
        /// </summary>
        /// <returns>An available AudioSource or null if none are available.</returns>
        private AudioSource GetAvailableAudioSource()
        {
            var availableSource = _audioSources.FirstOrDefault(source => !source.isPlaying);
            if (availableSource == null && _audioSources.Count < _maxAudioSources)
            {
                availableSource = CreateNewAudioSource();
            }
            return availableSource;
        }

        #endregion
    }
}