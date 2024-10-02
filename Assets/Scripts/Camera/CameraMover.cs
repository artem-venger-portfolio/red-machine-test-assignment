using Player;
using Player.ActionHandlers;
using UnityEngine;
using Utils;

namespace Camera
{
    public class CameraMover : MonoBehaviour
    {
        private CameraBase _camera;
        private ClickHandler _clickHandler;
        private bool _isDragging;
        private Vector3 _startPosition;
        private Vector3 _targetPosition;
        
        private void Awake()
        {
            LogInfo(nameof(Awake));
            _camera = CameraHolder.Instance.MainCamera;
            _clickHandler = ClickHandler.Instance;
            _clickHandler.DragStartEvent += OnDragStart;
            _clickHandler.DragUpdateEvent += OnDragUpdate;
            _clickHandler.DragEndEvent += OnDragEnd;
        }

        private void OnDragStart(Vector3 startPosition)
        {
            if (IsConnecting)
                return;
            
            IsDragging = true;
            _startPosition = startPosition;
        }
        
        private void OnDragUpdate(Vector3 position)
        {
            if (IsDragging == false)
                return;
            
            _targetPosition = position;
        }

        private void OnDragEnd(Vector3 finishPosition)
        {
            if (IsConnecting)
                return;
            
            IsDragging = false;
        }

        private static bool IsConnecting => PlayerController.PlayerState == PlayerState.Connecting;
        
        private bool IsDragging
        {
            get => _isDragging;
            set
            {
                _isDragging = value;
                LogInfo($"IsDragging: {value}");
            }
        }

        private void OnDestroy()
        {
            LogInfo(nameof(OnDestroy));
            _clickHandler.DragStartEvent -= OnDragStart;
            _clickHandler.DragEndEvent -= OnDragEnd;
        }
        
        private void LogInfo(string message)
        {
            Log.Info(GetType(), message);
        }
    }
}