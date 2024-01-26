using GameSystem;
using Interfaces;
using UnityEngine;

namespace AA
{
    public class AADamageAndHealthSystem : MonoBehaviour, IHittableFromBomb
    {
        private int _healths;
        private int _maxHealths = 3;
        private bool _isDead;
        private void Start()
        {
            _healths = _maxHealths;
        }
        void IHittableFromBomb.OnBombHit(BombHitPayload payload)
        {
            if (_isDead) return;
            _healths--;
            if (_healths <= 0)
            {
                _isDead = true;                
            }
        }


        
    }
}