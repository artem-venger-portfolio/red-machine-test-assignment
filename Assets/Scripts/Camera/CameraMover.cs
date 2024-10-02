using Player.ActionHandlers;
using UnityEngine;
using Utils;

namespace Camera
{
    public class CameraMover : MonoBehaviour
    {
        [SerializeField] private float transitionTime = 0.5f;
        [SerializeField] private float sensitivity = 0.5f;
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
            _positionController = new PositionController(_camera, sensitivity, transitionTime);
        }
        
        private void DragStartedEventHandler()
        {
            LogInfo(nameof(DragStartedEventHandler));
            _positionController.PinPosition();
        }

        private void DragDeltaChangedEventHandler(Vector3 delta)
        {
            _positionController.ChangeDelta(delta);
        }

        private void DragEndedEventHandler()
        {
            LogInfo(nameof(DragEndedEventHandler));
            _positionController.UnpinPosition();
        }

        private void OnDestroy()
        {
            LogInfo(nameof(OnDestroy));
            _inputWatcher.DragStarted -= DragStartedEventHandler;
            _inputWatcher.DragDeltaChanged -= DragDeltaChangedEventHandler;
            _inputWatcher.DragEnded -= DragEndedEventHandler;
            _inputWatcher.Dispose();
            _positionController.Dispose();
        }

        private void LogInfo(string message)
        {
            Log.Info(GetType(), message);
        }
    }
}