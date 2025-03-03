using UnityEngine;
using System.Collections.Generic;
using TriInspector;
using Zenject;
using System.Linq;
<<<<<<< Updated upstream
=======
using SerapKeremZenCoreTools._Game.SaveLoadSystem;
using UnityEngine.Audio;
>>>>>>> Stashed changes

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

        [Group("Audio Mixer")]
        [SerializeField, Tooltip("The Audio Mixer for managing sound groups.")]
        private AudioMixer _audioMixer;

        [Group("Audio Mixer Parameters")]
        [SerializeField, Tooltip("Parameter name for master volume.")]
        private string _masterVolumeParam = "Master";

        [SerializeField, Tooltip("Parameter name for music volume.")]
        private string _musicVolumeParam = "MusicVolume";

        [SerializeField, Tooltip("Parameter name for SFX volume.")]
        private string _sfxVolumeParam = "SfxVolume";

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

            SetVolume("Master", 1f);
            SetVolume("Music", 0.8f);
            SetVolume("Sfx", 1f); 

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
        /// Sets the volume of a specific sound group using the Audio Mixer.
        /// </summary>
        /// <param name="volumeType">The type of volume to adjust (e.g., Master, Music, Sfx).</param>
        /// <param name="volume">The new volume level (0 to 1).</param>
        public void SetVolume(string volumeType, float volume)
        {
            if (_audioMixer == null)
            {
                Debug.LogWarning("AudioMixer is not assigned.");
                return;
            }

            string parameterName = volumeType switch
            {
                "Master" => _masterVolumeParam,
                "Music" => _musicVolumeParam,
                "Sfx" => _sfxVolumeParam,
                _ => null
            };

            if (parameterName != null)
            {
                _audioMixer.SetFloat(parameterName, Mathf.Log10(volume) * 20); // Linear to decibels conversion
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
<<<<<<< Updated upstream
                PlayerPrefs.SetInt(AUDIO_MUTE_KEY, _isAudioMuted ? 1 : 0);
                PlayerPrefs.Save();
=======
                _saveManager.SaveData<int>(AUDIO_MUTE_KEY, _isAudioMuted ? 1 : 0);
>>>>>>> Stashed changes

                // Update all playing audio sources
                foreach (var source in _audioSources)
                {
                    if (source.isPlaying)
                    {
                        source.volume = source.volume * (_isAudioMuted ? 0 : 1);
                    }
                }

                // Update Audio Mixer master volume
                if (_audioMixer != null)
                {
                    _audioMixer.SetFloat(_masterVolumeParam, _isAudioMuted ? -80f : 0f); // -80f is effectively silent
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