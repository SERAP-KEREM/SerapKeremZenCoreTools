using UnityEngine;
using Zenject;
using SerapKeremZenCoreTools._Game.AudioSystem;

namespace SerapKeremZenCoreTools._Game._ZenjectInstaller
{
    /// <summary>
    /// Installer class for game systems using Zenject.
    /// </summary>
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private AudioManager _audioManager;

        public override void InstallBindings()
        {
            InstallAudioSystem();
            SignalBusInstaller.Install(Container);
        }

        /// <summary>
        /// Binds the audio system to the dependency injection container.
        /// </summary>
        private void InstallAudioSystem()
        {
            Container
                .Bind<AudioManager>()
                .FromInstance(_audioManager)
                .AsSingle()
                .NonLazy();
        }
    }
}