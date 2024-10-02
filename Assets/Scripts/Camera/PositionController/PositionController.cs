using System.Collections;
using UnityEngine;
using Utils;
using Utils.MonoBehaviourUtils;

namespace Camera
{
    public class PositionController : IPositionController
    {
        private readonly CameraBase _camera;
        private readonly float _sensitivity;
        private readonly float _transitionTime;

        public PositionController(CameraBase camera, float sensitivity)
        {
            _camera = camera;
            _sensitivity = sensitivity;
        }

        public void StartTransition()
        {
            
        }

        public void ChangeDelta(Vector3 delta)
        {
            LogInfo($"{nameof(ChangeDelta)}: {delta}");
            CameraPosition += -delta * _sensitivity;
        }

        public void StopTransition()
        {
            
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