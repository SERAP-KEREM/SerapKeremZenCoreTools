using UnityEngine;
using System.Collections.Generic;

namespace SerapKeremZenCoreTools._Game.ObjectPool
{
    /// <summary>
    /// A generic object pooling system for efficient management of reusable objects.
    /// </summary>
    /// <typeparam name="T">The type of MonoBehaviour to pool.</typeparam>
    public class ObjectPool<T> where T : MonoBehaviour
    {
        #region Variables

        private readonly Stack<T> _pool;
        private readonly T _prefab;
        private readonly Transform _parent;

        #endregion

        #region Initialization

        /// <summary>
        /// Initializes a new instance of the ObjectPool class.
        /// </summary>
        /// <param name="prefab">The prefab to instantiate for the pool.</param>
        /// <param name="initialSize">The initial number of objects to create in the pool.</param>
        /// <param name="parent">The parent transform for pooled objects.</param>
        public ObjectPool(T prefab, int initialSize, Transform parent)
        {
            _prefab = prefab;
            _parent = parent;
            _pool = new Stack<T>(initialSize);

            for (int i = 0; i < initialSize; i++)
            {
                CreateObject();
            }
        }

        #endregion

        #region Object Management

        /// <summary>
        /// Creates a new object and adds it to the pool.
        /// </summary>
        private void CreateObject()
        {
            T obj = GameObject.Instantiate(_prefab, _parent);
            obj.gameObject.SetActive(false);
            _pool.Push(obj);
        }

        /// <summary>
        /// Retrieves an object from the pool or creates a new one if the pool is empty.
        /// </summary>
        /// <returns>An active object from the pool.</returns>
        public T Get()
        {
            T obj = _pool.Count > 0 ? _pool.Pop() : GameObject.Instantiate(_prefab, _parent);
            obj.gameObject.SetActive(true);
            return obj;
        }

        /// <summary>
        /// Retrieves an object from the pool and sets its position.
        /// </summary>
        /// <param name="position">The position to place the object at.</param>
        /// <returns>An active object from the pool with the specified position.</returns>
        public T Get(Vector3 position)
        {
            T obj = Get();
            obj.transform.position = position;
            return obj;
        }

        /// <summary>
        /// Retrieves an object from the pool and sets its position and rotation.
        /// </summary>
        /// <param name="position">The position to place the object at.</param>
        /// <param name="rotation">The rotation to apply to the object.</param>
        /// <returns>An active object from the pool with the specified position and rotation.</returns>
        public T Get(Vector3 position, Quaternion rotation)
        {
            T obj = Get();
            obj.transform.SetPositionAndRotation(position, rotation);
            return obj;
        }

        /// <summary>
        /// Releases an object back into the pool.
        /// </summary>
        /// <param name="obj">The object to release.</param>
        public void Release(T obj)
        {
            if (obj == null) return;

            obj.gameObject.SetActive(false);
            _pool.Push(obj);
        }

        /// <summary>
        /// Clears the pool by destroying all objects in it.
        /// </summary>
        public void Clear()
        {
            while (_pool.Count > 0)
            {
                var obj = _pool.Pop();
                if (obj != null)
                {
                    GameObject.Destroy(obj.gameObject);
                }
            }
        }

        #endregion
    }
}