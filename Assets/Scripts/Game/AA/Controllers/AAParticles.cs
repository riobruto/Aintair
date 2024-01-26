using AA.Weapon;
using Interfaces;

using UnityEngine;

namespace AA.Controllers
{
    public class AAParticles : MonoBehaviour, IControllerFromWeapon
    {
        private ParticleSystem _particle;
        [SerializeField] private ParticleSystem _heatParticle;
        private AAWeapon _weapon;

        private void Start()
        {
            _particle = gameObject.GetComponentInChildren<ParticleSystem>();
            _weapon = gameObject.GetComponentInParent<AAWeapon>();
        }

        void IControllerFromWeapon.OnWeaponChange(AAWeaponStates states)
        {
            switch (states)
            {
                case AAWeaponStates.END_SHOOT:
                    _particle.transform.position = _weapon.ShootPosition.position;
                    _particle.transform.rotation = Quaternion.LookRotation(_weapon.AimPoint);
                    _particle.Play();
                    break;

                case AAWeaponStates.OVERHEATED:
                    _heatParticle.Play();
                    break;
            }
        }
    }
}