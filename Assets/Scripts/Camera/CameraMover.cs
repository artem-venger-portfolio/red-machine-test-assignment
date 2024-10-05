using Player.ActionHandlers;
using UnityEngine;
using Utils;
using Utils.MonoBehaviourUtils;

namespace Camera
{
    public class CameraMover : MonoBehaviour
    {
        [SerializeField] private CameraConfigSO cameraConfigSO;

        private CameraBase _camera;
        private IInputWatcher _inputWatcher;
        private IPositionController _positionController;

        private void Awake()
        {
            LogInfo(nameof(Awake));
            _camera = CameraHolder.Instance.MainCamera;
            _inputWatcher = new InputWatcher(ClickHandler.Instance);
            _inputWatcher.DragStarted += DragStartedEventHandler;
            _inputWatcher.DragDeltaChanged += DragDeltaChangedEventHandler;
            _inputWatcher.DragEnded += DragEndedEventHandler;
            _inputWatcher.Initialize();
            _positionController = new PositionController(_camera, Coroutines.Instance, cameraConfigSO);
        }
        
        private void DragStartedEventHandler()
        {
            LogInfo(nameof(DragStartedEventHandler));
            _positionController.StartTransition();
        }

        private void DragDeltaChangedEventHandler(Vector3 delta)
        {
            _positionController.ChangeDelta(delta);
        }

        private void DragEndedEventHandler()
        {
            LogInfo(nameof(DragEndedEventHandler));
            _positionController.StopTransition();
        }

        private void OnDestroy()
        {
            LogInfo(nameof(OnDestroy));
            _inputWatcher.DragStarted -= DragStartedEventHandler;
            _inputWatcher.DragDeltaChanged -= DragDeltaChangedEventHandler;
            _inputWatcher.DragEnded -= DragEndedEventHandler;
            _inputWatcher.Dispose();
        }

        private void LogInfo(string message)
        {
            Log.Info(GetType(), message);
        }
    }
}