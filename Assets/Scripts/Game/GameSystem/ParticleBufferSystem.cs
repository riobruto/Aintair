using System.Collections.Generic;
using UnityEngine;

namespace GameSystem
{
    public class ParticleBufferSystem : MonoBehaviour
    {
        [SerializeField] private GameObject particlePrefab;
        [SerializeField] private int _amount = 10;     
        
        public GameObject GetParticle() => particlePrefab;
        public static ParticleBufferSystem Instance { get; private set; }
        public CircularBuffer<GameObject> ParticleRingBuffer;

        private void Awake()
        {
            Instance = this;
            
            GameObject[] particle = new GameObject[_amount];
            for (int i = 0; i < particle.Length; i++)
            {
                particle[i] = Instantiate(particlePrefab);
                particle[i].SetActive(false);

                particle[i].hideFlags = HideFlags.HideInHierarchy;

            }

            ParticleRingBuffer = new CircularBuffer<GameObject>(particle);
        }
    }
}