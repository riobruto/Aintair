using UnityEngine;

namespace AA
{
    public class AAMovement : MonoBehaviour
    {
        private AACameraMovement _camera;
        private Quaternion _final;

        [SerializeField] private Transform _yawTurret;
        [SerializeField] private float _speed;

        private void Start()
        {
            _camera = FindObjectOfType<AACameraMovement>();
        }

        private void Update()
        {
            Quaternion target = _camera.transform.rotation;

            _final = Quaternion.RotateTowards(_final, target, Time.deltaTime * _speed);
            //_final = Quaternion.LookRotation(Vector3.Lerp(movLookDirection, lookDirection, 0.5f));

            //move thing
            transform.localRotation = Quaternion.Euler(0, _final.eulerAngles.y, 0);
            _yawTurret.localRotation = Quaternion.Euler(_final.eulerAngles.x, 0, 0); ;
        }
    }
}