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
        private readonly Coroutines _coroutineManager;
        private readonly float _transitionTime;
        private Vector3 _pinnedPosition;
        private Coroutine _moveCoroutine;

        public PositionController(CameraBase camera, float sensitivity, float transitionTime)
        {
            _camera = camera;
            _sensitivity = sensitivity;
            _transitionTime = transitionTime;
            _coroutineManager = Coroutines.Instance;
        }
        
        public void PinPosition()
        {
            LogInfo(nameof(PinPosition));
            _pinnedPosition = CameraPosition;
        }
        
        public void ChangeDelta(Vector3 delta)
        {
            LogInfo($"{nameof(ChangeDelta)}: {delta}");
            StopCurrentTransitionIfPossible();
            var targetPosition = _pinnedPosition + -delta * _sensitivity;
            var moveCoroutine = GetMoveCoroutine(targetPosition);
            _moveCoroutine = _coroutineManager.StartCoroutine(moveCoroutine);
        }

        public void UnpinPosition()
        {
            LogInfo(nameof(UnpinPosition));
            _pinnedPosition = Vector3.zero;
        }

        public void Dispose()
        {
            LogInfo(nameof(Dispose));
            StopCurrentTransitionIfPossible();
        }

        private void StopCurrentTransitionIfPossible()
        {
            if (_moveCoroutine != null)
            {
                _coroutineManager.StopCoroutine(_moveCoroutine);
            } 
        }
        
        private Vector3 CameraPosition
        {
            get => _camera.Position;
            set => _camera.Position = value;
        }

        private IEnumerator GetMoveCoroutine(Vector3 position)
        {
            var startPosition = CameraPosition;
            var elapsedTime = 0f;

            while (elapsedTime < _transitionTime)
            {
                elapsedTime += Time.deltaTime;
                CameraPosition = Vector3.Lerp(startPosition, position, elapsedTime / _transitionTime);
                yield return null;
            }

            _moveCoroutine = null;
        }

        private void LogInfo(string message)
        {
            Log.Info(typeof(PositionController), message);
        }
    }
}