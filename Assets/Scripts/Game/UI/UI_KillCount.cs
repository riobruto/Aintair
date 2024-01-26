using GameSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UI_KillCount : MonoBehaviour
    {
        private int _killCount;
        
        private TextMeshProUGUI _text;

        private void Start()
        {
            _text = GetComponent<TextMeshProUGUI>();
        }

        private void LateUpdate()
        {
            _killCount = JetsSytem.Instance.JetCount;
            _text.text = _killCount.ToString();
        }
    }
}