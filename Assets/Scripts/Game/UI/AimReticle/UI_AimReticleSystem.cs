using GameSystem;
using Jets;
using UnityEngine;

namespace UI.AimReticle
{
    public class UI_AimReticleSystem : MonoBehaviour
    {
        [SerializeField] private Sprite _reticleTexture;

        private static UI_AimReticleSystem _instance;

        public static UI_AimReticleSystem Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<UI_AimReticleSystem>();
                    if (_instance == null)
                    {
                        throw new UnityEngine.UnityException("Sos boludo??????, no hay un UI_AimReticleSystem en la escena");
                    }
                }
                return _instance;
            }
            
        }
        /*
        private void Start()
        {
            CreateReticles(JetsSytem.Instance.JetList.ToArray());
        }
        
        public void CreateReticles(Jet[] jetArray)
        {
            foreach (Jet jet in jetArray)
            {
                UI_AimReticleObject reticle = gameObject.AddComponent<UI_AimReticleObject>();
                reticle.ReticleSprite = _reticleTexture;
                reticle.CreateReticle(jet);
            }
        }

        public void CreateReticle(Jet jet)
        {
            UI_AimReticleObject reticle = gameObject.AddComponent<UI_AimReticleObject>();
            reticle.ReticleSprite = _reticleTexture;
            reticle.CreateReticle(jet);
        }*/
    }
}