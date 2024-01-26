using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AACameraZoom : MonoBehaviour
{
    private float _zoomSpeed = 5.0f;
    private float _normal = 30.0f;
    private float _zoom = 10.0f;
    private float _currentZoom = 0.0f;

    private Camera _camera;
    private void Start()
    {
        _camera = Camera.main;
    }
    private void Update()
    {
        _currentZoom = Input.GetKey(KeyCode.Mouse1) ? _zoom : _normal;  
        _camera.fieldOfView = Mathf.Lerp(_camera.fieldOfView, _currentZoom, Time.deltaTime * _zoomSpeed);
    }

}
