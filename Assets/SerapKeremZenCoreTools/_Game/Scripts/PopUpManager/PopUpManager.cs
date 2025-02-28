using UnityEngine;
using Zenject;
using DG.Tweening;
using TriInspector;
using SerapKeremZenCoreTools._Game.ObjectPool;

namespace SerapKeremZenCoreTools._Game.PopUpSystem
{
    /// <summary>
    /// Base class for managing pop-ups with pooling and animations.
    /// </summary>
    /// <typeparam name="T">The type of PopUp to manage.</typeparam>
    public abstract class PopUpManager<T> : MonoBehaviour where T : PopUp
    {
        #region Variables

        [Group("Pool Settings")]
        [SerializeField]
        [Tooltip("The initial size of the object pool.")]
        protected int poolSize = 10;

        [SerializeField]
        [Tooltip("The prefab used to instantiate pop-ups.")]
        protected T prefab;

        [Group("Animation Settings")]
        [SerializeField]
        [Tooltip("Duration of the animation in seconds.")]
        protected float animationDuration = 0.5f;

        [SerializeField]
        [Tooltip("Delay before hiding the pop-up after the animation ends.")]
        protected float hideDelay = 1f;

        [SerializeField]
        [Tooltip("Height of the bounce animation.")]
        protected float bounceHeight = 2f;

        [SerializeField]
        [Tooltip("Number of bounces in the bounce animation.")]
        protected int bounceCount = 3;

        [SerializeField]
        [Tooltip("Offset for slide animations (e.g., SlideUp or SlideDown).")]
        protected Vector3 slideOffset = new Vector3(0, 2, 0);

        protected ObjectPool<T> popUpPool;

        #endregion

        #region Initialization

        /// <summary>
        /// Initializes the component by setting up the object pool.
        /// </summary>
        protected virtual void Start()
        {
            InitializePool();
        }

        /// <summary>
        /// Creates and initializes the object pool for pop-ups.
        /// </summary>
        protected virtual void InitializePool()
        {
            if (prefab == null)
            {
                Debug.LogError("[PopUpManager] Prefab is not assigned.");
                return;
            }

            popUpPool = new ObjectPool<T>(prefab, poolSize, transform);
        }

        #endregion

        #region Animation Handling

        /// <summary>
        /// Handles the animation for the given pop-up based on the specified animation type.
        /// </summary>
        /// <param name="popUp">The pop-up to animate.</param>
        /// <param name="animationType">The type of animation to apply.</param>
        protected virtual void HandleAnimation(T popUp, PopUpAnimationType animationType)
        {
            Sequence sequence = DOTween.Sequence();

            switch (animationType)
            {
                case PopUpAnimationType.ScaleAndFade:
                    sequence.Append(popUp.transform.DOScale(Vector3.zero, animationDuration));
                    break;

                case PopUpAnimationType.SlideUp:
                    sequence.Append(popUp.transform.DOMove(popUp.transform.position + slideOffset, animationDuration));
                    break;

                case PopUpAnimationType.SlideDown:
                    sequence.Append(popUp.transform.DOMove(popUp.transform.position - slideOffset, animationDuration));
                    break;

                case PopUpAnimationType.Bounce:
                    for (int i = 0; i < bounceCount; i++)
                    {
                        sequence.Append(popUp.transform.DOMoveY(popUp.transform.position.y + bounceHeight, animationDuration / bounceCount))
                               .Append(popUp.transform.DOMoveY(popUp.transform.position.y, animationDuration / bounceCount));
                    }
                    break;
            }

            sequence.AppendInterval(hideDelay)
                   .OnComplete(() => ReturnToPool(popUp));
        }

        #endregion

        #region Pool Management

        /// <summary>
        /// Returns the given pop-up back to the object pool.
        /// </summary>
        /// <param name="popUp">The pop-up to return to the pool.</param>
        protected virtual void ReturnToPool(T popUp)
        {
            if (popUp == null)
            {
                Debug.LogWarning("[PopUpManager] Attempted to return a null pop-up to the pool.");
                return;
            }

            popUp.ResetProperties();
            popUpPool.Release(popUp);
        }

        #endregion
    }
}