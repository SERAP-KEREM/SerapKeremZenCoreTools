using SerapKeremZenCoreTools._Game.InputSystem;
using UnityEngine;
using Zenject;

namespace SerapKeremZenCoreTools._Game.PopUpSystem
{
    /// <summary>
    /// Installer class for setting up the Pop-Up System using Zenject.
    /// </summary>
    public class PopUpSystemInstaller : MonoInstaller
    {
        #region Variables

        [SerializeField]
        [Tooltip("The instance of PopUpTextManager to bind.")]
        private PopUpTextManager popUpTextManager;

        [SerializeField]
        [Tooltip("The instance of PopUpIconManager to bind.")]
        private PopUpIconManager popUpIconManager;

        [SerializeField]
        [Tooltip("Prefab of the PlayerInput to be instantiated.")]
        private PlayerInput playerInputPrefab;

        #endregion

        #region Zenject Installation

        /// <summary>
        /// Installs the bindings for the Pop-Up System and Input System.
        /// </summary>
        public override void InstallBindings()
        {
            BindPopUpTextManager();
            BindPopUpIconManager();
            BindPlayerInput();
        }

        /// <summary>
        /// Binds the PopUpTextManager system to the dependency injection container.
        /// </summary>
        private void BindPopUpTextManager()
        {
            Container
                .Bind<PopUpTextManager>()
                .FromInstance(popUpTextManager)
                .AsSingle()
                .NonLazy();
        }

        /// <summary>
        /// Binds the PopUpIconManager system to the dependency injection container.
        /// </summary>
        private void BindPopUpIconManager()
        {
            Container
                .Bind<PopUpIconManager>()
                .FromInstance(popUpIconManager)
                .AsSingle()
                .NonLazy();
        }

        /// <summary>
        /// Binds the PlayerInput system to the dependency injection container.
        /// </summary>
        private void BindPlayerInput()
        {
            Container
                .Bind<PlayerInput>()
                .FromComponentInNewPrefab(playerInputPrefab)
                .AsSingle()
                .NonLazy();
        }

        #endregion
    }
}