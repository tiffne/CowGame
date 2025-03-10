using UnityEngine;

namespace _Scripts.Player
{
    public class PointOfView : MonoBehaviour
    {
        [SerializeField] private Sprite sprite;

        void Start()
        {
            //Cursor.SetCursor(sprite.texture, Vector2.zero, CursorMode.Auto);
        }

        // Update is called once per frame
        void Update()
        {
        }
    }
}