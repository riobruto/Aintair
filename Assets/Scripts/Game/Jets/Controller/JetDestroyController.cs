using AA.Weapon;
using Audio;
using GameSystem;
using UnityEngine;

namespace Jets.Controller
{
    public class JetDestroyController : JetController
    {
        [SerializeField] private GameObject _jetDestroyed;
        [SerializeField] private GameObject _jetHealthy;
        [SerializeField] private AudioClip _jetDestroyedSound;

        private Vector3 _lastPosition;

        private bool _canDestroy;
        private float _time;
        private float _destroyTime = 1f;
        private Vector3 _point;

        internal override void SetUp()
        {
            _point = FindObjectOfType<AAWeapon>().transform.position;
        }

        internal override void UpdateController()
        {
            /*
            if (_lastPosition != transform.position)
            {
                _lastPosition = transform.position;
            }*/

            if (!_canDestroy) return;

            _time += Time.deltaTime;

            if (_time >= _destroyTime)
            {
                GameObject destroyed = Instantiate(_jetDestroyed);
                destroyed.transform.position = transform.position;
                destroyed.transform.rotation = transform.rotation;
                _jetHealthy.SetActive(false);
                destroyed.GetComponent<JetDestroyedObject>().InitializeDestroy((transform.position - _lastPosition).normalized * 100f);
                _canDestroy = false;
                AudioSourceExtention.PlayClipAtPoint(_jetDestroyedSound, _point, 1, 0, 1, SoundMixerSystem.Instance.ExplosionsMixer);
            }
        }

        internal override void OnJetChangeState(JetState state)
        {
            switch (state)
            {
                case JetState.INITIALIZED:
                    {
                        _jetHealthy.SetActive(true);
                        break;
                    }
                case JetState.DESTROYED:
                    {
                        _time = 0;
                        _canDestroy = true;
                        _lastPosition = transform.position;

                        break;
                    }
                case JetState.RESETTED:
                    {
                        _jetHealthy.SetActive(true);

                        break;
                    }
            }
        }
    }
}