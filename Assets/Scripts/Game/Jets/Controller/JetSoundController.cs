using GameSystem;
using UnityEngine;

namespace Jets.Controller
{
    public class JetSoundController : JetController
    {
        private AudioSource _source;
        private AudioSource _engineSource;
        [SerializeField] private AudioClip[] _impactClips;
        [SerializeField] private AudioClip _engineClip;

        private float engineVolume;

        internal override void SetUp()
        {
            _source = gameObject.AddComponent<AudioSource>();
            _engineSource = gameObject.AddComponent<AudioSource>();
            _engineSource.minDistance = _source.minDistance = 500;
            _engineSource.maxDistance = _source.maxDistance = 2000;
            _engineSource.clip = _engineClip;
            _engineSource.spatialBlend = 1;
            _engineSource.outputAudioMixerGroup = SoundMixerSystem.Instance.PlanesMixer;
            _source.outputAudioMixerGroup = SoundMixerSystem.Instance.PlanesMixer;
            _engineSource.loop = true;
            _engineSource.Play();
            _source.volume = 0.5f;
        }

        internal override void UpdateController()
        {
            _engineSource.volume = Mathf.Lerp(_engineSource.volume, engineVolume, Time.deltaTime);
        }

        internal override void OnJetChangeState(JetState state)
        {
            switch (state)
            {
                case JetState.INITIALIZED:
                    _engineSource.volume = 1;
                    break;

                case JetState.RECIEVING_DAMAGE:
                    _source.PlayOneShot(_impactClips[Random.Range(0, _impactClips.Length)]);
                    break;

                case JetState.DESTROYED:
                    engineVolume = 0;
                    _engineSource.Stop();
                    break;

                case JetState.RESETTED:
                    engineVolume = 1;
                    _engineSource.Play();
                    break;
            }
        }
    }
}