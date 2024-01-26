using Jets;
using UnityEngine;
using UnityEngine.UI;

namespace UI.AimReticle
{
    public class UI_AimReticleObject : MonoBehaviour
    {
        private bool _initialized = false;
        private Camera _camera;        
        private Jet _jet;
        
        private Sprite _reticleSprite;        
        private Image _reticle;
        
        public Sprite ReticleSprite { set => _reticleSprite = value; }        
        public void CreateReticle(Jet jet)
        {
            RectTransform t = new GameObject("Reticle").AddComponent<RectTransform>();
            Image i = t.gameObject.AddComponent<Image>();
            i.sprite = _reticleSprite;
            t.SetParent(transform);
            
            _camera = Camera.main;   
            
            _jet = jet;
            _reticle = i;
            
            _initialized = true;            
        }
        
        private void Start()
        {            
            
            _reticle.rectTransform.localScale = Vector3.one *  0.1f;
        }

        private void LateUpdate()
        {
            if (!_initialized ) return;
            if (_jet.IsDestroyed)
            {
                _reticle.rectTransform.position = -Vector3.one;
                return;
            }
            Vector3 point = _camera.WorldToScreenPoint(_jet.GetColliderWorldPosition());
            point.z = 0;
            _reticle.rectTransform.position = point;
            Vector2 screenSize = new Vector2(Screen.width, Screen.height);

            if (_jet != null) _reticle.rectTransform.localScale = (_jet.GetComponent<Collider>().bounds.size / screenSize).magnitude * Vector3.one *10;
        }
    }
}