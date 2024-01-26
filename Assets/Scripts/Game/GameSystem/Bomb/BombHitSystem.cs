using UnityEngine;

namespace GameSystem
{
    public delegate void BombHitDelegate(BombHitPayload payload);

    public class BombHitSystem : MonoBehaviour
    {
        private static BombHitSystem _instance;
        
        public static BombHitSystem Instance
        {
            get
            {
                if (_instance == null) _instance = FindObjectOfType<BombHitSystem>();
                if (_instance == null) _instance = new GameObject("BombHitSystem").AddComponent<BombHitSystem>();
                return _instance;
            }
        }    

        public event BombHitDelegate BombHitEvent;
        public void Hit(BombHitPayload payload)
        {            
            BombHitEvent?.Invoke(payload);
        }
    }
    public class BombHitPayload
    {
        public RaycastHit Hit;
        public float Damage;

        public BombHitPayload(RaycastHit hit, float damage)
        {
            Hit = hit;
            Damage = damage;
        }
    }
}