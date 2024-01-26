using AA.Controllers;
using GameSystem;

using UnityEngine;

namespace Invaders
{
    public class BombEntity : MonoBehaviour
    {
        private float _radius = 100;
        private float _damage => GameSystem.GameConfigurationsSystem.Instance.BombDamage;
        private Rigidbody _rigidbody;
        [SerializeField] private AudioClip[] _clips;
        
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }
        
        private void OnCollisionEnter(Collision collision)
        {
            RaycastHit[] hits = Physics.SphereCastAll(collision.GetContact(0).point, _radius, transform.forward);

            foreach (RaycastHit h in hits)
            {
                BombHitSystem.Instance.Hit(new BombHitPayload(h, _damage));
            }
            
            //visuals and sonoric
            GameObject particle = BombParticleBufferSystem.Instance.ParticleRingBuffer.GetNext();
            particle.SetActive(true);
            particle.transform.position = collision.GetContact(0).point;
            particle.transform.localScale = Vector3.one * 10 * Random.Range(1, 2);
            particle.GetComponent<ParticleSystem>().Play();

            Audio.AudioSourceExtention.PlayClipAtPoint(_clips[Random.Range(0, _clips.Length)], collision.GetContact(0).point, .8f, 1,1,SoundMixerSystem.Instance.ExplosionsMixer);
            FindObjectOfType<AACamera>().TriggerShake(Random.Range(-0.2f, 0));

            gameObject.SetActive(false);
        }

        internal void Initialize()
        {
            gameObject.SetActive(true);
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.angularVelocity = Vector3.zero;
            _rigidbody.AddForce(transform.forward * ( 1000 * Random.Range(2, 5)));
        }
    }
}