namespace SerapKeremZenCoreTools._Game.InputSystem
{
    /// <summary>
    /// Defines the interface for objects that can be selected and deselected.
    /// </summary>
    public interface ISelectable
    {
        /// <summary>
        /// Called when the object is selected.
        /// </summary>
        void Select();

        /// <summary>
        /// Called when the object is deselected.
        /// </summary>
        void DeSelect();
    }
}