using UnityEngine;

namespace SerapKeremZenCoreTools._Game.InputSystem
{
    /// <summary>
    /// A test class implementing the ICollectable and ISelectable interfaces for testing purposes.
    /// </summary>
    public class TestCollectable : MonoBehaviour, ICollectable, ISelectable
    {
        #region Variables

        [SerializeField]
        [Tooltip("The default material of the object.")]
        private Material _defaultMaterial;

        [SerializeField]
        [Tooltip("The material to use when the object is selected.")]
        private Material _selectedMaterial;

        private MeshRenderer _meshRenderer;

        #endregion

        #region Initialization

        /// <summary>
        /// Initializes the component by caching the MeshRenderer.
        /// </summary>
        private void Awake()
        {
            _meshRenderer = GetComponent<MeshRenderer>();
            if (_meshRenderer == null)
            {
                Debug.LogError("[TestCollectable] MeshRenderer not found on this GameObject.");
            }
        }

        #endregion

        #region ICollectable Implementation

        /// <summary>
        /// Called when the object is collected.
        /// Destroys the object and logs a message.
        /// </summary>
        public void OnCollect()
        {
            Debug.Log($"[TestCollectable] Collected: {gameObject.name}");
            Destroy(gameObject);
        }

        #endregion

        #region ISelectable Implementation

        /// <summary>
        /// Changes the object's material to indicate it is selected.
        /// </summary>
        public void Select()
        {
            if (_meshRenderer != null && _selectedMaterial != null)
            {
                _meshRenderer.material = _selectedMaterial;
            }
            else
            {
                Debug.LogWarning("[TestCollectable] MeshRenderer or SelectedMaterial is missing.");
            }
        }

        /// <summary>
        /// Restores the object's material to its default state.
        /// </summary>
        public void DeSelect()
        {
            if (_meshRenderer != null && _defaultMaterial != null)
            {
                _meshRenderer.material = _defaultMaterial;
            }
            else
            {
                Debug.LogWarning("[TestCollectable] MeshRenderer or DefaultMaterial is missing.");
            }
        }

        #endregion
    }
}