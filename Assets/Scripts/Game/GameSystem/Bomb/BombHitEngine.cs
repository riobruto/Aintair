using Interfaces;
using System;
using System.Collections;
using UnityEngine;

namespace GameSystem
{
    public class BombHitEngine : MonoBehaviour
    {
        private void OnEnable()
        {
            BombHitSystem.Instance.BombHitEvent += OnHit;
        }

        private void OnHit(BombHitPayload payload)
        {
            if (payload.Hit.transform == null) return;

            foreach (IHittableFromBomb hittable in payload.Hit.transform.GetComponents<IHittableFromBomb>())
            {
                hittable.OnBombHit(payload);
            }

        }
    }
}