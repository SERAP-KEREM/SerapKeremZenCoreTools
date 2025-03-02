using UnityEngine;
using Zenject;

namespace SerapKeremZenCoreTools._Game.InputSystem
{
    /// <summary>
    /// Installer class for setting up the InputSystem components using Zenject.
    /// </summary>
    public class InputSystemInstaller : MonoInstaller
    {
        #region Variables

        [SerializeField]
        [Tooltip("Prefab of the PlayerInput to be instantiated.")]
        private PlayerInput _playerInputPrefab;

        [SerializeField]
        [Tooltip("Instance of the Selector to be bound.")]
        private Selector _selector;

        #endregion

        #region Zenject Installation

        /// <summary>
        /// Installs the bindings for the PlayerInput and Selector systems.
        /// </summary>
        public override void InstallBindings()
        {
            InstallPlayerInput();
            InstallSelector();
        }

        /// <summary>
        /// Binds the PlayerInput system to the dependency injection container.
        /// </summary>
        private void InstallPlayerInput()
        {
            Container
                .Bind<PlayerInput>()
                .FromComponentInNewPrefab(_playerInputPrefab)
                .AsSingle()
                .NonLazy();
        }

        /// <summary>
        /// Binds the Selector system to the dependency injection container.
        /// </summary>
        private void InstallSelector()
        {
            Container
                .Bind<Selector>()
                .FromInstance(_selector)
                .AsSingle();
        }

        #endregion
    }
}