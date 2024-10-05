using UnityEngine;

namespace Camera
{
    public abstract class CameraBoundsBase : MonoBehaviour
    {
        public abstract bool IsInsideBounds(Vector3 position, float orthographicSize);
    }
}