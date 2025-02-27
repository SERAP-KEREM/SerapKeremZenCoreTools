using UnityEngine;
using System.Collections.Generic;
using Zenject;

namespace SerapKeremZenCoreTools._Game.ObjectPool
{
    /// <summary>
    /// Manages object pools for efficient reuse of game objects.
    /// </summary>
    public class ObjectPoolManager : MonoBehaviour
    {
        #region Variables

        [SerializeField]
        [Tooltip("The root transform for all pooled objects.")]
        private Transform poolRoot;

        [SerializeField]
        [Tooltip("Whether the manager should persist across scenes.")]
        private bool dontDestroyOnLoad = true;

        private Dictionary<string, object> _pools = new();

        #endregion

        #region Initialization

        /// <summary>
        /// Initializes the object pool manager and sets up the pool root.
        /// </summary>
        private void Awake()
        {
            if (dontDestroyOnLoad)
            {
                DontDestroyOnLoad(gameObject);
            }

            if (poolRoot == null)
            {
                poolRoot = transform;
            }
        }

        #endregion

        #region Pool Management

        /// <summary>
        /// Creates a new object pool or retrieves an existing one for the given prefab.
        /// </summary>
        /// <typeparam name="T">The type of MonoBehaviour to pool.</typeparam>
        /// <param name="prefab">The prefab to instantiate for the pool.</param>
        /// <param name="initialSize">The initial number of objects to create in the pool.</param>
        /// <returns>An ObjectPool instance for the specified prefab.</returns>
        public ObjectPool<T> CreatePool<T>(T prefab, int initialSize = 10) where T : MonoBehaviour
        {
            string key = $"{typeof(T).Name}_{prefab.name}";

            if (_pools.TryGetValue(key, out object existingPool))
            {
                return (ObjectPool<T>)existingPool;
            }

            Transform poolParent = new GameObject($"Pool_{prefab.name}").transform;
            poolParent.SetParent(poolRoot);

            var pool = new ObjectPool<T>(prefab, initialSize, poolParent);
            _pools.Add(key, pool);

            return pool;
        }

        /// <summary>
        /// Retrieves an existing object pool by its prefab name.
        /// </summary>
        /// <typeparam name="T">The type of MonoBehaviour to retrieve.</typeparam>
        /// <param name="prefabName">The name of the prefab associated with the pool.</param>
        /// <returns>The requested ObjectPool instance, or null if not found.</returns>
        public ObjectPool<T> GetPool<T>(string prefabName) where T : MonoBehaviour
        {
            string key = $"{typeof(T).Name}_{prefabName}";

            if (_pools.TryGetValue(key, out object pool))
            {
                return (ObjectPool<T>)pool;
            }

            Debug.LogWarning($"[ObjectPoolManager] Pool not found for {key}");
            return null;
        }

        /// <summary>
        /// Clears all object pools and destroys their contents.
        /// </summary>
        public void ClearAllPools()
        {
            foreach (var pool in _pools.Values)
            {
                if (pool is ObjectPool<MonoBehaviour> objectPool)
                {
                    objectPool.Clear();
                }
            }

            _pools.Clear();
        }

        #endregion

        #region Cleanup

        /// <summary>
        /// Cleans up all pools when the manager is destroyed.
        /// </summary>
        private void OnDestroy()
        {
            ClearAllPools();
        }

        #endregion
    }
}