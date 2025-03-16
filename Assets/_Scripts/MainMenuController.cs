using UnityEngine;

namespace _Scripts
{
    public class MainMenuController : MonoBehaviour
    {
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                ExitGame();
            }
        }

        private void PlayGame()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Main");
        }

        private void ExitGame()
        {
            Application.Quit();
        }

    }
}