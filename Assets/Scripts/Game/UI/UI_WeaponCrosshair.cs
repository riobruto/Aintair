using AA.Weapon;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UI_WeaponCrosshair : UI_WeaponController
    {
        [SerializeField] private Image _image;
        private Camera _camera;
        

        private void Start()
        {
            _weapon = FindObjectOfType<AAWeapon>();
            _camera = Camera.main;
            _image = GetComponent<Image>();
        }

        private void Update()
        {
            Vector3 point;         

            point = _camera.WorldToScreenPoint(_weapon.GetAimRay().GetPoint(1000));
            _image.rectTransform.anchoredPosition = Vector2.Lerp(_image.rectTransform.anchoredPosition, point, Time.deltaTime * 10);
        }
    }
}