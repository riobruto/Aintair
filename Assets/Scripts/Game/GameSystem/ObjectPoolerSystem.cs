using System.Collections.Generic;
using UnityEngine;


public class ObjectPoolerSystem : MonoBehaviour
{
    Dictionary<int, Queue<ObjectInstance>> poolDictionary = new Dictionary<int, Queue<ObjectInstance>>();

    static ObjectPoolerSystem _instance;

    public static ObjectPoolerSystem Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<ObjectPoolerSystem>();
            }
            return _instance;

        }
    }

    public void CreatePool(GameObject prefab, int poolSize)
    {
        int poolKey = prefab.GetInstanceID();
        //Parent para organizar
        GameObject poolHolder = new GameObject(prefab.name + "pool");
        poolHolder.transform.parent = transform;

        if (!poolDictionary.ContainsKey(poolKey))
        {
            poolDictionary.Add(poolKey, new Queue<ObjectInstance>());

            for (int i = 0; i < poolSize; i++)
            {
                ObjectInstance newObject = new ObjectInstance(Instantiate(prefab) as GameObject);
                poolDictionary[poolKey].Enqueue(newObject);

                //Pool Parent
                newObject.SetParent(poolHolder.transform);
            }
        }
    }
    public void ReuseObject(GameObject prefab, Vector3 position, Quaternion rotation, Transform parent)
    {
        int poolKey = prefab.GetInstanceID();

        if (poolDictionary.ContainsKey(poolKey))
        {
            ObjectInstance objectToReuse = poolDictionary[poolKey].Dequeue();
            poolDictionary[poolKey].Enqueue(objectToReuse);
            objectToReuse.Reuse(position, rotation, parent);

        }

    }

    public class ObjectInstance
    {
        public GameObject gameObject;
        Transform transform;
        Transform parent;

        bool hasPoolObject;
        PoolObject poolObject;

        public ObjectInstance(GameObject objectInstance)
        {

            gameObject = objectInstance;
            transform = gameObject.transform;

            gameObject.SetActive(false);
            if (gameObject.GetComponent<PoolObject>() != null)
            {
                hasPoolObject = true;
                poolObject = gameObject.GetComponent<PoolObject>();
            }
        }

        public void Reuse(Vector3 pos, Quaternion rot, Transform parent)
        {
            gameObject.SetActive(true);
            gameObject.transform.position = pos;
            gameObject.transform.rotation = rot;
            gameObject.transform.parent = parent;

            if (hasPoolObject)
            {
                poolObject.OnObjectReuse();
            }

        }

        public void SetParent(Transform parent)
        {
            transform.parent = parent;
        }
    }
}