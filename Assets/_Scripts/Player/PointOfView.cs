using Unity.VisualScripting;
using UnityEngine;

namespace _Scripts.Player
{
    public class PointOfView : MonoBehaviour
    {
        [SerializeField] private Texture2D sprite;

        [SerializeField] private Transform[] views;
        private const float TransitionSpeed = 5.0f;
        private int currentViewIndex;
        [SerializeField] private AudioSource switchSound;

        private void Start()
        {
            Cursor.SetCursor(sprite, Vector2.zero, CursorMode.Auto);
        }

        private void Update()
        {
            float scroll = Input.GetAxis("Mouse ScrollWheel");

            if (Input.GetKeyDown(KeyCode.E) || (scroll > 0f))
            {
                switchSound.Play();
                currentViewIndex = Mathf.Clamp(currentViewIndex + 1, 0, views.Length - 1);
            }
            else if (Input.GetKeyDown(KeyCode.Q) || (scroll < 0f))
            {
                switchSound.Play();
                currentViewIndex = Mathf.Clamp(currentViewIndex - 1, 0, views.Length - 1);
            }

            if (views.Length > 0 && views[currentViewIndex])
            {
                Quaternion targetRotation =
                    Quaternion.LookRotation(views[currentViewIndex].position - transform.position);
                transform.rotation =
                    Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * TransitionSpeed);
            }
        }
    }
}