using UnityEngine;
using System.Collections;
using TMPro;

public class ThoughtBubbles : MonoBehaviour
{
    [SerializeField] private SpriteRenderer bubbleSprite;
    [SerializeField] private TextMeshProUGUI bubbleText;

    [SerializeField] private float showDuration = 1.5f;
    [SerializeField] private string[] messages = {
        "I can't do this.", "Best not to do this.",
        "That might not be a good idea."
    };

    private void Awake()
    {
        // Hide the bubble initially
        if (bubbleSprite != null) bubbleSprite.enabled = false;
        if (bubbleText != null) bubbleText.text = "";
    }

    public void ShowBubble()
    {
        if (bubbleSprite == null || bubbleText == null)
        {
            Debug.LogWarning("Bubble components not assigned!");
            return;
        }

        bubbleText.text = messages[Random.Range(0, messages.Length)];
        
        bubbleSprite.enabled = true;
        
        StartCoroutine(HideAfterDelay());
    }

    private IEnumerator HideAfterDelay()
    {
        yield return new WaitForSeconds(showDuration);
        
        bubbleSprite.enabled = false;
        bubbleText.text = "";
    }
}