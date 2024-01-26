using AA.Weapon;
using UnityEngine;

namespace UI
{
    public abstract class UI_WeaponController : MonoBehaviour
    {
        internal AAWeapon _weapon;
        

        // Use this for initialization
        private void Start()
        {
            _weapon = FindObjectOfType<AAWeapon>();
            OnStartUI();
        }

        protected virtual void OnStartUI()
        {
        }

        private void LateUpdate()
        {
            OnLateUpdateUI();
        }

        protected virtual void OnLateUpdateUI()
        {
        }
    }
}