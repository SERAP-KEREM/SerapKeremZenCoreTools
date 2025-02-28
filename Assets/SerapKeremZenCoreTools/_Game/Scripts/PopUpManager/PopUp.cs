using UnityEngine;
using DG.Tweening;
using TriInspector;

namespace SerapKeremZenCoreTools._Game.PopUpSystem
{
    /// <summary>
    /// Base class for managing pop-up animations and behaviors.
    /// </summary>
    public abstract class PopUp : MonoBehaviour
    {
        #region Variables

        [Group("Animation Settings")]
        [SerializeField]
        [Tooltip("Duration of the scale animation.")]
        protected float scaleDuration = 0.3f;

        [SerializeField]
        [Tooltip("Multiplier for the scale animation.")]
        protected float scaleMultiplier = 1.2f;

        protected Vector3 initialScale;
        protected Sequence scaleSequence;

        #endregion

        #region Initialization

        /// <summary>
        /// Initializes the component by caching the initial scale.
        /// </summary>
        protected virtual void Awake()
        {
            initialScale = transform.localScale;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Initializes the pop-up with the given arguments.
        /// </summary>
        /// <param name="args">Optional arguments to configure the pop-up.</param>
        public abstract void Initialize(params object[] args);

        /// <summary>
        /// Resets the pop-up's properties to their initial state.
        /// </summary>
        public virtual void ResetProperties()
        {
            scaleSequence?.Kill();
            transform.localScale = initialScale;
        }

        #endregion

        #region Animation

        /// <summary>
        /// Plays a scale animation for the pop-up.
        /// </summary>
        protected virtual void PlayScaleAnimation()
        {
            scaleSequence?.Kill();
            scaleSequence = DOTween.Sequence();
            scaleSequence.Append(transform.DOScale(initialScale * scaleMultiplier, scaleDuration / 2))
                         .Append(transform.DOScale(initialScale, scaleDuration / 2));
        }

        #endregion

        #region Cleanup

        /// <summary>
        /// Cleans up the animation sequence when the object is destroyed.
        /// </summary>
        private void OnDestroy()
        {
            scaleSequence?.Kill();
        }

        #endregion
    }
}