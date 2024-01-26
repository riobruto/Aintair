using AA.Weapon;
using Interfaces;
using UnityEngine;

namespace AA.Controllers
{
    public class AAWeaponController : MonoBehaviour
    {
        private AAWeapon _w;
        private IControllerFromWeapon[] _controllers;
        private void Start()
        {
            _w = FindObjectOfType<AAWeapon>();
            _w.OnWeaponStateChange += OnWeaponStateChanged;
            _controllers = GetComponents<IControllerFromWeapon>();
        }

        private void OnWeaponStateChanged(AAWeaponStates state)
        {
            foreach (IControllerFromWeapon weaponController in _controllers)
            {
                weaponController.OnWeaponChange(state);
            }
        }      

    }
}