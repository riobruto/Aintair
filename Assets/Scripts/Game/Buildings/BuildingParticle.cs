using System;
using System.Collections;
using UnityEngine;

namespace Buildings
{
    public class BuildingParticle : MonoBehaviour
    {

        private void Start()
        {
            GetComponent<Building>().BuildingStateEvent += OnBuildingStateEvent;
        }

        private void OnBuildingStateEvent(BuildingState state)
        {
            if(state == BuildingState.DESTROYED)
            {
                GetComponentInChildren<ParticleSystem>().Play();
            }
        }
    }
}