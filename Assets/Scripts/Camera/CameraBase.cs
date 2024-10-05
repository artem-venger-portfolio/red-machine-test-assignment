using UnityEngine;

namespace Camera
{
    public abstract class CameraBase : MonoBehaviour
    {
        public abstract Vector3 Position { set; get; }
        public abstract float OrthographicSize { get; }
        public abstract Vector3 ScreenToWorldPoint(Vector3 position);
        public abstract Vector3 WorldToViewportPoint(Vector3 position);
    }
}