using AA.Weapon;
using Interfaces;
using System.Collections;
using UnityEngine;

namespace AA.Controllers
{
    public class AACamera : MonoBehaviour, IControllerFromWeapon        
    {
       
        [SerializeField] private AnimationCurve _curve;
        [SerializeField] private Transform _shakeTransform;
        [SerializeField] private float _time;      
        void IControllerFromWeapon.OnWeaponChange(AAWeaponStates states)
        {
            if (states == AAWeaponStates.END_SHOOT)
            {
                _time = 0;                
                
            }

        }
        private void LateUpdate()
        {
            _time += Time.deltaTime;
            _shakeTransform.localRotation = Quaternion.Euler(new Vector3(Random.Range(-1, 1), Random.Range(-1, 1), 0) * _curve.Evaluate(_time));
            
            
        }

        public void TriggerShake(float time)
        {
            _time = time;
        }


    }
}