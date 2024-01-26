using GameSystem;
using Interfaces;
using UnityEngine;

namespace AmbientCosmetics
{
    public class FanImpact : MonoBehaviour, IHittableFromWeapon
    {
        void IHittableFromWeapon.OnHit(HitPayload payload)
        {

            GetComponent<Rigidbody>().AddForce(payload.RaycastHit.point - payload.RayOrigin, ForceMode.Impulse);
            
        }

       
    }
}