using UnityEngine;
using System.Collections.Generic;
using TriInspector;
using Zenject;

namespace SerapKeremZenCoreTools._Game.ParticleEffectSystem
{
    /// <summary>
    /// Manages particle effects using an object pooling system for efficient performance.
    /// </summary>
    public class ParticleEffectManager : MonoBehaviour
    {
        #region Variables

        [SerializeField, Required]
        [PropertyTooltip("The list of particle data configurations.")]
        private List<ParticleData> _particleDataList = new();

        private Dictionary<string, Queue<ParticleSystem>> _particlePools;
        private List<ParticleSystem> _activeParticles;

        #endregion

        #region Initialization

        /// <summary>
        /// Initializes the particle pools and active particles list.
        /// </summary>
        private void Awake()
        {
            Initialize();
        }

        /// <summary>
        /// Sets up the particle pools and initializes all particle data configurations.
        /// </summary>
        private void Initialize()
        {
            _particlePools = new Dictionary<string, Queue<ParticleSystem>>();
            _activeParticles = new List<ParticleSystem>();

            foreach (var particleData in _particleDataList)
            {
                InitializePool(particleData);
            }
        }

        /// <summary>
        /// Initializes a pool for a specific particle data configuration.
        /// </summary>
        /// <param name="data">The particle data configuration to initialize.</param>
        private void InitializePool(ParticleData data)
        {
            var pool = new Queue<ParticleSystem>();

            for (int i = 0; i < data.ParticleCount; i++)
            {
                foreach (var prefab in data.ParticleSystemList)
                {
                    var particle = CreateParticle(prefab, data.ParticleName);
                    particle.gameObject.SetActive(false);
                    pool.Enqueue(particle);
                }
            }

            _particlePools[data.ParticleName] = pool;
        }

        #endregion

        #region Particle Management

        /// <summary>
        /// Creates a particle instance from a prefab and assigns it a unique name.
        /// </summary>
        /// <param name="prefab">The particle system prefab to instantiate.</param>
        /// <param name="name">The name of the particle effect.</param>
        /// <returns>The instantiated particle system.</returns>
        private ParticleSystem CreateParticle(ParticleSystem prefab, string name)
        {
            var particle = Instantiate(prefab, transform);
            particle.name = $"{name}_Particle";
            return particle;
        }

        /// <summary>
        /// Plays a particle effect at the specified position and rotation.
        /// </summary>
        /// <param name="particleName">The name of the particle effect to play.</param>
        /// <param name="position">The position to play the particle effect at.</param>
        /// <param name="rotation">The rotation to apply to the particle effect (default is Quaternion.identity).</param>
        /// <returns>The played particle system or null if the effect is not found.</returns>
        public ParticleSystem PlayParticle(string particleName, Vector3 position, Quaternion rotation = default)
        {
            if (!_particlePools.ContainsKey(particleName))
            {
                Debug.LogWarning($"Particle effect '{particleName}' not found!");
                return null;
            }

            var pool = _particlePools[particleName];
            ParticleSystem particle;

            if (pool.Count > 0)
            {
                particle = pool.Dequeue();
            }
            else
            {
                var data = _particleDataList.Find(x => x.ParticleName == particleName);
                var randomPrefab = data.ParticleSystemList[Random.Range(0, data.ParticleSystemList.Count)];
                particle = CreateParticle(randomPrefab, particleName);
            }

            particle.transform.position = position;
            particle.transform.rotation = rotation;
            particle.gameObject.SetActive(true);
            particle.Play();

            _activeParticles.Add(particle);
            return particle;
        }

        /// <summary>
        /// Stops a particle effect and returns it to the pool when finished.
        /// </summary>
        /// <param name="particle">The particle system to stop.</param>
        public void StopParticle(ParticleSystem particle)
        {
            if (particle == null) return;

            particle.Stop();
            StartCoroutine(ReturnToPoolWhenFinished(particle));
        }

        /// <summary>
        /// Returns a particle system to its pool after it finishes playing.
        /// </summary>
        /// <param name="particle">The particle system to return to the pool.</param>
        /// <returns>An IEnumerator for the coroutine.</returns>
        private System.Collections.IEnumerator ReturnToPoolWhenFinished(ParticleSystem particle)
        {
            yield return new WaitUntil(() => !particle.IsAlive());

            if (_activeParticles.Contains(particle))
            {
                _activeParticles.Remove(particle);
            }

            string particleName = particle.name.Replace("_Particle(Clone)", "");
            if (_particlePools.ContainsKey(particleName))
            {
                particle.gameObject.SetActive(false);
                _particlePools[particleName].Enqueue(particle);
            }
        }

        /// <summary>
        /// Stops all active particle effects and returns them to their pools.
        /// </summary>
        public void StopAllParticles()
        {
            foreach (var particle in _activeParticles.ToArray())
            {
                StopParticle(particle);
            }
        }

        #endregion

        #region Utility Methods

        /// <summary>
        /// Checks if a particle effect with the specified name exists in the pools.
        /// </summary>
        /// <param name="particleName">The name of the particle effect to check.</param>
        /// <returns>True if the particle effect exists; otherwise, false.</returns>
        public bool HasParticleEffect(string particleName)
        {
            return _particlePools.ContainsKey(particleName);
        }

        /// <summary>
        /// Gets the number of currently active particle effects.
        /// </summary>
        /// <returns>The count of active particles.</returns>
        public int GetActiveParticleCount()
        {
            return _activeParticles.Count;
        }

        #endregion
    }
}