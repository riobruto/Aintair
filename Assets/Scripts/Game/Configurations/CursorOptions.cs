using UnityEngine;

namespace Configurations
{
    public class CursorOptions : MonoBehaviour
    {
        [SerializeField] private CursorLockMode cursorLockState = CursorLockMode.None;
        [SerializeField] private bool visible = true;

        private bool _pause;

        private void Start()
        {
            _pause = false;
        }

        private void Update()
        {
            Cursor.lockState = cursorLockState;
            Cursor.visible = visible;

            if (Input.GetKeyDown(KeyCode.Escape) && !_pause)
            {
                _pause = true;
                visible = true;
                cursorLockState = CursorLockMode.None;

                return;
            }

            if (Input.GetKeyDown(KeyCode.Escape) && _pause)
            {
                _pause = false;
                cursorLockState = CursorLockMode.Locked;
                visible = false;
            }
        }
    }
}