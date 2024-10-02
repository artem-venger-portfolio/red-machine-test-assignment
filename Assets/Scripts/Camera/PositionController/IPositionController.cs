using UnityEngine;

namespace Camera
{
    public interface IPositionController
    {
        void Dispose();
        void PinPosition();
        void ChangeDelta(Vector3 delta);
        void UnpinPosition();
    }
}