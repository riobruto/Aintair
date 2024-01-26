using Buildings;
using GameSystem;
using Interfaces;
using UnityEngine;

namespace Invaders
{
    public delegate void InvaderStateDelegate(InvaderState state);

    public class Invader : MonoBehaviour, IHittableFromWeapon
    {
        public event InvaderStateDelegate InvaderStateEvent;

        [SerializeField] private bool _canFireMissiles;

        private bool _alive;
        private bool _lastAlive;
        private Vector2Int _coords;
        private float _randomTimer;
        private float _timer;
        private bool _active;

        public Vector2Int Coords => _coords;
        public bool Alive => _alive;
        public bool CanFireMissiles { get => _canFireMissiles; set => _canFireMissiles = value; }

        private void Start()
        {
            _randomTimer = Random.Range(0.0f, 10.0f);
            _active = _lastAlive = _alive = true;
        }

        public void OnHit(HitPayload payload)
        {
            if (!_alive) return;
            NotifyTopInvader();
            InvaderSwarmSystem.Instance.InvaderCount--;
            gameObject.SetActive(false);
            _alive = false;
        }

        public void SetPosition(Vector3 position)
        {
            if (!_alive) return;
            transform.position = position;
        }

        private void Update()
        {
            if (!_active)
            {
                return;
            }
            if (_lastAlive != _alive)
            {
                _active = false;
                _lastAlive = _alive;
                NotifyState(InvaderState.DEAD);
                InvaderSwarmSystem.Instance.InvaderCount--;
                NotifyTopInvader();
            }

            if (!_alive)
            {
                return;
            }

            _timer += Time.deltaTime;
            if (_timer >= _randomTimer)
            {
                _randomTimer = Random.Range(10.0f, 15.0f);

                _timer = 0;

                if (_canFireMissiles)
                {
                    BombEntity missile = BombBufferSystem.Instance.BombCircularBuffer.GetNext().GetComponent<BombEntity>();
                    missile.gameObject.SetActive(true);
                    missile.transform.position = transform.position;

                    Building targetBuilding = GetRandomBuilding();
                    //if (targetBuilding != null) missile.SetVelocity(Vector3.zero);
                }
            }
        }

        private Building GetRandomBuilding()
        {
            int r = Random.Range(0, BuildingsSystem.Instance.Buildings.Length);
            Building b = BuildingsSystem.Instance.Buildings[r];
            return b;
        }

        public void SetCoordenates(int x, int y)
        {
            _coords.x = x;
            _coords.y = y;
        }

        private void NotifyTopInvader()
        {
            foreach (Invader invader in InvaderSwarmSystem.Instance.Invaders)
            {
                if (invader.Coords.x == _coords.x && invader.Coords.y == _coords.y + 1)
                {
                    if (invader)
                    {
                        invader.CanFireMissiles = true;
                    }
                }
            }
        }

        private void NotifyState(InvaderState state)
        {
            InvaderStateEvent?.Invoke(state);
        }
    }
}