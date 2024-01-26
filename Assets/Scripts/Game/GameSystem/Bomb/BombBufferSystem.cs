using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombBufferSystem : MonoBehaviour
{
    [SerializeField] private GameObject _bombPrefab;
    [SerializeField] private int _amount = 30;

    public GameObject GetParticle() => _bombPrefab;
    public static BombBufferSystem Instance { get; private set; }
    public CircularBuffer<GameObject> BombCircularBuffer;

    private void Awake()
    {
        Instance = this;

        GameObject[] bombs = new GameObject[_amount];
        
        for (int i = 0; i < bombs.Length; i++)
        {
            bombs[i] = Instantiate(_bombPrefab);
            bombs[i].SetActive(false);
            bombs[i].hideFlags = HideFlags.HideInHierarchy;
        }

        BombCircularBuffer = new CircularBuffer<GameObject>(bombs);
    }
}
