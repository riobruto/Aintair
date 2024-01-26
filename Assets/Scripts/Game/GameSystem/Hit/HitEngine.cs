using Interfaces;
using System;
using System.Collections;
using UnityEngine;

namespace GameSystem
{
    public class HitEngine : MonoBehaviour
    {
        private void OnEnable()
        {
            HitSystem.Instance.HitEvent += OnHit;
        }

        private void OnHit(HitPayload payload)
        {
            if (payload.RaycastHit.transform == null) return;

            foreach (IHittableFromWeapon hittable in payload.RaycastHit.transform.GetComponents<IHittableFromWeapon>())
            {
                hittable.OnHit(payload);
            }

        }
    }
}