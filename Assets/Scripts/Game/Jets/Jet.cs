using AA.Weapon;
using GameSystem;
using Interfaces;
using UnityEngine;

namespace Jets
{
    public delegate void JetDelegate(JetState state);

    public class Jet : MonoBehaviour, IHittableFromWeapon
    {
        public event JetDelegate JetStateEvent;

        private float _health = 1000;
        private bool _isDestroyed = false;
        private float _radius = 5f;
        private SphereCollider _collider;
        private AAWeapon _weapon;

        public Vector3 GetColliderWorldPosition() => transform.TransformPoint(_collider.center);

        public JetPathFollower PathFollower() => gameObject.GetComponent<JetPathFollower>();

        public bool IsDestroyed => _isDestroyed;

        private void Start()
        {
            _weapon = FindObjectOfType<AAWeapon>();
            _collider = GetComponent<SphereCollider>();
            _collider.radius = _radius;
        }

        public void Initialize() => NotifyEvent(JetState.INITIALIZED);

        private void Update()
        {
            Vector3 pos = new Vector3(0, 0, Vector3.Distance(transform.position, _weapon.transform.position) / 10);
            _collider.center = pos;
        }

        void IHittableFromWeapon.OnHit(HitPayload payload)
        {
            ManageDamage(payload.Damage);
        }

        private void ManageDamage(float damage)
        {
            if (_isDestroyed) return;

            if (_health <= 0)
            {
                NotifyEvent(JetState.DESTROYED);
                _isDestroyed = true;
            }
            NotifyEvent(JetState.RECIEVING_DAMAGE);
            _health -= damage;
        }

        private void NotifyEvent(JetState state)
        {
            JetStateEvent?.Invoke(state);
        }

        public void ResetJet()
        {
            NotifyEvent(JetState.RESETTED);
            _health = 1000;
            _isDestroyed = false;
        }

        private void OnDrawGizmos()
        {
            if (Application.isPlaying)
            {
                Gizmos.color = Color.red;
                Gizmos.matrix = transform.localToWorldMatrix;
                if(_collider)
                Gizmos.DrawSphere(_collider.center, _collider.radius);
            }
        }
    }
}