using GameSystem;
using Interfaces;
using UnityEngine;

namespace Buildings
{
    public delegate void BuildingDelegate(BuildingState state);

    public class Building : MonoBehaviour, IHittableFromBomb, IHittableFromWeapon
    {
        public event BuildingDelegate BuildingStateEvent;

        private float _health = 1000;
        private bool _isDestroyed = false;
        private float _recoverValue = 10f;

        private Material _mat;

        //Damage from missiles
        private void Start()
        {
            _mat = gameObject.GetComponent<Renderer>().material;
        }

        void IHittableFromBomb.OnBombHit(BombHitPayload payload)
        {
            ManageDamage(payload.Damage);
        }

        //Damage from AntiAir
        void IHittableFromWeapon.OnHit(HitPayload payload)
        {
            ManageDamage(payload.Damage/4);
        }

        private void Update()
        {
            if (_isDestroyed) return;
            _health += _recoverValue * Time.deltaTime;
            _mat.SetColor("_emissiveColor", Color.Lerp( Color.red, Color.green, _health / 1000));
          
            //_mat.SetFloat("_destructedBlend", Mathf.Lerp(1, 0, _health / 1000));
        }

        private void ManageDamage(float damage)
        {
            if (_isDestroyed) return;

            _health -= damage;

            if (_health < 0)
            {
                _mat.SetColor("_emissiveColor", Color.black);
                _mat.SetFloat("_destructedBlend", 1);

                Debug.Log("Building destroyed");
                NotifyEvent(BuildingState.DESTROYED);
                _isDestroyed = true;
                return;
            }

            NotifyEvent(BuildingState.RECIEVING_DAMAGE);
        }

        private void NotifyEvent(BuildingState state)
        {
            BuildingStateEvent.Invoke(state);
        }
    }
}