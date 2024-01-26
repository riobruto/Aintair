using AA.Controllers;
using Jets;
using System.Collections.Generic;
using UnityEngine;

namespace GameSystem
{
    public class JetsSytem : MonoBehaviour
    {
        private List<Jet> _jetList = new List<Jet>();
        public List<Jet> JetList { get => _jetList; }

        private static JetsSytem _instance;
        public static JetsSytem Instance { get => _instance; }
        public int OnPathFollowerEvent { get; private set; }

        
        
        private int _jetDestroyedCount = 0;
        public int JetCount { get => _jetDestroyedCount; }

        [SerializeField] private GameObject _jetPrefab;

        private void Awake()
        {
            if (_instance == null) _instance = this;

            Jet[] j = FindObjectsOfType<Jet>();

            foreach (Jet jet in j)
            {
                jet.JetStateEvent += OnJetStateChange;
                jet.PathFollower().OnPathFinishedEvent += OnJetPathFollowerFinished;
                _jetList.Add(jet);
            }
        }

        private void OnJetPathFollowerFinished(Jet jet)
        {
            jet.ResetJet();
        }

        private void OnJetStateChange(JetState state)
        {
            switch (state)
            {
                case JetState.DESTROYED:
                    {
                        _jetDestroyedCount++;
                        FindObjectOfType<AACamera>().TriggerShake(Random.Range(-0.2f, 0));
                        break;
                    }
            }
        }

        public void CreateJet()
        {
            GameObject newJet = Instantiate(_jetPrefab);
            Jet jet = newJet.GetComponent<Jet>();
            jet.Initialize();

            jet.JetStateEvent += OnJetStateChange;
            jet.PathFollower().OnPathFinishedEvent += OnJetPathFollowerFinished;            
            _jetList.Add(jet);
            //UI.AimReticle.UI_AimReticleSystem.Instance.CreateReticle(jet);
        }
    }
}