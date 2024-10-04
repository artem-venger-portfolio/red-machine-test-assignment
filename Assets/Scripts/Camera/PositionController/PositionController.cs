using System.Collections;
using UnityEngine;
using Utils;
using Utils.MonoBehaviourUtils;

namespace Camera
{
    public class PositionController : IPositionController
    {
        private readonly CameraBase _camera;

        public PositionController(CameraBase camera)
        {
            _camera = camera;
        }

        public void StartTransition()
        {
            LogInfo(nameof(StartTransition));
        }

        public void ChangeDelta(Vector3 delta)
        {
            CameraPosition += -delta;
        }

        public void StopTransition()
        {
            LogInfo(nameof(StopTransition));
        }

        private Vector3 CameraPosition
        {
            get => _camera.Position;
            set => _camera.Position = value;
        }

        private void LogInfo(string message)
        {
            Log.Info(typeof(PositionController), message);
        }
    }
}