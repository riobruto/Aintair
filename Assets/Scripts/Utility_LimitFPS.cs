
using UnityEngine;

public class Utility_LimitFPS : MonoBehaviour
{
    private void Start()
    {
        Application.targetFrameRate = 60;
    }

}
