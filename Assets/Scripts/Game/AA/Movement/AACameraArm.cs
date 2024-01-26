using UnityEngine;

public class AACameraArm : MonoBehaviour
{

    [SerializeField] private Transform _arm;
    [SerializeField] private float _speed;
    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, _arm.position, Time.deltaTime * _speed);
        transform.rotation = Quaternion.Lerp(transform.rotation, _arm.rotation, Time.deltaTime * _speed);
    }



}