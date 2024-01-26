using Configurations;
using GameSystem;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI.Menu
{
    public class UI_InGameOptionMenu : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Slider _volumeSlider;

        [SerializeField] private Slider _sensitivitySlider;
        [SerializeField] private GameObject _menu;

        [SerializeField] private GameObject[] _wholeMenu;
        [SerializeField] private GameObject _exitWarning;
        
        private float _volumeValue;
        private float _sesitivityValue;
        private bool _isMenuActive => _menu.activeSelf;

        private void Start()
        {
            _menu.SetActive(false);
            //   slider = GetComponent<Slider>();
            _volumeSlider.onValueChanged.AddListener(delegate { SetVolumeSlider(); });
            _sensitivitySlider.onValueChanged.AddListener(delegate { SetSensitivitySlider(); });
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Time.timeScale = _isMenuActive ? 1f : 0f;
                GameConfigurationsSystem.Instance.CanPlay = _isMenuActive ? true : false;
                _menu.SetActive(!_isMenuActive);
            }
        }

        private void SetSensitivitySlider()
        {
            _sesitivityValue = _sensitivitySlider.value;
            ControlTypeSystem.Instance.SetSensitivity(_sesitivityValue);
            
        }
        public void SetVolumeSlider()
        {
            _volumeValue = _volumeSlider.value;
            SoundMixerSystem.Instance.SetGeneralVolume(_volumeValue);
        }
        public void ResetLevel()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene("Game");
        }

        public void ShowExitWarning()
        {
            foreach (GameObject item in _wholeMenu)
            {
                item.SetActive(false);
            }
            
            _exitWarning.SetActive(true);
        }

        public void CloseExitWarning()
        {
            foreach (GameObject item in _wholeMenu)
            {
                item.SetActive(true);
            }
            _exitWarning.SetActive(false);
        }
        public void ExitGame()
        {
            Application.Quit();
        }
    }
}