using UnityEngine;
using TriInspector;
using Zenject;

namespace SerapKeremZenCoreTools._Game.InputSystem
{
    /// <summary>
    /// Manages selection and collection of objects using raycasting.
    /// Implements ISelector for selecting/deselecting objects and ICollector for collecting objects.
    /// </summary>
    public class Selector : MonoBehaviour, ISelector, ICollector
    {
        #region Variables

        [Group("Camera Settings")]
        [SerializeField, Required]
        [Tooltip("The camera used for raycasting.")]
        private Camera _selectionCamera;

        [Group("Raycast Settings")]
        [SerializeField]
        [Tooltip("The layer mask to filter raycast hits.")]
        private LayerMask _raycastLayerMask = ~0;

        [SerializeField]
        [Tooltip("The maximum distance of the raycast.")]
        private float _raycastLength = 100f;

        [Group("Gizmo Colors")]
        [SerializeField]
        [Tooltip("The color of the gizmo when the mouse is pressed.")]
        private Color _mouseDownGizmoColor = Color.green;

        [SerializeField]
        [Tooltip("The color of the gizmo when the mouse is held.")]
        private Color _mouseHoldGizmoColor = Color.yellow;

        [SerializeField]
        [Tooltip("The color of the gizmo when the mouse is released.")]
        private Color _mouseUpGizmoColor = Color.red;

        private ISelectable _currentSelectable;
        private PlayerInput _playerInput;
        private Color _currentGizmoColor;
        private Vector3 _lastInputPosition;

        #endregion

        #region Initialization

        /// <summary>
        /// Constructor method for dependency injection.
        /// </summary>
        /// <param name="playerInput">The PlayerInput instance to handle input events.</param>
        [Inject]
        private void Construct(PlayerInput playerInput)
        {
            _playerInput = playerInput;
        }

        /// <summary>
        /// Initializes the component by setting up the camera and subscribing to input events.
        /// </summary>
        private void Start()
        {
            _selectionCamera = _selectionCamera ?? Camera.main;
            SubscribeToEvents();
        }

        /// <summary>
        /// Cleans up event subscriptions when the object is destroyed.
        /// </summary>
        private void OnDestroy()
        {
            UnsubscribeFromEvents();
        }

        #endregion

        #region Event Handling

        /// <summary>
        /// Subscribes to input events from the PlayerInput system.
        /// </summary>
        private void SubscribeToEvents()
        {
            _playerInput.OnMouseDown.AddListener(HandleMouseDown);
            _playerInput.OnMouseHeld.AddListener(HandleMouseHeld);
            _playerInput.OnMouseUp.AddListener(HandleMouseUp);
        }

        /// <summary>
        /// Unsubscribes from input events to prevent memory leaks.
        /// </summary>
        private void UnsubscribeFromEvents()
        {
            if (_playerInput == null) return;

            _playerInput.OnMouseDown.RemoveListener(HandleMouseDown);
            _playerInput.OnMouseHeld.RemoveListener(HandleMouseHeld);
            _playerInput.OnMouseUp.RemoveListener(HandleMouseUp);
        }

        #endregion

        #region Mouse Input Handlers

        /// <summary>
        /// Handles the mouse down event by performing a raycast and selecting an object.
        /// </summary>
        /// <param name="position">The screen position of the mouse click.</param>
        private void HandleMouseDown(Vector3 position)
        {
            _lastInputPosition = position;
            _currentGizmoColor = _mouseDownGizmoColor;

            PerformRaycast(position, hit =>
            {
                var selectable = hit.collider.GetComponent<ISelectable>();
                if (selectable != null)
                {
                    _currentSelectable = selectable;
                    Select(_currentSelectable);
                }
            });
        }

        /// <summary>
        /// Handles the mouse held event by performing a raycast and updating the selection.
        /// </summary>
        /// <param name="position">The current screen position of the mouse.</param>
        private void HandleMouseHeld(Vector3 position)
        {
            _lastInputPosition = position;
            _currentGizmoColor = _mouseHoldGizmoColor;

            PerformRaycast(position, hit =>
            {
                var selectable = hit.collider.GetComponent<ISelectable>();
                if (selectable != null && _currentSelectable != selectable)
                {
                    DeSelect(_currentSelectable);
                    _currentSelectable = selectable;
                    Select(_currentSelectable);
                }
            });
        }

        /// <summary>
        /// Handles the mouse up event by deselecting the current object and collecting it if applicable.
        /// </summary>
        /// <param name="position">The screen position of the mouse release.</param>
        private void HandleMouseUp(Vector3 position)
        {
            _lastInputPosition = position;
            _currentGizmoColor = _mouseUpGizmoColor;

            if (_currentSelectable != null)
            {
                DeSelect(_currentSelectable);
                _currentSelectable = null;
            }

            PerformRaycast(position, hit =>
            {
                var collectable = hit.collider.GetComponent<ICollectable>();
                if (collectable != null)
                {
                    Collect(collectable);
                }
            });
        }

        #endregion

        #region Raycasting

        /// <summary>
        /// Performs a raycast from the camera to the specified screen position.
        /// </summary>
        /// <param name="screenPosition">The screen position to cast the ray from.</param>
        /// <param name="onHit">Callback to execute when a raycast hit occurs.</param>
        private void PerformRaycast(Vector3 screenPosition, System.Action<RaycastHit> onHit)
        {
            if (_selectionCamera == null) return;

            Ray ray = _selectionCamera.ScreenPointToRay(screenPosition);
            if (Physics.Raycast(ray, out RaycastHit hit, _raycastLength, _raycastLayerMask))
            {
                onHit?.Invoke(hit);
            }
        }

        #endregion

        #region Interface Implementations

        /// <summary>
        /// Selects the given object.
        /// </summary>
        /// <param name="selectable">The object to select.</param>
        public void Select(ISelectable selectable)
        {
            selectable?.Select();
        }

        /// <summary>
        /// Deselects the given object.
        /// </summary>
        /// <param name="selectable">The object to deselect.</param>
        public void DeSelect(ISelectable selectable)
        {
            selectable?.DeSelect();
        }

        /// <summary>
        /// Collects the given object.
        /// </summary>
        /// <param name="collectable">The object to collect.</param>
        public void Collect(ICollectable collectable)
        {
            collectable?.OnCollect();
        }

        #endregion

        #region Gizmos

        /// <summary>
        /// Draws a ray in the Scene view to visualize the raycast.
        /// </summary>
        private void OnDrawGizmos()
        {
            if (_selectionCamera == null || !Application.isPlaying) return;

            Ray ray = _selectionCamera.ScreenPointToRay(_lastInputPosition);
            Gizmos.color = _currentGizmoColor;
            Gizmos.DrawRay(ray.origin, ray.direction * _raycastLength);
        }

        #endregion
    }
}