using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Invaders
{
    public class InvaderSwarmSystem : MonoBehaviour
    {

        private static InvaderSwarmSystem _instance;
        public static InvaderSwarmSystem Instance
        {
            get
            {
                if (_instance == null) _instance = new GameObject("InvaderSwarmSystem").AddComponent<InvaderSwarmSystem>();
                if (_instance == null) _instance = FindObjectOfType<InvaderSwarmSystem>();
                return _instance;
            }
        }

        [SerializeField] private int _rows = 5;
        [SerializeField] private int _columns = 11;
        [SerializeField] private Invader[] _prefabs;
        [SerializeField] private float _amplitude;
        [SerializeField] private float _frequency;

        //Create a list of invaders to make them easier to manage
        private Invader[] _invaders;
        private int _invaderCount;
        private int _lastInvaderCount;
        public Invader[] Invaders { get => _invaders;}
        public int InvaderCount { get => _invaderCount; set => _invaderCount = value; }


        

        private void Awake()
        {
            _instance = this;
            CreateGrid();
        }

        private void CreateGrid()
        {
            _invaders = new Invader[_rows * _columns];
            int index = 0;
            float scale = 30f;
            for (int row = 0; row < _rows; row++)
            {
                float w = scale * (_columns - 1);
                float h = scale * (_rows - 1);
                Vector2 center = new Vector2(-w / 2.0f, -h / 2.0f);

                Vector3 rowPos = new Vector3(center.x, center.y + (row * scale), 0);

                for (int col = 0; col < _columns; col++)
                {
                    Invader invader = Instantiate(_prefabs[row], transform);                    
                    invader.SetCoordenates(col, row);                    
                    Vector3 pos = rowPos;
                    pos.x += col * scale;
                    _invaders[index] = invader;                    
                    index++;                    
                    invader.transform.localPosition = pos;
                }
            }
            _lastInvaderCount = _invaderCount = index;
        }


        private void Update()
        {
            if (_invaderCount <= 0) return;
            if (_lastInvaderCount != _invaderCount)
            {
                Debug.Log("invader count changed:" + _invaderCount);
                _lastInvaderCount = _invaderCount;
            }
            for (int i = 0; i < _invaders.Length; i++)
            {
                Vector3 pos = _invaders[i].transform.position;
                pos.y += Mathf.Sin(Time.time * _frequency + (i / _columns)) * _amplitude * Time.deltaTime;
                _invaders[i].SetPosition(pos);
            }
            
        }
    }
}