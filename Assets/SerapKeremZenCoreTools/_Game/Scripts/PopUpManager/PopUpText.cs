using TMPro;
using UnityEngine;
using TriInspector;

namespace SerapKeremZenCoreTools._Game.PopUpSystem
{
    /// <summary>
    /// Represents a text-based pop-up with support for animations and initialization.
    /// </summary>
    public class PopUpText : PopUp
    {
        #region Variables

        [Group("Components")]
        [SerializeField]
        [Tooltip("The TextMeshPro component used to display the text.")]
        private TextMeshPro textComponent;

        #endregion

        #region Initialization

        /// <summary>
        /// Initializes the component by caching the TextMeshPro component.
        /// </summary>
        protected override void Awake()
        {
            base.Awake();
            textComponent ??= GetComponent<TextMeshPro>();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Initializes the pop-up text with the given arguments.
        /// </summary>
        /// <param name="args">An array of arguments where the first element should be a string.</param>
        public override void Initialize(params object[] args)
        {
            if (args.Length == 0 || !(args[0] is string text))
            {
                Debug.LogError("[PopUpText] Invalid initialization arguments. Expected a string as the first argument.");
                return;
            }

            textComponent.text = text;
            PlayScaleAnimation();
        }

        /// <summary>
        /// Resets the pop-up text's properties to their initial state.
        /// </summary>
        public override void ResetProperties()
        {
            base.ResetProperties();
            textComponent.text = string.Empty;
        }

        #endregion
    }
}