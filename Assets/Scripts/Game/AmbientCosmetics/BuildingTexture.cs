using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingTexture : MonoBehaviour
{
    
    void Start()
    {
        Material mat = GetComponent<Renderer>().material;
        mat.SetFloat("_Index", Random.Range(0, 7));

    }

  
}
