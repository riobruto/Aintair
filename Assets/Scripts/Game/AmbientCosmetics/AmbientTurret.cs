using Audio;
using GameSystem;
using UnityEngine;

public class AmbientTurret : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particle;
    private ParticleSystem.MainModule _module;
    [SerializeField] private AudioClip[] _clip;
    private float _timer;
    private float _timeUntil;

    private Jets.Jet jet;

    private void Start()
    {
        jet = JetsSytem.Instance.JetList[Random.Range(0, JetsSytem.Instance.JetList.Count)];
        _timeUntil = Random.Range(5f, 20f);
        _module = _particle.main;
    }

    private void Update()
    {
        _timer += Time.deltaTime;

        if (_timer > _timeUntil)
        {
            _particle.Stop();
            AudioClip clip = _clip[Random.Range(0, _clip.Length)];
            _module.duration = clip.length / 6;

            jet = JetsSytem.Instance.JetList[Random.Range(0, JetsSytem.Instance.JetList.Count)];
            _timeUntil = Random.Range(5f, 20f);
            _timer = 0;
            
            if (!jet.IsDestroyed)
            {
                AudioSourceExtention.PlayClipAtPoint(clip, transform.position, .5f, 1 ,1,SoundMixerSystem.Instance.ExplosionsMixer);
                _particle.Play();
            }
        }
        
        transform.LookAt(jet.transform.position);
    }
}