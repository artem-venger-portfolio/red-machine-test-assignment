using UnityEngine;

namespace Camera
{
    public abstract class CameraBase : MonoBehaviour
    {
        public abstract Vector3 ScreenToWorldPoint(Vector3 position);
    }
}