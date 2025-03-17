using Unity.VisualScripting;
using UnityEngine;

namespace _Scripts.Player
{
    public class PointOfView : MonoBehaviour
    {
        [SerializeField] private Sprite sprite;

        [SerializeField] private Transform[] views;
        private const float TransitionSpeed = 5.0f;
        private int currentViewIndex;

        void Start()
        {
            //Cursor.SetCursor(sprite.texture, Vector2.zero, CursorMode.Auto);
        }

        void Update()
        {
            float scroll = Input.GetAxis("Mouse ScrollWheel");

            //if (scroll > 0f)
            if (Input.GetKeyDown(KeyCode.E))
            {
                currentViewIndex = Mathf.Clamp(currentViewIndex + 1, 0, views.Length - 1);
            }
            //else if (scroll < 0f)
            else if (Input.GetKeyDown(KeyCode.Q))
            {
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