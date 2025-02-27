using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Zenject;
using TriInspector;
using System.Collections.Generic;

namespace SerapKeremZenCoreTools._Game.SaveLoadSystem
{
    /// <summary>
    /// A test class for the SaveManager and LoadManager to verify their functionality in a test scene.
    /// </summary>
    public class SaveLoadSystemTest : MonoBehaviour
    {
        #region Variables

        [Group("UI References")]
        [SerializeField]
        private TMP_InputField _keyInput;

        [SerializeField]
        private TMP_InputField _valueInput;

        [SerializeField]
        private TMP_Dropdown _dataTypeDropdown;

        [SerializeField]
        private TextMeshProUGUI _outputText;

        [Group("UI References/Buttons")]
        [SerializeField]
        private Button _saveButton;

        [SerializeField]
        private Button _loadButton;

        [SerializeField]
        private Button _deleteButton;

        [SerializeField]
        private Button _deleteAllButton;

        private SaveManager _saveManager;
        private LoadManager _loadManager;

        #endregion

        #region Initialization

        /// <summary>
        /// Constructor method for dependency injection.
        /// </summary>
        /// <param name="saveManager">The SaveManager instance to be tested.</param>
        /// <param name="loadManager">The LoadManager instance to be tested.</param>
        [Inject]
        private void Construct(SaveManager saveManager, LoadManager loadManager)
        {
            _saveManager = saveManager;
            _loadManager = loadManager;
        }

        /// <summary>
        /// Initializes the test by setting up UI elements.
        /// </summary>
        private void Start()
        {
            SetupUI();
        }

        #endregion

        #region UI Setup

        /// <summary>
        /// Sets up button listeners and initializes the dropdown options.
        /// </summary>
        private void SetupUI()
        {
            _saveButton.onClick.AddListener(SaveData);
            _loadButton.onClick.AddListener(LoadData);
            _deleteButton.onClick.AddListener(DeleteData);
            _deleteAllButton.onClick.AddListener(DeleteAllData);

            // Dropdown options initialization
            _dataTypeDropdown.ClearOptions();
            var options = new List<TMP_Dropdown.OptionData>
            {
                new TMP_Dropdown.OptionData("String"),
                new TMP_Dropdown.OptionData("Int"),
                new TMP_Dropdown.OptionData("Float"),
                new TMP_Dropdown.OptionData("Bool"),
                new TMP_Dropdown.OptionData("Vector2"),
                new TMP_Dropdown.OptionData("Vector3"),
                new TMP_Dropdown.OptionData("Color"),
                new TMP_Dropdown.OptionData("DateTime")
            };
            _dataTypeDropdown.AddOptions(options);
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Saves data based on the selected data type and input values.
        /// </summary>
        private void SaveData()
        {
            string key = _keyInput.text;
            string value = _valueInput.text;

            try
            {
                switch (_dataTypeDropdown.value)
                {
                    case 0: // String
                        _saveManager.SaveData(key, value);
                        break;
                    case 1: // Int
                        _saveManager.SaveData(key, int.Parse(value));
                        break;
                    case 2: // Float
                        _saveManager.SaveData(key, float.Parse(value));
                        break;
                    case 3: // Bool
                        _saveManager.SaveData(key, bool.Parse(value));
                        break;
                    case 4: // Vector2
                        string[] v2Components = value.Split(',');
                        Vector2 v2 = new Vector2(float.Parse(v2Components[0]), float.Parse(v2Components[1]));
                        _saveManager.SaveVector2(key, v2);
                        break;
                    case 5: // Vector3
                        string[] v3Components = value.Split(',');
                        Vector3 v3 = new Vector3(
                            float.Parse(v3Components[0]),
                            float.Parse(v3Components[1]),
                            float.Parse(v3Components[2]));
                        _saveManager.SaveVector3(key, v3);
                        break;
                    case 6: // Color
                        string[] colorComponents = value.Split(',');
                        Color color = new Color(
                            float.Parse(colorComponents[0]),
                            float.Parse(colorComponents[1]),
                            float.Parse(colorComponents[2]),
                            float.Parse(colorComponents[3]));
                        _saveManager.SaveColor(key, color);
                        break;
                    case 7: // DateTime
                        _saveManager.SaveDateTime(key, System.DateTime.Parse(value));
                        break;
                }
                UpdateOutput($"Saved successfully: {key}");
            }
            catch (System.Exception e)
            {
                UpdateOutput($"Error saving data: {e.Message}");
            }
        }

        /// <summary>
        /// Loads data based on the selected data type and input key.
        /// </summary>
        private void LoadData()
        {
            string key = _keyInput.text;

            try
            {
                object loadedValue = null;
                switch (_dataTypeDropdown.value)
                {
                    case 0: // String
                        loadedValue = _loadManager.LoadData<string>(key);
                        break;
                    case 1: // Int
                        loadedValue = _loadManager.LoadData<int>(key);
                        break;
                    case 2: // Float
                        loadedValue = _loadManager.LoadData<float>(key);
                        break;
                    case 3: // Bool
                        loadedValue = _loadManager.LoadData<bool>(key);
                        break;
                    case 4: // Vector2
                        loadedValue = _loadManager.LoadVector2(key);
                        break;
                    case 5: // Vector3
                        loadedValue = _loadManager.LoadVector3(key);
                        break;
                    case 6: // Color
                        loadedValue = _loadManager.LoadColor(key);
                        break;
                    case 7: // DateTime
                        loadedValue = _loadManager.LoadDateTime(key);
                        break;
                }
                UpdateOutput($"Loaded value: {loadedValue}");
                _valueInput.text = loadedValue?.ToString() ?? "null";
            }
            catch (System.Exception e)
            {
                UpdateOutput($"Error loading data: {e.Message}");
            }
        }

        /// <summary>
        /// Deletes a specific key from PlayerPrefs.
        /// </summary>
        private void DeleteData()
        {
            string key = _keyInput.text;
            _saveManager.DeleteKey(key);
            UpdateOutput($"Deleted key: {key}");
        }

        /// <summary>
        /// Deletes all keys and values from PlayerPrefs.
        /// </summary>
        private void DeleteAllData()
        {
            _saveManager.DeleteAll();
            UpdateOutput("All data deleted");
        }

        #endregion

        #region Utility Methods

        /// <summary>
        /// Updates the output text and logs the message.
        /// </summary>
        /// <param name="message">The message to display and log.</param>
        private void UpdateOutput(string message)
        {
            _outputText.text = message;
            Debug.Log($"[SaveLoadTest] {message}");
        }

        #endregion

        #region Cleanup

        /// <summary>
        /// Cleans up button listeners when the object is destroyed.
        /// </summary>
        private void OnDestroy()
        {
            if (_saveButton) _saveButton.onClick.RemoveListener(SaveData);
            if (_loadButton) _loadButton.onClick.RemoveListener(LoadData);
            if (_deleteButton) _deleteButton.onClick.RemoveListener(DeleteData);
            if (_deleteAllButton) _deleteAllButton.onClick.RemoveListener(DeleteAllData);
        }

        #endregion
    }
}