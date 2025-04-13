using System.Collections;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    public static string lastButtonClicked { get; private set; } = "Cow";

    [SerializeField] private Texture2D cowSprite;
    [SerializeField] private  Texture2D predatorSprite;
    [SerializeField] private AudioSource thoughtBubbleSound;
    [SerializeField] private GameObject thoughtBubble;
    private bool thoughtBubbleActive;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.SetCursor(cowSprite, Vector2.zero, CursorMode.Auto);
        thoughtBubble.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
    
    public void ExitGame()
    {
        Application.Quit();
    }

    public void PlayGame()
    {
        if (lastButtonClicked.Equals("Cow"))
        {
            StartCoroutine(EnableThoughtBubble());
            thoughtBubbleSound.Play();
            return;
        }
        UnityEngine.SceneManagement.SceneManager.LoadScene("Open");
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
    
    private IEnumerator EnableThoughtBubble()
    {
        if (thoughtBubbleActive) yield break;
        thoughtBubble.SetActive(true);
        thoughtBubbleActive = true;
        yield return new WaitForSeconds(2);
        thoughtBubble.SetActive(false);
        thoughtBubbleActive = false;
    }
}