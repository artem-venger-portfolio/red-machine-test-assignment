using System.Collections;
using UnityEngine;
using Utils;
using Utils.MonoBehaviourUtils;

namespace Camera
{
    public class PositionController : IPositionController
    {
        private readonly Coroutines _coroutineManager;
        private readonly ICameraConfig _config;
        private readonly CameraBoundsBase _cameraBounds;
        private readonly CameraBase _camera;
        private Coroutine _transitionCoroutine;
        private Vector3 _targetPosition;
        private Vector3 _velocity;

        public PositionController(CameraBase camera, Coroutines coroutineManager, ICameraConfig config, 
            CameraBoundsBase cameraBounds)
        {
            _camera = camera;
            _coroutineManager = coroutineManager;
            _config = config;
            _cameraBounds = cameraBounds;
        }

        public void StartTransition()
        {
            LogInfo(nameof(StartTransition));

            if (_transitionCoroutine != null)
            {
                _coroutineManager.StopCoroutine(_transitionCoroutine);
            }

            IsFollowing = true;
            _transitionCoroutine = _coroutineManager.StartCoroutine(GetTransitionCoroutine());
        }

        public void ChangeDelta(Vector3 delta)
        {
            _targetPosition = CameraPosition - delta;
        }

        public void StopTransition()
        {
            LogInfo(nameof(StopTransition));
            IsFollowing = false;
        }

        private Vector3 CameraPosition
        {
            get => _camera.Position;
            set => _camera.Position = _cameraBounds.CorrectPosition(value);
        }

        private bool IsFollowing { get; set; }

        private IEnumerator GetTransitionCoroutine()
        {
            _targetPosition = CameraPosition;
            _velocity = Vector3.zero;
        
            while (IsFollowing)
            {
                var distanceToTarget = Vector3.Distance(CameraPosition, _targetPosition);
                var isOnTarget = Mathf.Approximately(distanceToTarget, 0);
                if (isOnTarget == false)
                {
                    CameraPosition = Vector3.SmoothDamp(CameraPosition, _targetPosition, ref _velocity, _config.FollowTime);
                }
                
                yield return null;
            }
            
            var acceleration = Vector3.zero;
            var maxAcceleration = float.PositiveInfinity;
            while (_velocity.magnitude > 0.5f)
            {
                var deltaTime = Time.deltaTime;
                CameraPosition += _velocity * deltaTime;
                _velocity = Vector3.SmoothDamp(_velocity, Vector3.zero, ref acceleration, _config.DecelerationTime, maxAcceleration, deltaTime);
                yield return null;
            }
            
            _transitionCoroutine = null;
        }

        private void LogInfo(string message)
        {
            Log.Info(typeof(PositionController), message);
        }
    }
}