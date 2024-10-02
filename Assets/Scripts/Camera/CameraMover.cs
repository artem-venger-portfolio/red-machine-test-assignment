using Player.ActionHandlers;
using UnityEngine;
using Utils;

namespace Camera
{
    public class CameraMover : MonoBehaviour
    {
        [SerializeField] private float transitionTime = 0.5f;
        private CameraBase _camera;
        private IInputWatcher _inputWatcher;

        private IPositionController _positionController;

        private void Awake()
        {
            LogInfo(nameof(Awake));
            _camera = CameraHolder.Instance.MainCamera;
            _inputWatcher = new InputWatcher(ClickHandler.Instance);
            _inputWatcher.DragDeltaChanged += DragDeltaChangedEventHandler;
            _inputWatcher.Initialize();
            _positionController = new PositionController(_camera, transitionTime);
        }
        
        private void DragDeltaChangedEventHandler(Vector3 delta)
        {
            
        }

        private void OnDestroy()
        {
            LogInfo(nameof(OnDestroy));
            _inputWatcher.DragDeltaChanged -= DragDeltaChangedEventHandler;
            _inputWatcher.Dispose();
            _positionController.Dispose();
        }

        private void LogInfo(string message)
        {
            Log.Info(GetType(), message);
        }
    }
}