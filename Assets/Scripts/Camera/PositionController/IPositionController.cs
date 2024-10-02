using UnityEngine;

namespace Camera
{
    public interface IPositionController
    {
        void StartTransition();
        void ChangeDelta(Vector3 delta);
        void StopTransition();
    }
}