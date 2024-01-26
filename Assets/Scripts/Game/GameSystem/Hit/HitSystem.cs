using UnityEngine;

namespace GameSystem
{
    public delegate void HitDelegate(HitPayload payload);

    public class HitSystem : MonoBehaviour
    {
        private static HitSystem _hitSystem { get; set; }

        public static HitSystem Instance
        {
            get
            {
                if (_hitSystem == null) _hitSystem = FindObjectOfType<HitSystem>();
                if (_hitSystem == null) _hitSystem = new GameObject("HitSystem").AddComponent<HitSystem>();
                return _hitSystem;
            }
        }

        public event HitDelegate HitEvent;

        public void Hit(HitPayload payload)
        {
            HitEvent?.Invoke(payload);
        }
    }

    public class HitPayload
    {
        public float Distance;
        
        public Vector3 RayOrigin;
        public RaycastHit RaycastHit { get; }    
        public float Damage { get; }

        public HitPayload(Vector3 origin, RaycastHit hit, float damage)
        {
            RayOrigin = origin;
            RaycastHit = hit;            
            Distance = Vector3.Distance(origin, hit.point);
            Damage = damage;
        }
    }
}