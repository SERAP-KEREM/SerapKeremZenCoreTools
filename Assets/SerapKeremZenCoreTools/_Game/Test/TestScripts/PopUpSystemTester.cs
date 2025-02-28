using UnityEngine;
using System.Collections.Generic;
using Zenject;
using TriInspector;
using SerapKeremZenCoreTools._Game.PopUpSystem;
using SerapKeremZenCoreTools._Game.InputSystem;

namespace SerapKeremZenCoreTools.Tests
{
    /// <summary>
    /// A tester class for the Pop-Up System to verify its functionality in a test scene.
    /// </summary>
    [DeclareBoxGroup("Text Settings")]
    [DeclareBoxGroup("Icon Settings")]
    [DeclareBoxGroup("Animation Settings")]
    public class PopUpSystemTester : MonoBehaviour
    {
        #region Variables

        [Group("Text Settings")]
        [SerializeField]
        [Tooltip("List of messages to choose from when creating a pop-up.")]
        private List<string> popUpTextOptions = new List<string>
        {
            "Great!",
            "+100",
            "Perfect!",
            "Awesome!",
            "Level Up!"
        };

        [Group("Text Settings")]
        [SerializeField]
        [Tooltip("The animation type for text pop-ups.")]
        private PopUpAnimationType textAnimationType = PopUpAnimationType.ScaleAndFade;

        [Group("Icon Settings")]
        [SerializeField]
        [Tooltip("List of icons to choose from when creating a pop-up.")]
        private List<Sprite> popUpIconOptions = new List<Sprite>();

        [Group("Icon Settings")]
        [SerializeField]
        [Tooltip("The animation type for icon pop-ups.")]
        private PopUpAnimationType iconAnimationType = PopUpAnimationType.Bounce;

        [Group("Animation Settings")]
        [SerializeField]
        [Tooltip("Offset for text pop-ups relative to the mouse position.")]
        private Vector3 textOffset = Vector3.up;

        [Group("Animation Settings")]
        [SerializeField]
        [Tooltip("Offset for icon pop-ups relative to the mouse position.")]
        private Vector3 iconOffset = Vector3.down;

        private PopUpTextManager _textManager;
        private PopUpIconManager _iconManager;
        private PlayerInput _playerInput;
        private System.Random _random;
        private Camera _mainCamera;

        #endregion

        #region Initialization

        /// <summary>
        /// Constructor method for dependency injection.
        /// </summary>
        /// <param name="textManager">The manager for handling text pop-ups.</param>
        /// <param name="iconManager">The manager for handling icon pop-ups.</param>
        /// <param name="playerInput">The input system for handling mouse events.</param>
        [Inject]
        private void Construct(
            PopUpTextManager textManager,
            PopUpIconManager iconManager,
            PlayerInput playerInput)
        {
            _textManager = textManager;
            _iconManager = iconManager;
            _playerInput = playerInput;
            _random = new System.Random();
        }

        /// <summary>
        /// Initializes the component by setting up the camera and subscribing to input events.
        /// </summary>
        private void Start()
        {
            _mainCamera = Camera.main;
            _playerInput.OnMouseDown.AddListener(HandleMouseInput);
        }

        /// <summary>
        /// Cleans up event subscriptions when the object is destroyed.
        /// </summary>
        private void OnDestroy()
        {
            if (_playerInput != null)
            {
                _playerInput.OnMouseDown.RemoveListener(HandleMouseInput);
            }
        }

        #endregion

        #region Input Handling

        /// <summary>
        /// Handles mouse input by converting screen position to world position and triggering pop-ups.
        /// </summary>
        /// <param name="mousePosition">The screen position of the mouse click.</param>
        private void HandleMouseInput(Vector3 mousePosition)
        {
            Vector3 worldPosition = _mainCamera.ScreenToWorldPoint(mousePosition);
            worldPosition.z = 0;
            ShowPopUps(worldPosition);
        }

        #endregion

        #region Pop-Up Management

        /// <summary>
        /// Displays random text and icon pop-ups at the specified position with offsets.
        /// </summary>
        /// <param name="position">The world position where pop-ups will appear.</param>
        private void ShowPopUps(Vector3 position)
        {
            if (popUpTextOptions.Count > 0)
            {
                string message = popUpTextOptions[_random.Next(popUpTextOptions.Count)];
                _textManager.ShowPopUpText(position + textOffset, message, textAnimationType);
            }

            if (popUpIconOptions.Count > 0)
            {
                Sprite icon = popUpIconOptions[_random.Next(popUpIconOptions.Count)];
                _iconManager.ShowPopUpIcon(position + iconOffset, icon, iconAnimationType);
            }
        }

        /// <summary>
        /// Tests pop-ups at the current position of the GameObject.
        /// </summary>
        [Button("Test At Current Position")]
        private void TestAtCurrentPosition()
        {
            ShowPopUps(transform.position);
        }

        #endregion
    }
}