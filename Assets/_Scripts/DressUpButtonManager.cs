using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    public static string lastButtonClicked { get; private set; }

    [SerializeField] private Texture2D cowSprite;
    [SerializeField] private  Texture2D predatorSprite;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.SetCursor(cowSprite, Vector2.zero, CursorMode.Auto);
    }

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

    public void LionClick()
    {
        Debug.Log("Lion Button Clicked");
        lastButtonClicked = "Lion";
        Cursor.SetCursor(predatorSprite, Vector2.zero, CursorMode.Auto);
    }

    public void WolfClick()
    {
        Debug.Log("Wolf Button Clicked");
        lastButtonClicked = "Wolf";
        Cursor.SetCursor(predatorSprite, Vector2.zero, CursorMode.Auto);
    }

    public void CowClick()
    {
        Debug.Log("Cow Button Clicked");
        lastButtonClicked = "Cow";
        Cursor.SetCursor(cowSprite, Vector2.zero, CursorMode.Auto);
    }
}
