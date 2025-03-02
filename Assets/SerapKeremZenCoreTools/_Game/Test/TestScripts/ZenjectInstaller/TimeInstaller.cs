using SerapKeremZenCoreTools._Game.TimeSystem;
using UnityEngine;
using Zenject;

namespace SerapKeremZenCoreTools._Game._TimeInstaller
{
    /// <summary>
    /// Installer class for setting up the TimeManager system using Zenject.
    /// </summary>
    public class TimeInstaller : MonoInstaller
    {
        #region Variables

        [SerializeField, Tooltip("Prefab of the TimeManager to be instantiated.")]
        private TimeManager _timeManagerPrefab;

        #endregion

        #region Zenject Installation

        /// <summary>
        /// Installs the bindings for the TimeManager and SignalBus systems.
        /// </summary>
        public override void InstallBindings()
        {
            InstallTimeSystem();
            SignalBusInstaller.Install(Container);
        }

        /// <summary>
        /// Binds the TimeManager system to the dependency injection container.
        /// </summary>
        private void InstallTimeSystem()
        {
            Container
                .Bind<TimeManager>()
                .FromComponentInNewPrefab(_timeManagerPrefab)
                .AsSingle()
                .NonLazy();
        }

        #endregion
    }
}