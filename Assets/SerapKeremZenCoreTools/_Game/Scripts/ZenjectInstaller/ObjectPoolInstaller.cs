using UnityEngine;
using Zenject;

namespace SerapKeremZenCoreTools._Game.ObjectPool
{
    /// <summary>
    /// Installer class for setting up the ObjectPoolManager using Zenject.
    /// </summary>
    public class ObjectPoolInstaller : MonoInstaller
    {
        #region Variables

        [SerializeField]
        [Tooltip("Prefab of the ObjectPoolManager to be instantiated.")]
        private ObjectPoolManager poolManagerPrefab;

        #endregion

        #region Zenject Installation

        /// <summary>
        /// Installs the bindings for the ObjectPoolManager system.
        /// </summary>
        public override void InstallBindings()
        {
            BindObjectPoolManager();
        }

        /// <summary>
        /// Binds the ObjectPoolManager system to the dependency injection container.
        /// </summary>
        private void BindObjectPoolManager()
        {
            Container
                .Bind<ObjectPoolManager>()
                .FromComponentInNewPrefab(poolManagerPrefab)
                .AsSingle()
                .NonLazy();
        }

        #endregion
    }
}