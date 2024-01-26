using UnityEngine;

namespace Assets.Scripts
{
    public class Loops : MonoBehaviour
    {
        public Transform[] _transforms;
        public Transform parent;

        private void Start()
        {
        }

        private void Update()
        {
        }

        public int _x, _y, _z;

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;

            for (int x = 0; x < _x; x++)
            {
                for (int y = 0; y < _y; y++)
                {
                    for (int z = 0; z < _z; z++) { Gizmos.DrawCube(new Vector3(x, y, z), Vector3.one * .25f); }
                }
            }
        }
    }
}