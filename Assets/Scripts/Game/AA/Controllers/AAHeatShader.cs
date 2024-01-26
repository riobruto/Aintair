using AA.Weapon;
using UnityEngine;

namespace Controllers
{
    public class AAHeatShader : MonoBehaviour
    {
        [SerializeField] private Material _material;
        private AAWeapon _weapon;
        private float _currentHeat;

        private void Start()
        {
            _material = gameObject.GetComponentInParent<Renderer>().material;
            _weapon = gameObject.GetComponentInParent<AAWeapon>();
        }

        private void LateUpdate()
        {
            _material.SetFloat("_heat",Mathf.Clamp(_weapon.Heat*2,0,1.25f));            
        }
    }
}