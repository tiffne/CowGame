using UnityEngine;

public class OpenSignManager : MonoBehaviour
{
    public float delayBeforeNextScene = 3f;

    private float timeElapsed = 0f;

    void Update()
    {
        timeElapsed += Time.deltaTime;

        if (timeElapsed >= delayBeforeNextScene)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Main");
        }
    }
}
