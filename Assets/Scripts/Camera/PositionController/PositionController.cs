using System.Collections;
using UnityEngine;
using Utils;
using Utils.MonoBehaviourUtils;

namespace Camera
{
    public class PositionController : IPositionController
    {
        private readonly CameraBase _camera;
        private readonly Coroutines _coroutineManager;
        private readonly float _transitionTime;
        private Coroutine _moveCoroutine;

        public PositionController(CameraBase camera, float transitionTime)
        {
            _camera = camera;
            _transitionTime = transitionTime;
            _coroutineManager = Coroutines.Instance;
        }

        public void MoveTo(Vector3 position)
        {
            LogInfo($"{nameof(MoveTo)}: {position}");
            StopCurrentTransitionIfPossible();

            var moveCoroutine = GetMoveCoroutine(position);
            _moveCoroutine = _coroutineManager.StartCoroutine(moveCoroutine);
        }

        public void Dispose()
        {
            LogInfo(nameof(Dispose));
            StopCurrentTransitionIfPossible();
        }

        private void StopCurrentTransitionIfPossible()
        {
            if (_moveCoroutine != null) _coroutineManager.StopCoroutine(_moveCoroutine);
        }

        private IEnumerator GetMoveCoroutine(Vector3 position)
        {
            var startPosition = _camera.Position;
            var elapsedTime = 0f;

            while (elapsedTime < _transitionTime)
            {
                elapsedTime += Time.deltaTime;
                _camera.Position = Vector3.Lerp(startPosition, position, elapsedTime / _transitionTime);
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