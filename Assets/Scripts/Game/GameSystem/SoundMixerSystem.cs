
using UnityEngine;
using UnityEngine.Audio;

namespace GameSystem
{
    public class SoundMixerSystem : MonoBehaviour
    {
        private static SoundMixerSystem _instance;

        public static SoundMixerSystem Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<SoundMixerSystem>();
                    if (_instance == null)
                    {
                        throw new UnityEngine.UnityException("Sos boludo??????, no hay un SoundMixerSystem en la escena");
                    }
                }
                return _instance;
            }
            
        }

        public AudioMixerGroup GunMixer { get => _gunMixer; }
        public AudioMixerGroup PlanesMixer { get => _planesMixer; }
        public AudioMixerGroup ExplosionsMixer { get => _explosionsMixer; }
        public AudioMixerGroup AmbientMixer { get => _ambientMixer; }

        [SerializeField] private AudioMixerGroup _gunMixer;
        [SerializeField] private AudioMixerGroup _planesMixer;
        [SerializeField] private AudioMixerGroup _explosionsMixer;
        [SerializeField] private AudioMixerGroup _ambientMixer;
        [Header("Mixer")]
        [SerializeField] private AudioMixer _mixer;
        public void SetGeneralVolume(float value)
        {
            float logarithmicValue = Mathf.Log(value) * 20;
            _mixer.SetFloat("Master", logarithmicValue);
        }
    }
}