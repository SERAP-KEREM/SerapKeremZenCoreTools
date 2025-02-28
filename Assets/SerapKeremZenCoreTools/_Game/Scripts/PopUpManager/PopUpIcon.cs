using UnityEngine;
using TriInspector;

namespace SerapKeremZenCoreTools._Game.PopUpSystem
{
    /// <summary>
    /// Represents an icon-based pop-up with support for animations and initialization.
    /// </summary>
    public class PopUpIcon : PopUp
    {
        #region Variables

        [Group("Components")]
        [SerializeField]
        [Tooltip("The SpriteRenderer component used to display the icon.")]
        private SpriteRenderer spriteRenderer;

        #endregion

        #region Initialization

        /// <summary>
        /// Initializes the component by caching the SpriteRenderer.
        /// </summary>
        protected override void Awake()
        {
            base.Awake();
            spriteRenderer ??= GetComponent<SpriteRenderer>();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Initializes the pop-up icon with the given arguments.
        /// </summary>
        /// <param name="args">An array of arguments where the first element should be a Sprite.</param>
        public override void Initialize(params object[] args)
        {
            if (args.Length == 0 || !(args[0] is Sprite sprite))
            {
                Debug.LogError("[PopUpIcon] Invalid initialization arguments. Expected a Sprite as the first argument.");
                return;
            }

            spriteRenderer.sprite = sprite;
            PlayScaleAnimation();
        }

        /// <summary>
        /// Resets the pop-up icon's properties to their initial state.
        /// </summary>
        public override void ResetProperties()
        {
            base.ResetProperties();
            spriteRenderer.sprite = null;
        }

        #endregion
    }
}