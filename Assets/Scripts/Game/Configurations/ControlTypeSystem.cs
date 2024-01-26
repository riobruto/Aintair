using UnityEngine;

namespace Configurations
{
    public sealed class ControlTypeSystem : MonoBehaviour
    {
        [SerializeField] private float _mouseSensitivity;
        
        public float MouseSensitivity
        {
            get { return _mouseSensitivity; }
        }

        private static ControlTypeSystem _instance;

        public static ControlTypeSystem Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<ControlTypeSystem>();

                    if (_instance == null)
                    {
                        throw new UnityEngine.UnityException("There is no ControlTypeSystem in the scene");
                    }
                }
                return _instance;
            }
        }

        [SerializeField] private ControlType _controlType;

        public ControlType ControlType
        {
            get { return _controlType; }
        }

        public void SetControlType(ControlType controlType)
        {
            _controlType = controlType;
        }

        public void SetSensitivity(float value)
        {
            _mouseSensitivity = value;
        }
    }
}