using GameSystem;
using Interfaces;
using UnityEngine;

namespace Entities
{
    public  class HittableEntity : MonoBehaviour, IHittableFromWeapon
    {
        void IHittableFromWeapon.OnHit(HitPayload payload)
        {
            Debug.Log("Hit");            
        }
    }
}