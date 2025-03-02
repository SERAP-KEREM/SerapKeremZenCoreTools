using UnityEngine;
using System;

namespace SerapKeremZenCoreTools._Game.SaveLoadSystem
{
    /// <summary>
    /// Manages saving data to PlayerPrefs with optional encryption support.
    /// </summary>
    public class SaveManager : MonoBehaviour
    {
        #region Variables

        [SerializeField]
        [Tooltip("Enables or disables encryption for saved data.")]
        private bool _useEncryption = true;

        private const string ENCRYPTION_KEY = "SERAP_KEREM_ZEN_CORE_TOOLS";

        #endregion

        #region Public Methods

        /// <summary>
        /// Saves data of type T to PlayerPrefs.
        /// </summary>
        /// <typeparam name="T">The type of data to save.</typeparam>
        /// <param name="key">The key to identify the saved data.</param>
        /// <param name="value">The value to save.</param>
        public void SaveData<T>(string key, T value)
        {
            try
            {
                string finalKey = _useEncryption ? EncryptKey(key) : key;
                string valueToSave = string.Empty;

                if (typeof(T) == typeof(string))
                {
                    valueToSave = value.ToString();
                }
                else if (typeof(T) == typeof(int) || typeof(T) == typeof(float) || typeof(T) == typeof(bool))
                {
                    valueToSave = value.ToString();
                }
                else
                {
                    valueToSave = JsonUtility.ToJson(value);
                }

                string finalValue = _useEncryption ? EncryptValue(valueToSave) : valueToSave;
                PlayerPrefs.SetString(finalKey, finalValue);
                PlayerPrefs.Save();

            }
            catch (Exception e)
            {
                Debug.LogError($"[SaveManager] Error saving data: {e.Message}");
            }
        }

        /// <summary>
        /// Saves a Vector2 to PlayerPrefs.
        /// </summary>
        /// <param name="key">The key to identify the saved data.</param>
        /// <param name="value">The Vector2 to save.</param>
        public void SaveVector2(string key, Vector2 value)
        {
            SaveData(key, $"{value.x},{value.y}");
        }

        /// <summary>
        /// Saves a Vector3 to PlayerPrefs.
        /// </summary>
        /// <param name="key">The key to identify the saved data.</param>
        /// <param name="value">The Vector3 to save.</param>
        public void SaveVector3(string key, Vector3 value)
        {
            SaveData(key, $"{value.x},{value.y},{value.z}");
        }

        /// <summary>
        /// Saves a Color to PlayerPrefs.
        /// </summary>
        /// <param name="key">The key to identify the saved data.</param>
        /// <param name="value">The Color to save.</param>
        public void SaveColor(string key, Color value)
        {
            SaveData(key, $"{value.r},{value.g},{value.b},{value.a}");
        }

        /// <summary>
        /// Saves a DateTime to PlayerPrefs.
        /// </summary>
        /// <param name="key">The key to identify the saved data.</param>
        /// <param name="value">The DateTime to save.</param>
        public void SaveDateTime(string key, DateTime value)
        {
            SaveData(key, value.ToBinary().ToString());
        }

        /// <summary>
        /// Deletes a specific key from PlayerPrefs.
        /// </summary>
        /// <param name="key">The key to delete.</param>
        public void DeleteKey(string key)
        {
            string finalKey = _useEncryption ? EncryptKey(key) : key;
            PlayerPrefs.DeleteKey(finalKey);
            PlayerPrefs.Save();
        }

        /// <summary>
        /// Deletes all keys and values from PlayerPrefs.
        /// </summary>
        public void DeleteAll()
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save();
        }

        /// <summary>
        /// Checks if a specific key exists in PlayerPrefs.
        /// </summary>
        /// <param name="key">The key to check.</param>
        /// <returns>True if the key exists; otherwise, false.</returns>
        public bool HasKey(string key)
        {
            string finalKey = _useEncryption ? EncryptKey(key) : key;
            return PlayerPrefs.HasKey(finalKey);
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
        /// Encrypts a value using a base64 encoding scheme.
        /// </summary>
        /// <param name="value">The value to encrypt.</param>
        /// <returns>The encrypted value.</returns>
        private string EncryptValue(string value)
        {
            return Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(value));
        }

        internal void SetFloat(string mUSIC_VOLUME_KEY, float volume)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}