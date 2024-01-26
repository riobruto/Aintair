using Data;
using TMPro;
using UnityEngine;

namespace UI
{
    [AddComponentMenu("Game/UI/Record")]
    public class UI_Record : MonoBehaviour
    {
        private TextMeshProUGUI _text;

        private void Awake()
        {
            PlayerData.CommitEvent += OnPlayerData;
            _text = GetComponent<TextMeshProUGUI>();
        }

        private void OnPlayerData(PlayerData data)
        {
            _text.text = $"Your last record is {data.KillCount}";
        }
    }
}