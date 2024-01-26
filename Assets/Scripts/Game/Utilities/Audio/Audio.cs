using UnityEngine;
using UnityEngine.Audio;

namespace Audio
{
    public class AudioSourceExtention : MonoBehaviour
    {
        public static void PlayClipAtPoint
        (AudioClip clip, Vector3 position, float volume = 1.0f, float spatialBlend = 0.1f, float pitch = 1f, AudioMixerGroup group = null)
        {
            if (clip == null) { Debug.Log("Clip at point was called with null Audioclip"); return; }
            GameObject gameObject = new GameObject("One shot audio");
            gameObject.transform.position = position;
            AudioSource audioSource = (AudioSource)gameObject.AddComponent(typeof(AudioSource));
            if (group != null)
                audioSource.outputAudioMixerGroup = group;
            audioSource.clip = clip;
            audioSource.spatialBlend = spatialBlend;
            audioSource.volume = volume;
            audioSource.pitch = 1.0f;
            audioSource.Play();
            audioSource.minDistance = 500;
            audioSource.maxDistance = 5000;
            Object.Destroy(gameObject, clip.length *
                (Time.timeScale < 0.009999999776482582 ? 0.01f : Time.timeScale));
        }
    }
}