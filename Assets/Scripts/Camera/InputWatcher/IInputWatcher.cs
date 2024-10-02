using System;
using UnityEngine;

namespace Camera
{
    public interface IInputWatcher
    {
        void Initialize();
        void Dispose();
        event Action<Vector3> TargetPositionChanged;
    }
}