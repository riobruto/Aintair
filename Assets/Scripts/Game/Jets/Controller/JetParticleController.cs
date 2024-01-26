using System.Collections;
using UnityEngine;

namespace Jets.Controller
{
    public class JetParticleController : JetController
    {

        [SerializeField] private ParticleSystem particle;
        ParticleSystem.MainModule _main;
        private float _emissionRate = 0;
        internal override void SetUp()
        {
            _main = particle.main;
        }
        internal override void OnJetChangeState(JetState state)
        {

            switch (state)
            {
                case JetState.RESETTED:
                    particle.Play();                                     
                    _emissionRate = 0;
                    _main.startColor = new Color(1,1,1, _emissionRate);
                    break;
                case JetState.RECIEVING_DAMAGE:                    
                    _emissionRate += .1f;
                    _main.startColor = new Color(1, 1, 1, _emissionRate);
                    break;
                case JetState.DESTROYED:
                    particle.Stop();
                    break;
            }
        }




    }
}