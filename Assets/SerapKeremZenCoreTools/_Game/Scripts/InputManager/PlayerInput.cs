using UnityEngine;
using UnityEngine.Events;
using TriInspector;

namespace SerapKeremZenCoreTools._Game.InputSystem
{
    /// <summary>
    /// Manages mouse input events and invokes UnityEvents for MouseDown, MouseHeld, and MouseUp.
    /// </summary>
    public class PlayerInput : MonoBehaviour
    {
        #region Events

        /// <summary>
        /// Event triggered when the left mouse button is pressed down.
        /// </summary>
        [PropertyTooltip("Event triggered when the left mouse button is pressed.")]
        public UnityEvent<Vector3> OnMouseDown = new();

        /// <summary>
        /// Event triggered while the left mouse button is being held down.
        /// </summary>
        [PropertyTooltip("Event triggered while the left mouse button is held.")]
        public UnityEvent<Vector3> OnMouseHeld = new();

        /// <summary>
        /// Event triggered when the left mouse button is released.
        /// </summary>
        [PropertyTooltip("Event triggered when the left mouse button is released.")]
        public UnityEvent<Vector3> OnMouseUp = new();

        #endregion

        #region Input Handling

        /// <summary>
        /// Updates the component every frame to handle mouse input.
        /// </summary>
        private void Update()
        {
            HandleMouseInput();
        }

        /// <summary>
        /// Handles mouse input and invokes the appropriate events based on the mouse state.
        /// </summary>
        private void HandleMouseInput()
        {
            if (Input.GetMouseButtonDown(0))
            {
                OnMouseDown?.Invoke(Input.mousePosition);
            }
            else if (Input.GetMouseButton(0))
            {
                OnMouseHeld?.Invoke(Input.mousePosition);
            }
            else if (Input.GetMouseButtonUp(0))
            {
                OnMouseUp?.Invoke(Input.mousePosition);
            }
        }

        #endregion
    }
}