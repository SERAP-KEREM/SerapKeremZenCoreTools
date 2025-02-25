using UnityEngine;
using TriInspector;
using System.Collections.Generic;
using System;

namespace SerapKeremZenCoreTools._Game.ParticleEffectSystem
{
    /// <summary>
    /// Represents the data for a particle effect, including its name, count, and associated particle systems.
    /// </summary>
    [Serializable]
    public class ParticleData
    {
        #region Variables

        [Group("Particle Settings")]
        [PropertyTooltip("The name of the particle effect.")]
        [SerializeField]
        private string _particleName;

        [Group("Particle Settings")]
        [PropertyTooltip("The number of particles to be generated.")]
        [SerializeField]
        private int _particleCount;

        [Group("Particle Settings")]
        [PropertyTooltip("The list of particle system prefabs.")]
        [SerializeField]
        private List<ParticleSystem> _particleSystemList;

        #endregion

        #region Properties

        /// <summary>
        /// The name of the particle effect.
        /// </summary>
        public string ParticleName => _particleName;

        /// <summary>
        /// The number of particles to be generated.
        /// </summary>
        public int ParticleCount => _particleCount;

        /// <summary>
        /// The list of particle system prefabs associated with this particle effect.
        /// </summary>
        public List<ParticleSystem> ParticleSystemList => _particleSystemList;

        #endregion

        #region Public Methods

        /// <summary>
        /// Returns a random particle system from the list of particle systems.
        /// </summary>
        /// <returns>A random particle system or null if the list is empty.</returns>
        public ParticleSystem GetRandomParticleSystem()
        {
            if (_particleSystemList == null || _particleSystemList.Count == 0)
            {
                Debug.LogWarning("ParticleData: No particle systems available.");
                return null;
            }

            return _particleSystemList[UnityEngine.Random.Range(0, _particleSystemList.Count)];
        }

        #endregion
    }
}