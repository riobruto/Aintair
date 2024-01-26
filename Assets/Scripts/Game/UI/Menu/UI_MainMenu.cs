using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI.Menu
{
    public class UI_MainMenu : MonoBehaviour
    {
        [SerializeField] private GameObject _exitWarning;
        public void Play()
        {
            SceneManager.LoadScene("Game");
        }
        //Exit
        public void ExitWarningEnter()
        {
            _exitWarning.SetActive(true);

        }
        public void ExitWarningExit()
        {
            _exitWarning.SetActive(false);

        }
        public void ExitGame()
        {
            Application.Quit();
        }

    }
}
