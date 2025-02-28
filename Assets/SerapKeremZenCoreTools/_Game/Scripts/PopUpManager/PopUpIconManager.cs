using UnityEngine;
using TriInspector;

namespace SerapKeremZenCoreTools._Game.PopUpSystem
{
    /// <summary>
    /// Manages the creation and display of icon-based pop-ups.
    /// </summary>
    public class PopUpIconManager : PopUpManager<PopUpIcon>
    {
        #region Public Methods

        /// <summary>
        /// Displays an icon-based pop-up at the specified position with the given animation type.
        /// </summary>
        /// <param name="position">The world position where the pop-up will appear.</param>
        /// <param name="icon">The sprite to display in the pop-up.</param>
        /// <param name="animationType">The type of animation to apply to the pop-up.</param>
        public void ShowPopUpIcon(Vector3 position, Sprite icon, PopUpAnimationType animationType)
        {
            // Retrieve a pop-up from the pool or create a new one if the pool is empty.
            PopUpIcon popUp = popUpPool.Get(position);

            // Initialize the pop-up with the provided icon.
            popUp.Initialize(icon);

            // Apply the specified animation type to the pop-up.
            HandleAnimation(popUp, animationType);
        }

        #endregion
    }
}