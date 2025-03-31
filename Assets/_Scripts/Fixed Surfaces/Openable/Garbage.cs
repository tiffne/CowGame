namespace _Scripts.Fixed_Surfaces.Openable
{
    public class Garbage : OpenableContainer
    {
        private void Update()
        {
            if (transform.childCount == 0) return;
            Destroy(transform.GetChild(0).gameObject);
        }
    }
}