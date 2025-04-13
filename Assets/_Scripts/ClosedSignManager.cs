using UnityEngine;

public class ClosedSignManager : MonoBehaviour
{
    public float delayBeforeNextScene = 2f;

    private float timeElapsed = 0f;

    void Update()
    {
        timeElapsed += Time.deltaTime;

        if (timeElapsed >= delayBeforeNextScene)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("EOD");
        }
    }
}
