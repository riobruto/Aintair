using PathCreation;
using System.Collections.Generic;
using UnityEngine;

namespace Jets
{
    public class JetPathFollower : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private List<PathCreator> _paths = new List<PathCreator>();
        [SerializeField] private float _speed = 25;

        private PathCreator _currentPath;
        private EndOfPathInstruction endOfPathInstruction = EndOfPathInstruction.Stop;
        private float _distanceTravelled;
        public event System.Action<Jet> OnPathFinishedEvent;
        private Jet _jet;

        
        private void Start()
        {
            _jet = GetComponent<Jet>();


            if (_paths != null)
            {

                _currentPath = GetNewPath();             
                _currentPath.Taken = true;
            }
            _distanceTravelled = Random.Range(0, _currentPath.path.length);
        }

        private void Update()
        {

            if (_paths != null || _currentPath != null)
            {
                _distanceTravelled += _speed * Time.deltaTime;

                transform.position = _currentPath.path.GetPointAtDistance(_distanceTravelled, endOfPathInstruction);
                transform.rotation = _currentPath.path.GetRotationAtDistance(_distanceTravelled, endOfPathInstruction);

                if (_distanceTravelled >= _currentPath.path.length)
                {
                    _distanceTravelled = 0;
                    
                    _currentPath.Taken = false;
                    _currentPath = GetNewPath();
                    _currentPath.Taken = true;

                    OnPathFinishedEvent?.Invoke(_jet);
                }
            }
        }

        // If the path changes during the game, update the distance travelled so that the follower's position on the new path
        // is as close as possible to its position on the old path
       
        private PathCreator GetNewPath()
        {
            List<PathCreator> avalaiblePaths = _paths.FindAll(p => !p.Taken && p != _currentPath);

            if (avalaiblePaths.Count == 0)
            {
                return _currentPath;
            }
            int index = Random.Range(0, avalaiblePaths.Count);
            return avalaiblePaths[index];
        }
    }
}