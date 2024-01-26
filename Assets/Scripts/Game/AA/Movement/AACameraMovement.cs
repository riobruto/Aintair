using Configurations;
using GameSystem;
using UnityEngine;

namespace AA
{
    public class AACameraMovement : MonoBehaviour
    {
        private float _pitch;
        private float _yaw;

        private Quaternion _finalRot;

        [SerializeField, Range(5, 20)] private float _speed = 10;
        private float _mouseSensitivity => ControlTypeSystem.Instance.MouseSensitivity;

        private float _pitchAxis;
        private float _yawAxis;
        private float _aimingMultiplier;
        public float YawDelta => _pitchAxis;
        public float PitchDelta => _yawAxis;

        private void Update()
        {
            if (!GameConfigurationsSystem.Instance.CanPlay) return;
            
            _aimingMultiplier = Input.GetKey(KeyCode.Mouse1) ? .5f : 1f;

            //Define Control Input Logic
            if (ControlTypeSystem.Instance.ControlType == ControlType.KEYBOARD)
            {
                _pitchAxis = Input.GetAxis("Horizontal") * Time.deltaTime * _aimingMultiplier;
                _yawAxis = Input.GetAxis("Vertical") * Time.deltaTime * _aimingMultiplier;
            }
            if (ControlTypeSystem.Instance.ControlType == ControlType.MOUSE)
            {
                _pitchAxis = (Input.GetAxis("Mouse X") * _mouseSensitivity);
                _yawAxis = (Input.GetAxis("Mouse Y") * _mouseSensitivity);
            }
            //Apply Values
            _pitch += _pitchAxis * _speed * Time.deltaTime * _aimingMultiplier;
            _yaw += _yawAxis * _speed * Time.deltaTime * _aimingMultiplier;
            //Limits
            _pitch = Mathf.Clamp(_pitch, -50, 50);
            _yaw = Mathf.Clamp(_yaw, -5, 50);

            _finalRot = Quaternion.Euler(_yaw, _pitch, 0);

            //move thing
            transform.localRotation = _finalRot;
        }
    }
}