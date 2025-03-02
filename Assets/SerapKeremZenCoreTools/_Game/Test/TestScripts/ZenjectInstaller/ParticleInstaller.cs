using SerapKeremZenCoreTools._Game.ParticleEffectSystem;
using UnityEngine;
using Zenject;

namespace SerapKeremZenCoreTools._Game.ParticleEffectInstaller
{
    /// <summary>
    /// Installer class for setting up the ParticleEffectManager system using Zenject.
    /// </summary>
    public class ParticleInstaller : MonoInstaller
    {
        #region Variables

        [SerializeField]
        [Tooltip("The prefab or instance of the ParticleEffectManager to be installed.")]
        private ParticleEffectManager _particleManager;

        #endregion

        #region Zenject Installation

        /// <summary>
        /// Installs the bindings for the ParticleEffectManager system.
        /// </summary>
        public override void InstallBindings()
        {
            InstallParticleEffectSystem();
        }

        /// <summary>
        /// Binds the ParticleEffectManager system to the dependency injection container.
        /// </summary>
        private void InstallParticleEffectSystem()
        {
            Container
                .Bind<ParticleEffectManager>()
                .FromInstance(_particleManager)
                .AsSingle();
        }

        #endregion
    }
}