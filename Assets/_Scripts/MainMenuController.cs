using UnityEngine;

namespace _Scripts
{
    public class MainMenuController : MonoBehaviour
    {

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                ExitGame();
            }
        }

        public void PlayGame()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Main");
        }

        public void ExitGame()
        {
            Application.Quit();
        }

    }
}