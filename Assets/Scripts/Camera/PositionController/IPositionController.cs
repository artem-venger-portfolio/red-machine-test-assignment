using UnityEngine;

namespace Camera
{
    public interface IPositionController
    {
        void MoveTo(Vector3 position);
        void Dispose();
    }
}