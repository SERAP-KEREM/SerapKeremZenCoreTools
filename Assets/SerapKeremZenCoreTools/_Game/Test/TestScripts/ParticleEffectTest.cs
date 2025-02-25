using UnityEngine;
using UnityEngine.UI;
using Zenject;
using TriInspector;
using TMPro;

namespace SerapKeremZenCoreTools._Game.ParticleEffectSystem
{
    /// <summary>
    /// A test class for the ParticleEffectManager to verify its functionality in a test scene.
    /// </summary>
    public class ParticleEffectTest : MonoBehaviour
    {
        #region Variables

        [Group("Test Settings")]
        [SerializeField]
        [Tooltip("The name of the particle effect to test.")]
        private string _testParticleName = "TestEffect";

        [Group("Test Settings")]
        [SerializeField]
        [Tooltip("The spawn point for the particle effect.")]
        private Transform _spawnPoint;

        [Group("UI References")]
        [Required]
        [SerializeField]
        private Button _playButton;

        [Required]
        [SerializeField]
        private Button _stopAllButton;

        [Required]
        [SerializeField]
        private TextMeshProUGUI _activeCountText;

        private ParticleEffectManager _particleManager;

        #endregion

        #region Initialization

        /// <summary>
        /// Constructor method for dependency injection.
        /// </summary>
        /// <param name="particleManager">The ParticleEffectManager instance to be tested.</param>
        [Inject]
        private void Construct(ParticleEffectManager particleManager)
        {
            _particleManager = particleManager;
        }

        /// <summary>
        /// Initializes the test by setting up UI and updating the active particle count.
        /// </summary>
        private void Start()
        {
            SetupUI();
            UpdateActiveCount();
        }

        #endregion

        #region UI Setup

        /// <summary>
        /// Sets up button listeners for testing particle effects.
        /// </summary>
        private void SetupUI()
        {
            _playButton.onClick.AddListener(PlayTestParticle);
            _stopAllButton.onClick.AddListener(StopAllParticles);
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Plays the test particle effect at the specified spawn point.
        /// </summary>
        private void PlayTestParticle()
        {
            Vector3 spawnPosition = _spawnPoint ? _spawnPoint.position : Vector3.zero;
            _particleManager.PlayParticle(_testParticleName, spawnPosition);
            UpdateActiveCount();
        }

        /// <summary>
        /// Stops all active particle effects and updates the active count display.
        /// </summary>
        private void StopAllParticles()
        {
            _particleManager.StopAllParticles();
            UpdateActiveCount();
        }

        #endregion

        #region Utility Methods

        /// <summary>
        /// Updates the UI text to display the current number of active particles.
        /// </summary>
        private void UpdateActiveCount()
        {
            _activeCountText.text = $"Active Particles: {_particleManager.GetActiveParticleCount()}";
        }

        #endregion

        #region Cleanup

        /// <summary>
        /// Cleans up button listeners when the object is destroyed.
        /// </summary>
        private void OnDestroy()
        {
            if (_playButton) _playButton.onClick.RemoveListener(PlayTestParticle);
            if (_stopAllButton) _stopAllButton.onClick.RemoveListener(StopAllParticles);
        }

        #endregion
    }
}