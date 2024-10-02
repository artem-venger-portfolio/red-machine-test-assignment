using Player.ActionHandlers;
using UnityEngine;
using Utils;

namespace Camera
{
    public class CameraMover : MonoBehaviour
    {
        private CameraBase _camera;
        private IInputWatcher _inputWatcher;
        
        private void Awake()
        {
            LogInfo(nameof(Awake));
            _camera = CameraHolder.Instance.MainCamera;
            _inputWatcher = new InputWatcher(ClickHandler.Instance);
            _inputWatcher.Initialize();
        }

        private void OnDestroy()
        {
            LogInfo(nameof(OnDestroy));
            _inputWatcher.Dispose();
        }
        
        private void LogInfo(string message)
        {
            Log.Info(GetType(), message);
        }
    }
}