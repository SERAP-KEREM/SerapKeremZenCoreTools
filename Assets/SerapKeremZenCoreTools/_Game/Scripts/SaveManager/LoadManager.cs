using UnityEngine;
using System;

namespace SerapKeremZenCoreTools._Game.SaveLoadSystem
{
    /// <summary>
    /// Manages loading data from PlayerPrefs with optional encryption support.
    /// </summary>
    public class LoadManager : MonoBehaviour
    {
        #region Variables

        [SerializeField]
        [Tooltip("Enables or disables encryption for saved data.")]
        private bool _useEncryption = true;

        private const string ENCRYPTION_KEY = "SERAP_KEREM_ZEN_CORE_TOOLS";

        #endregion

        #region Public Methods

        /// <summary>
        /// Loads data of type T from PlayerPrefs.
        /// </summary>
        /// <typeparam name="T">The type of data to load.</typeparam>
        /// <param name="key">The key to identify the saved data.</param>
        /// <param name="defaultValue">The default value to return if no data is found.</param>
        /// <returns>The loaded data or the default value if an error occurs.</returns>
        public T LoadData<T>(string key, T defaultValue = default)
        {
            try
            {
                string finalKey = _useEncryption ? EncryptKey(key) : key;

                if (!PlayerPrefs.HasKey(finalKey))
                {
                    Debug.LogWarning($"[LoadManager] No data found for key: {key}");
                    return defaultValue;
                }

                string encryptedValue = PlayerPrefs.GetString(finalKey);
                string decryptedValue = _useEncryption ? DecryptValue(encryptedValue) : encryptedValue;

                if (typeof(T) == typeof(string))
                {
                    return (T)(object)decryptedValue;
                }
                else if (typeof(T) == typeof(int))
                {
                    return (T)(object)int.Parse(decryptedValue);
                }
                else if (typeof(T) == typeof(float))
                {
                    return (T)(object)float.Parse(decryptedValue);
                }
                else if (typeof(T) == typeof(bool))
                {
                    return (T)(object)bool.Parse(decryptedValue);
                }
                else
                {
                    return JsonUtility.FromJson<T>(decryptedValue);
                }
            }
            catch (Exception e)
            {
                Debug.LogError($"[LoadManager] Error loading data: {e.Message}");
                return defaultValue;
            }
        }

        /// <summary>
        /// Loads a Vector2 from PlayerPrefs.
        /// </summary>
        /// <param name="key">The key to identify the saved data.</param>
        /// <param name="defaultValue">The default value to return if no data is found.</param>
        /// <returns>The loaded Vector2 or the default value if an error occurs.</returns>
        public Vector2 LoadVector2(string key, Vector2 defaultValue = default)
        {
            string value = LoadData<string>(key);
            if (string.IsNullOrEmpty(value)) return defaultValue;

            try
            {
                string[] parts = value.Split(',');
                return new Vector2(float.Parse(parts[0]), float.Parse(parts[1]));
            }
            catch
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// Loads a Vector3 from PlayerPrefs.
        /// </summary>
        /// <param name="key">The key to identify the saved data.</param>
        /// <param name="defaultValue">The default value to return if no data is found.</param>
        /// <returns>The loaded Vector3 or the default value if an error occurs.</returns>
        public Vector3 LoadVector3(string key, Vector3 defaultValue = default)
        {
            string value = LoadData<string>(key);
            if (string.IsNullOrEmpty(value)) return defaultValue;

            try
            {
                string[] parts = value.Split(',');
                return new Vector3(float.Parse(parts[0]), float.Parse(parts[1]), float.Parse(parts[2]));
            }
            catch
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// Loads a Color from PlayerPrefs.
        /// </summary>
        /// <param name="key">The key to identify the saved data.</param>
        /// <param name="defaultValue">The default value to return if no data is found.</param>
        /// <returns>The loaded Color or the default value if an error occurs.</returns>
        public Color LoadColor(string key, Color defaultValue = default)
        {
            string value = LoadData<string>(key);
            if (string.IsNullOrEmpty(value)) return defaultValue;

            try
            {
                string[] parts = value.Split(',');
                return new Color(
                    float.Parse(parts[0]),
                    float.Parse(parts[1]),
                    float.Parse(parts[2]),
                    float.Parse(parts[3])
                );
            }
            catch
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// Loads a DateTime from PlayerPrefs.
        /// </summary>
        /// <param name="key">The key to identify the saved data.</param>
        /// <param name="defaultValue">The default value to return if no data is found.</param>
        /// <returns>The loaded DateTime or the default value if an error occurs.</returns>
        public DateTime LoadDateTime(string key, DateTime defaultValue = default)
        {
            string value = LoadData<string>(key);
            if (string.IsNullOrEmpty(value)) return defaultValue;

            try
            {
                long binaryValue = long.Parse(value);
                return DateTime.FromBinary(binaryValue);
            }
            catch
            {
                return defaultValue;
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Encrypts a key using a base64 encoding scheme.
        /// </summary>
        /// <param name="key">The key to encrypt.</param>
        /// <returns>The encrypted key.</returns>
        private string EncryptKey(string key)
        {
            return Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(key + ENCRYPTION_KEY));
        }

        /// <summary>
        /// Decrypts a value using a base64 decoding scheme.
        /// </summary>
        /// <param name="encryptedValue">The encrypted value to decrypt.</param>
        /// <returns>The decrypted value or an empty string if decryption fails.</returns>
        private string DecryptValue(string encryptedValue)
        {
            try
            {
                byte[] bytes = Convert.FromBase64String(encryptedValue);
                return System.Text.Encoding.UTF8.GetString(bytes);
            }
            catch
            {
                return string.Empty;
            }
        }

        #endregion
    }
}