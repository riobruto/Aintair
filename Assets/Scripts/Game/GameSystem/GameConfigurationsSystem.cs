using Data;
using System.Collections;
using UnityEngine;

namespace GameSystem
{
    public class GameConfigurationsSystem : MonoBehaviour
    {
        private static GameConfigurationsSystem _instance;

        public static GameConfigurationsSystem Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<GameConfigurationsSystem>();

                    if (_instance == null)
                    {
                        throw new UnityEngine.UnityException("There is no ControlTypeSystem in the scene");
                    }
                }
                return _instance;
            }
        }

        [SerializeField] private float _bombingDistance;
        [SerializeField] private float _bombDamage;

        private UI.UI_Fade _fade;
        private float _maxPlanes = 6;
        private float _lastJetCount;
        private bool _canPlay;

        public bool CanPlay { get => _canPlay; set => _canPlay = value; }
        public float BombDamage { get => _bombDamage; }
        public float BombingDistance { get => _bombingDistance; }

        private IEnumerator Start()
        {
            _fade = FindObjectOfType<UI.UI_Fade>();
            _lastJetCount = 0;
            _canPlay = true;
            BuildingsSystem.Instance.OnBuildingLossEvent += OnLost;
            yield return new WaitForEndOfFrame();
            PlayerData.Commit(x => x);
        }

        private void OnLost(bool hasLost)
        {
            if (!hasLost)
            {
                _fade._targetValue = 0;
                _canPlay = true;
                return;
                
            }
            if (hasLost)
            {
                _fade._targetValue = 1;
                _canPlay = false;

                return;
            }
        }

        private void Update()
        {

            if (_lastJetCount != JetsSytem.Instance.JetCount)
            {
                _lastJetCount = JetsSytem.Instance.JetCount;

                if (_lastJetCount % 10 == 0 && JetsSytem.Instance.JetList.Count < _maxPlanes)
                {
                    JetsSytem.Instance.CreateJet();
                   
                }
            }
        }
    }
}