using Unity.VisualScripting;
using UnityEngine;

namespace _Scripts.Player
{
    public class PointOfView : MonoBehaviour
    {
        [SerializeField] private Sprite sprite;

        [SerializeField] private Transform[] views;
        float transitionSpeed = 5f;

        private int currentViewIndex = 0;

        void Start()
        {
            //Cursor.SetCursor(sprite.texture, Vector2.zero, CursorMode.Auto);
        }

        void Update()
        {
            // float scroll = Input.GetAxis("Mouse ScrollWheel");
            //
            // if (scroll > 0f)
            // {
            //     currentViewIndex = Mathf.Clamp(currentViewIndex + 1, 0, views.Length - 1);
            // }
            // else if (scroll < 0f)
            // {
            //     currentViewIndex = Mathf.Clamp(currentViewIndex - 1, 0, views.Length - 1);
            // }
            //
            // if (views.Length > 0 && views[currentViewIndex] != null)
            // {
            //     Quaternion targetRotation = Quaternion.LookRotation(views[currentViewIndex].position - transform.position);
            //     transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * transitionSpeed);
            // }

            if (Input.GetKeyDown(KeyCode.E)) transform.Rotate(90 * Vector3.up);
            if (Input.GetKeyDown(KeyCode.Q)) transform.Rotate(90 * Vector3.down);

        }
    }
}