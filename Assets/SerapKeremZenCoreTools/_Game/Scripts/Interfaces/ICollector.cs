namespace SerapKeremZenCoreTools._Game.InputSystem
{
    /// <summary>
    /// Defines the interface for collecting objects.
    /// </summary>
    public interface ICollector
    {
        /// <summary>
        /// Collects the specified collectable object.
        /// </summary>
        /// <param name="collectable">The object to be collected.</param>
        void Collect(ICollectable collectable);
    }
}