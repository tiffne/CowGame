using UnityEngine;

namespace _Scripts
{
    public class MainMenuController : MonoBehaviour
    {
        public void PlayGame()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("DressUp");
        }

        public void ExitGame()
        {
            Application.Quit();
        }

    }
}