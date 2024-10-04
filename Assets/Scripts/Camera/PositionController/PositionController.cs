using System;
using System.Collections;
using UnityEngine;
using Utils;
using Utils.MonoBehaviourUtils;

namespace Camera
{
    public class PositionController : IPositionController
    {
        private readonly Coroutines _coroutineManager;
        private readonly CameraBase _camera;
        private readonly float _smoothTime;
        private Coroutine _transitionCoroutine;
        private Vector3 _targetPosition;
        private Vector3 _velocity;

        public PositionController(CameraBase camera, Coroutines coroutineManager, float smoothTime)
        {
            _camera = camera;
            _coroutineManager = coroutineManager;
            _smoothTime = smoothTime;
        }

        public void StartTransition()
        {
            LogInfo(nameof(StartTransition));

            if (_transitionCoroutine != null)
            {
                throw new InvalidOperationException("Transition is already started");
            }

            _transitionCoroutine = _coroutineManager.StartCoroutine(GetTransitionCoroutine());
        }

        public void ChangeDelta(Vector3 delta)
        {
            _targetPosition = CameraPosition - delta;
        }

        public void StopTransition()
        {
            LogInfo(nameof(StopTransition));
            _coroutineManager.StopCoroutine(_transitionCoroutine);
            _transitionCoroutine = null;
        }

        private Vector3 CameraPosition
        {
            get => _camera.Position;
            set => _camera.Position = value;
        }

        private IEnumerator GetTransitionCoroutine()
        {
            _targetPosition = CameraPosition;
            _velocity = Vector3.zero;
        
            while (true)
            {
                var distanceToTarget = Vector3.Distance(CameraPosition, _targetPosition);
                var isOnTarget = Mathf.Approximately(distanceToTarget, 0);
                if (isOnTarget == false)
                {
                    CameraPosition = Vector3.SmoothDamp(CameraPosition, _targetPosition, ref _velocity, _smoothTime);
                }
                
                yield return null;
            }
        }

        private void LogInfo(string message)
        {
            Log.Info(typeof(PositionController), message);
        }
    }
}