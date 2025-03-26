namespace _Scripts.Fixed_Surfaces
{
    public class Garbage : Surface
    {
        private void Update()
        {
            if (transform.childCount == 0) return;
            Destroy(transform.GetChild(0).gameObject);
        }
    }
}
