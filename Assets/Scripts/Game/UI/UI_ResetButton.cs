using GameSystem;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.SpaceInvaders.UI
{
    public class UI_ResetButton : MonoBehaviour
    {
        public void Update()
        {
            if (!GameConfigurationsSystem.Instance.CanPlay)
            {
                if (Input.GetKeyDown(KeyCode.T))
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                }
            }
        }
    }
}