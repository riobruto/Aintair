using Interfaces;
using System.Collections;
using UnityEngine;

namespace Jets.Controller
{
    public abstract class JetController : MonoBehaviour
    {        
        private Jet _owner;
        public Jet GetOwner() => _owner;
        
        
        private void Start()
        {
            _owner = GetComponentInParent<Jet>();                 
            _owner.JetStateEvent += OnJetChangeState;              
            SetUp();
        }
        private void Update()
        {
            UpdateController();
        }
        internal virtual void SetUp()
        {
            
            
        }

        internal virtual void UpdateController()
        {


        }


        internal virtual void OnJetChangeState(JetState state)
        {
            
        }
        private void OnDisable()
        {
            _owner.JetStateEvent -= OnJetChangeState;
            SetDown();
        }
        internal virtual void SetDown()
        {


        }
    }
}