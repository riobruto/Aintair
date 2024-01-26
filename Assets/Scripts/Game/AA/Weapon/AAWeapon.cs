using Configurations;
using GameSystem;
using System.Collections;
using UnityEngine;
using Utilities;

namespace AA.Weapon
{
    public delegate void WeaponStateDelegate(AAWeaponStates state);

    public class AAWeapon : MonoBehaviour
    {
        private Vector3 _point;

        [Header("References")]
        [SerializeField] private Transform[] _barrelPositions;

        private float _fireCooldown { get; set; }
        private float _currentCooldown { get; set; }

        private int _barrelIndex = 0;
        private int _ammo;
        private int _maxAmmo = 100;
        private bool _isReloading;

        private float _heat;

        private float _reloadingTime = 3f;
        private bool _overheated => _heat > .85f;
        private bool _lastOverheated;

        [Header("Settings")]
        [SerializeField] private float _sprayAmount = .5f;

        [SerializeField] private AnimationCurve _heatCurve;
        [SerializeField] private float _heatIncrementRate = 100f;
        [SerializeField] private float _heatDecrementRate = 100f;

        private bool _canFire => _ammo > 0 && !_isReloading;
        private bool _canReload => _ammo < _maxAmmo && !_isReloading;
        private Ray _ray { get; set; }

        private const int Damage = 250;

        public event WeaponStateDelegate OnWeaponStateChange;

        //public s
        public Vector3 AimPoint { get => _point; }

        public Transform ShootPosition { get => _barrelPositions[_barrelIndex]; }
        public Transform NextShootPosition { get => _barrelPositions[_barrelIndex]; }
        public int Ammo => _ammo;
        public bool IsReloading => _isReloading;
        public float ReloadingTime => _reloadingTime;
        public float Heat => _heat;

        private void Start()
        {
            _ammo = _maxAmmo;
        }

        private void Update()
        {
            if (!GameConfigurationsSystem.Instance.CanPlay) return;

            _point = _barrelPositions[_barrelIndex].forward * 1000;

            _currentCooldown = Mathf.Clamp(_currentCooldown - Time.deltaTime, 0, _fireCooldown);

            _fireCooldown = _heatCurve.Evaluate(_heat);
            //Overheat Behavior
            if (_lastOverheated != _overheated) { if (_overheated) NotifyState(AAWeaponStates.OVERHEATED); _lastOverheated = _overheated; }

            if (!_fireInputFromControlType) _heat = Mathf.Clamp(_heat - (Time.deltaTime * _heatDecrementRate), 0, 1);

            if (_isReloading) return;
            //Shoot Bergavio
            if (_fireInputFromControlType && _currentCooldown == 0 && !_overheated)
            {
                if (_ammo == 0 && _canReload)
                {
                    StartCoroutine(Reload());
                    return;
                }
                
                if (_canFire)
                {
                    RaycastHit hit;

                    _heat += _heatIncrementRate;

                    OnWeaponStateChange?.Invoke(AAWeaponStates.BEGIN_SHOOT);

                    _ammo--;
                    _barrelIndex = (_barrelIndex + 1) % _barrelPositions.Length;

                    _ray = new Ray(_barrelPositions[_barrelIndex].position, _barrelPositions[_barrelIndex].forward + GetRandomSpray());

                    Debug.DrawRay(_ray.origin, _ray.direction * 1000, Color.red, 1f);

                    if (Physics.Raycast(_ray, out hit))
                    {
                        PlayParticle(hit.point, (int)Vector3.Distance(_ray.origin, hit.point) / 2);
                        HitSystem.Instance.Hit(new HitPayload(_ray.origin, hit, Damage));
                    }
                    else
                    {
                        Vector3 point = _ray.GetPoint(1000);
                        PlayParticle(point, 500);
                    }

                    OnWeaponStateChange?.Invoke(AAWeaponStates.END_SHOOT);
                }
                
                else NotifyState(AAWeaponStates.FAIL_SHOOT);
                _currentCooldown = _fireCooldown;
            }

            if (Input.GetKeyDown(KeyCode.R) && _canReload)
            {
                StartCoroutine(Reload());
            }
        }

        public Ray GetAimRay() => new Ray(_barrelPositions[_barrelIndex].position, _barrelPositions[_barrelIndex].forward);

        private bool _fireInputFromControlType
        {
            get
            {
                if (ControlTypeSystem.Instance.ControlType == ControlType.MOUSE)
                {
                    return Input.GetMouseButton(0);
                }
                if (ControlTypeSystem.Instance.ControlType == ControlType.KEYBOARD)
                {
                    return Input.GetKey(KeyCode.Space);
                }
                else return false;
            }
        }

        private void PlayParticle(Vector3 point, int delay)
        {
            DistantParticleSystem.Instance.Play(point, Quaternion.LookRotation(point), delay);
        }

        private Vector3 GetRandomSpray()
        {
            float _aim = Input.GetKey(KeyCode.Mouse1) ? .05f : .5f;
            return new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0) * _sprayAmount * _fireCooldown * 10 * _aim;
        }

        private IEnumerator Reload()
        {
            _isReloading = true;
            NotifyState(AAWeaponStates.BEGIN_RELOAD);
            yield return new WaitForSeconds(_reloadingTime);
            _ammo = _maxAmmo;
            NotifyState(AAWeaponStates.END_RELOAD);
            _isReloading = false;
            yield return null;
        }

        private void OnDrawGizmos()
        {
            foreach (Transform barrel in _barrelPositions)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawWireSphere(barrel.position, .2f);
            }
        }

        private void NotifyState(AAWeaponStates states)
        {
            OnWeaponStateChange?.Invoke(states);
        }
    }
}