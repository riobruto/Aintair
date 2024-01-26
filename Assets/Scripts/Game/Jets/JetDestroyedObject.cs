using UnityEngine;

namespace Jets
{
    public class JetDestroyedObject : MonoBehaviour
    {
        [SerializeField] private Rigidbody[] _rbs;
        
        private Vector3[] _originPosition;
        private Quaternion[] _originRotation;

        private void OnEnable()
        {
            
            _rbs = GetComponentsInChildren<Rigidbody>();
            
            _originPosition = new Vector3[_rbs.Length];
            _originRotation = new Quaternion[_rbs.Length];            
            
            for (int i = 0; i < _rbs.Length; i++)
            {

                _rbs[i].gameObject.SetActive(false);
                _originPosition[i] = _rbs[i].transform.localPosition;
                _originRotation[i] = _rbs[i].transform.localRotation;
            }
        }

        public void ResetPositionAndVelocity()
        {
            for (int i = 0; i < _rbs.Length; i++)
            {
                _rbs[i].transform.parent = transform;
                _rbs[i].gameObject.SetActive(false);
                _rbs[i].transform.localPosition = _originPosition[i];
                _rbs[i].transform.localRotation = _originRotation[i];
                _rbs[i].velocity = Vector3.zero;
            }
        }

        public void InitializeDestroy(Vector3 velocity)
        {
            foreach (Rigidbody rb in _rbs)
            {
                //rb.transform.parent = null;
                rb.gameObject.SetActive(true);
                rb.AddTorque(velocity);
                rb.velocity = velocity;
            }
            Destroy(gameObject, 10f);
        }
    }
}