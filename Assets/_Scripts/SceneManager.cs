using UnityEngine;

namespace _Scripts
{
    public class SceneManager : MonoBehaviour
    {
        void Start()
        {

        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Application.Quit();
            }
        }
    }
}
