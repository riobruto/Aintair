using UnityEngine;

namespace Jets.Controller
{
    public class JetReticleQuadController : JetController
    {
        private SphereCollider _collider;
        private Material _material;

        // Update is called once per frame
        internal override void SetUp()
        {
            _collider = GetComponentInParent<SphereCollider>();
            _material = GetComponent<MeshRenderer>().material;
            _material.enableInstancing = true;

            _material.SetFloat("_reticleAlpha ", 1);
        }

        internal override void OnJetChangeState(JetState state)
        {
            switch (state)
            {
                case JetState.INITIALIZED:

                    _material.SetFloat("_reticleAlpha ", 1);
                    break;

                case JetState.RECIEVING_DAMAGE:

                    break;

                case JetState.DESTROYED:

                    _material.SetFloat("_reticleAlpha", 0);
                    break;

                case JetState.RESETTED:
                    _material.SetFloat("_reticleAlpha", 1);
                    break;

                default:
                    break;
            }
        }

        internal override void UpdateController()
        {
            transform.localPosition = (_collider.center);
        }
    }
}