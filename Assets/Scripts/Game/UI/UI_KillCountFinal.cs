using GameSystem;
using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace UI
{
   
    public class UI_KillCountFinal : MonoBehaviour
    {
        private int _killCount;
        private TextMeshProUGUI _text;
        
        private void Start()
        {
            _text = GetComponent<TextMeshProUGUI>();
            _text.text = "";
            BuildingsSystem.Instance.OnBuildingLossEvent += OnLost;
        }
        
        private void OnLost(bool state)
        {
            
            _killCount = JetsSytem.Instance.JetCount;
            _text.text = $"Your total score is {_killCount}";
        }
    }
}