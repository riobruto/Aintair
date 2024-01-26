using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UI_WeaponAmmo : UI_WeaponController
    {
        [SerializeField] private TextMeshProUGUI _text;

        protected override void OnStartUI()
        {
            _text = GetComponent<TextMeshProUGUI>();
        }

        protected override void OnLateUpdateUI()
        {
            if (_weapon.IsReloading)
            {
                _text.text = "<color=red>---</color>";
                return;
            }
            _text.color = _weapon.Ammo < 20 ? AlertColor() : Color.white;
            _text.text = _weapon.Ammo.ToString();
        }

        private Color AlertColor()
        {
            return Color.Lerp(Color.red, Color.white, Mathf.Abs(Mathf.Sin(Time.time*5)));

        }
    }
}