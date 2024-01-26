using Audio;
using GameSystem;
using Interfaces;
using UnityEngine;

namespace Entities
{
    public class SurfaceHitEntity : MonoBehaviour, IHittableFromWeapon, IHittableFromBomb
    {
        [SerializeField] private AudioClip _clip;

        public void OnBombHit(BombHitPayload payload)
        {

        }

        void IHittableFromWeapon.OnHit(HitPayload payload)
        {
            GameObject impact = ParticleBufferSystem.Instance.ParticleRingBuffer.GetNext();
            impact.transform.position = payload.RaycastHit.point;
            impact.transform.rotation = Quaternion.LookRotation(payload.RaycastHit.normal);
            impact.SetActive(true);
            ParticleSystem p = impact.GetComponent<ParticleSystem>();
            p.Play();
            AudioSourceExtention.PlayClipAtPoint(_clip, Camera.main.transform.position, 0, 1f);
        }
    }
}