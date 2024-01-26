using AA.Weapon;
using Interfaces;
using AA;
using System.Threading.Tasks;
using UnityEngine;
using GameSystem;


namespace AA.Controllers
{
    public class AASound : MonoBehaviour, IControllerFromWeapon
    {
        [Header("Fire")]
        [SerializeField] private AudioClip[] _clip;

        [SerializeField] private AudioClip _overheat;

        [SerializeField] private AudioClip[] _shellclip;

        [Header("Reload")]
        [SerializeField] private AudioClip _reloadStartclip;

        [SerializeField] private AudioClip _reloadEndclip;
        [SerializeField] private AudioClip _reloadFailclip;

        [Header("Movement")]
        [SerializeField] private AudioClip _yawClip;

        [SerializeField] private AudioClip _pitchClip;
        [SerializeField] private float _acceleration = 0.2f;

        private AudioSource _shellAudioSource;
        private AudioSource _yawScreechAudioSource;
        private AudioSource _pitchScreechAudioSource;
        private AudioSource _fireAudioSource;

        private AAMovement _movement;

        private void Start()
        {
            _movement = FindObjectOfType<AAMovement>();
            _fireAudioSource = gameObject.AddComponent<AudioSource>();
            _shellAudioSource = gameObject.AddComponent<AudioSource>();
            _yawScreechAudioSource = gameObject.AddComponent<AudioSource>();
            _pitchScreechAudioSource = gameObject.AddComponent<AudioSource>();
            _shellAudioSource.spatialBlend = .9f;
            //definir MixerGropus
            _shellAudioSource.outputAudioMixerGroup = SoundMixerSystem.Instance.GunMixer;
            _yawScreechAudioSource.outputAudioMixerGroup = SoundMixerSystem.Instance.GunMixer;
            _pitchScreechAudioSource.outputAudioMixerGroup = SoundMixerSystem.Instance.GunMixer;
            _fireAudioSource.outputAudioMixerGroup = SoundMixerSystem.Instance.GunMixer;
            //definir parametros de los loops
            _yawScreechAudioSource.clip = _yawClip;
            _pitchScreechAudioSource.clip = _pitchClip;
            _yawScreechAudioSource.loop = true;
            _yawScreechAudioSource.Play();
            _pitchScreechAudioSource.loop = true;
            _pitchScreechAudioSource.Play();
        }

        void IControllerFromWeapon.OnWeaponChange(AAWeaponStates states)
        {
            switch (states)
            {
                case AAWeaponStates.END_SHOOT:
                    PlayFireSound();
                    break;

                case AAWeaponStates.BEGIN_RELOAD:
                    PlaySound(_reloadStartclip);
                  
                    break;

                case AAWeaponStates.END_RELOAD:

                    PlaySound(_reloadEndclip);
                    break;

                case AAWeaponStates.FAIL_SHOOT:
                    PlaySound(_reloadFailclip);
                    break;

                case AAWeaponStates.OVERHEATED:
                    PlaySound(_overheat);
                    break;
            }
        }

        private void Update()
        {
            /*
           float y = Mathf.Clamp(Mathf.Abs(_movement.YawDelta), .0f, 1);
           float p = Mathf.Clamp(Mathf.Abs(_movement.PitchDelta), .0f, 1);

          _yawScreechAudioSource.pitch = Mathf.Lerp(_yawScreechAudioSource.pitch, y, _acceleration/2* Time.deltaTime);
           _pitchScreechAudioSource.pitch = Mathf.Lerp(_pitchScreechAudioSource.pitch, p, _acceleration / 2 * Time.deltaTime);
           */
            _yawScreechAudioSource.volume = 0; /*Mathf.Clamp(Mathf.Lerp(_yawScreechAudioSource.volume, Mathf.Abs(_movement.YawDelta), _acceleration * Time.deltaTime), 0.02f, .2f);*/
            _pitchScreechAudioSource.volume = 0; //Mathf.Clamp(Mathf.Lerp(_pitchScreechAudioSource.volume, Mathf.Abs(_movement.PitchDelta), _acceleration * Time.deltaTime), 0.02f, .2f);
        }

        private void PlaySound(AudioClip clip)
        {
            _fireAudioSource.PlayOneShot(clip);
        }

        private async void PlayFireSound()
        {
            _fireAudioSource.volume = _fireAudioSource.pitch = Random.Range(.9f, 1.1f);
            _fireAudioSource.PlayOneShot(_clip[Random.Range(0, _clip.Length)]);
            await Task.Delay(Random.Range(250, 500));
            _shellAudioSource.volume = Random.Range(.9f, 1.1f);
            _shellAudioSource.PlayOneShot(_shellclip[Random.Range(0, _shellclip.Length)]);
            Task.CompletedTask.Wait(10);
        }
    }
}