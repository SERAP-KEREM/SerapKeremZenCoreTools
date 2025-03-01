using UnityEngine;
using Zenject;
using SerapKeremZenCoreTools._Game.AudioSystem;
using SerapKeremZenCoreTools._Game.PauseSystem;
using SerapKeremZenCoreTools._Game.SaveLoadSystem;
using SerapKeremZenCoreTools._Game.UISystem;
using TriInspector;
using SerapKeremZenCoreTools._Game.TimeSystem;
using SerapKeremZenCoreTools._Game.LevelSystem;

namespace SerapKeremZenCoreTools.Tests
{
    /// <summary>
    /// Installer class for setting up the game systems using Zenject.
    /// </summary>
    public class GameSystemsInstaller : MonoInstaller
    {
        #region Variables

        [Group("Core Systems")]
        [SerializeField]
        [Tooltip("The AudioManager instance to bind.")]
        private AudioManager _audioManager;

        [SerializeField]
        [Tooltip("The FailUI instance to bind.")]
        private LevelManager _levelManager;

        [SerializeField]
        [Tooltip("Prefab of the PauseManager to be instantiated.")]
        private PauseManager _pauseManagerPrefab;

        [Group("Save/Load System")]
        [SerializeField]
        [Tooltip("Prefab of the SaveManager to be instantiated.")]
        private SaveManager _saveManagerPrefab;

        [SerializeField]
        [Tooltip("Prefab of the LoadManager to be instantiated.")]
        private LoadManager _loadManagerPrefab;

        [SerializeField]
        [Tooltip("Prefab of the TimeManager to be instantiated.")]
        private TimeManager _timeManagerPrefab;

        [Group("UI System")]
        [SerializeField]
        [Tooltip("The UIManager instance to bind.")]
        private UIManager _uiManager;

        [SerializeField]
        [Tooltip("The GameplayUI instance to bind.")]
        private GameplayUI _gameplayUI;

        [SerializeField]
        [Tooltip("The SettingsUI instance to bind.")]
        private SettingsUI _settingsUI;

        [SerializeField]
        [Tooltip("The WinUI instance to bind.")]
        private WinUI _winUI;

        [SerializeField]
        [Tooltip("The FailUI instance to bind.")]
        private FailUI _failUI;

        #endregion

        #region Zenject Installation

        /// <summary>
        /// Installs the bindings for all game systems.
        /// </summary>
        public override void InstallBindings()
        {
            BindCoreSystems();
            BindSaveLoadSystem();
            BindUISystem();
        }

        /// <summary>
        /// Binds the core systems (AudioManager, PauseManager).
        /// </summary>
        private void BindCoreSystems()
        {
            Container.Bind<AudioManager>()
                     .FromInstance(_audioManager)
                     .AsSingle();

            Container.Bind<LevelManager>()
                   .FromInstance(_levelManager)
                   .AsSingle();

            Container.Bind<PauseManager>()
                     .FromComponentInNewPrefab(_pauseManagerPrefab)
                     .AsSingle()
                     .NonLazy();

            Container.Bind<TimeManager>()
                    .FromComponentInNewPrefab(_timeManagerPrefab)
                    .AsSingle()
                    .NonLazy();
        }

        /// <summary>
        /// Binds the save/load system (SaveManager, LoadManager).
        /// </summary>
        private void BindSaveLoadSystem()
        {
            Container.Bind<SaveManager>()
                     .FromComponentInNewPrefab(_saveManagerPrefab)
                     .AsSingle()
                     .NonLazy();

            Container.Bind<LoadManager>()
                     .FromComponentInNewPrefab(_loadManagerPrefab)
                     .AsSingle()
                     .NonLazy();
        }

        /// <summary>
        /// Binds the UI system components (UIManager, GameplayUI, SettingsUI, WinUI, FailUI).
        /// </summary>
        private void BindUISystem()
        {
            Container.Bind<UIManager>()
                     .FromInstance(_uiManager)
                     .AsSingle();

            Container.Bind<GameplayUI>()
                     .FromInstance(_gameplayUI)
                     .AsSingle();

            Container.Bind<SettingsUI>()
                     .FromInstance(_settingsUI)
                     .AsSingle();

            Container.Bind<WinUI>()
                     .FromInstance(_winUI)
                     .AsSingle();

            Container.Bind<FailUI>()
                     .FromInstance(_failUI)
                     .AsSingle();
        }

        #endregion
    }
}