namespace SerapKeremZenCoreTools._Game.InputSystem
{
    /// <summary>
    /// Defines the interface for objects that can be collected.
    /// </summary>
    public interface ICollectable
    {
        /// <summary>
        /// Called when the object is collected.
        /// </summary>
        void OnCollect();
    }
}