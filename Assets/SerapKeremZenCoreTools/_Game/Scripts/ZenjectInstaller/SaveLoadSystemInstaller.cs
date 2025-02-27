using UnityEngine;
using Zenject;

namespace SerapKeremZenCoreTools._Game.SaveLoadSystem
{
    /// <summary>
    /// Installer class for setting up the SaveManager and LoadManager systems using Zenject.
    /// </summary>
    public class SaveLoadSystemInstaller : MonoInstaller
    {
        #region Variables

        [SerializeField]
        [Tooltip("Prefab of the SaveManager to be instantiated.")]
        private SaveManager _saveManagerPrefab;

        [SerializeField]
        [Tooltip("Prefab of the LoadManager to be instantiated.")]
        private LoadManager _loadManagerPrefab;

        #endregion

        #region Zenject Installation

        /// <summary>
        /// Installs the bindings for the SaveManager and LoadManager systems.
        /// </summary>
        public override void InstallBindings()
        {
            InstallSaveManager();
            InstallLoadManager();
        }

        /// <summary>
        /// Binds the SaveManager system to the dependency injection container.
        /// </summary>
        private void InstallSaveManager()
        {
            Container
                .Bind<SaveManager>()
                .FromComponentInNewPrefab(_saveManagerPrefab)
                .AsSingle()
                .NonLazy();
        }

        /// <summary>
        /// Binds the LoadManager system to the dependency injection container.
        /// </summary>
        private void InstallLoadManager()
        {
            Container
                .Bind<LoadManager>()
                .FromComponentInNewPrefab(_loadManagerPrefab)
                .AsSingle()
                .NonLazy();
        }

        #endregion
    }
}