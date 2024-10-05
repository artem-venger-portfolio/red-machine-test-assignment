using UnityEngine;

namespace Camera
{
    public abstract class CameraBoundsBase : MonoBehaviour
    {
        public abstract float Left { get; }
        public abstract float Right { get; }
        public abstract float Top { get; }
        public abstract float Bottom { get; }
    }
}