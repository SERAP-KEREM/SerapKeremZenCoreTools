namespace SerapKeremZenCoreTools._Game.InputSystem
{
    /// <summary>
    /// Defines the interface for selecting and deselecting objects.
    /// </summary>
    public interface ISelector
    {
        /// <summary>
        /// Selects the specified object.
        /// </summary>
        /// <param name="selectable">The object to be selected.</param>
        void Select(ISelectable selectable);

        /// <summary>
        /// Deselects the specified object.
        /// </summary>
        /// <param name="selectable">The object to be deselected.</param>
        void DeSelect(ISelectable selectable);
    }
}