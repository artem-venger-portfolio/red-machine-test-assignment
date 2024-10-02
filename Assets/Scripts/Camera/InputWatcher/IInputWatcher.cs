using System;
using UnityEngine;

namespace Camera
{
    public interface IInputWatcher
    {
        void Initialize();
        void Dispose();
        event Action DragStarted; 
        event Action<Vector3> DragDeltaChanged;
        event Action DragEnded;
    }
}