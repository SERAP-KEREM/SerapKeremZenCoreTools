using UnityEngine;
using TriInspector;

namespace SerapKeremZenCoreTools._Game.PopUpSystem
{
    /// <summary>
    /// Manages the creation and display of text-based pop-ups.
    /// </summary>
    public class PopUpTextManager : PopUpManager<PopUpText>
    {
        #region Public Methods

        /// <summary>
        /// Displays a text-based pop-up at the specified position with the given animation type.
        /// </summary>
        /// <param name="position">The world position where the pop-up will appear.</param>
        /// <param name="text">The text content to display in the pop-up.</param>
        /// <param name="animationType">The type of animation to apply to the pop-up.</param>
        public void ShowPopUpText(Vector3 position, string text, PopUpAnimationType animationType)
        {
            // Retrieve a pop-up from the pool or create a new one if the pool is empty.
            PopUpText popUp = popUpPool.Get(position);

            // Initialize the pop-up with the provided text.
            popUp.Initialize(text);

            // Apply the specified animation type to the pop-up.
            HandleAnimation(popUp, animationType);
        }

        #endregion
    }
}