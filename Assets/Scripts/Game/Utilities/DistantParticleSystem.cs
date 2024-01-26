using Audio;
using GameSystem;
using System.Threading.Tasks;
using UnityEngine;

namespace Utilities
{
    public class DistantParticleSystem : MonoBehaviour
    {
        public static DistantParticleSystem Instance => _instance;
        private static DistantParticleSystem _instance;
        
        [SerializeField] private AudioClip _clip;

        private void Start()
        {
            if (_instance != null)
                Destroy(gameObject);
            _instance = this;
        }
        public void Play(Vector3 point, Quaternion rotation, int delay)
        {
            PlayAsync(point, rotation, delay);
        }    
        
        private async void PlayAsync(Vector3 point, Quaternion rotation, int delay)
        {
            await Task.Delay(delay);
            
            GameObject impact = ParticleBufferSystem.Instance.ParticleRingBuffer.GetNext();
            impact.transform.position = point;
            impact.transform.rotation = rotation;
            impact.SetActive(true);
            impact.GetComponent<ParticleSystem>().Play();
            AudioSourceExtention.PlayClipAtPoint(_clip, point, .8f, 0, Random.Range(0.9f, 1.1f), SoundMixerSystem.Instance.ExplosionsMixer);
            
        }
    }
}