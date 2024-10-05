using UnityEngine;

namespace Camera
{
    public abstract class CameraBoundsBase : MonoBehaviour
    {
        public abstract void Initialize(CameraBase camera);
        public abstract Vector3 CorrectPosition(Vector3 position);
    }
}