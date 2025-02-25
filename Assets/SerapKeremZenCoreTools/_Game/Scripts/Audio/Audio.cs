using UnityEngine;
using TriInspector;
using System;

namespace SerapKeremZenCoreTools._Game.AudioSystem
{
    /// <summary>
    /// Represents an audio configuration with settings such as clips, volume, pitch, and loop.
    /// </summary>
    [Serializable]
    public class Audio
    {
        #region Variables

        [Required]
        [SerializeField]
        private string _name;

        [Group("Audio Settings")]
        [SerializeField]
        private AudioClip[] _clips;

        [Group("Audio Settings")]
        [Range(0f, 1f)]
        [SerializeField]
        private float _volume = 1f;

        [Group("Audio Settings")]
        [Range(0.1f, 3f)]
        [SerializeField]
        private float _pitch = 1f;

        [Group("Audio Settings")]
        [SerializeField]
        private bool _loop;

        #endregion

        #region Properties

        /// <summary>
        /// The name of the audio configuration.
        /// </summary>
        public string Name => _name;

        /// <summary>
        /// The array of audio clips associated with this configuration.
        /// </summary>
        public AudioClip[] Clips => _clips;

        /// <summary>
        /// The volume level of the audio (range: 0 to 1).
        /// </summary>
        public float Volume => _volume;

        /// <summary>
        /// The pitch level of the audio (range: 0.1 to 3).
        /// </summary>
        public float Pitch => _pitch;

        /// <summary>
        /// Indicates whether the audio should loop.
        /// </summary>
        public bool Loop => _loop;

        #endregion

        #region Public Methods

        /// <summary>
        /// Returns a random audio clip from the available clips.
        /// </summary>
        /// <returns>A random audio clip or null if no clips are available.</returns>
        public AudioClip GetRandomClip()
        {
            if (_clips == null || _clips.Length == 0)
            {
                Debug.LogWarning("Audio: No clips available.");
                return null;
            }

            return _clips[UnityEngine.Random.Range(0, _clips.Length)];
        }

        #endregion
    }
}