using UnityEngine;

namespace Infrastructure.Services.Chunks
{
    public class Chunk : MonoBehaviour
    {
        private void Centralize()
        {
            Bounds bounds = new Bounds();
            foreach (Renderer renderer in GetComponentsInChildren<Renderer>())
            {
                bounds.Encapsulate(renderer.bounds);
            }

            Vector3 delta = bounds.center - transform.position;
            foreach (Transform o in transform)
            {
                o.position -= delta;
            }
        }
    }
}