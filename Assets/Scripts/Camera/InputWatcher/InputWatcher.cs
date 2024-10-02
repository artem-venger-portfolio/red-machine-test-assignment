using System;
using Player;
using Player.ActionHandlers;
using UnityEngine;
using Utils;

namespace Camera
{
    public class InputWatcher : IInputWatcher
    {
        private readonly ClickHandler _clickHandler;
        private readonly CameraBase _cameraBase;
        private Vector3 _startPosition;
        private Vector3 _targetPosition;
        private bool _isDragging;

        public InputWatcher(ClickHandler clickHandler, CameraBase cameraBase)
        {
            _clickHandler = clickHandler;
            _cameraBase = cameraBase;
        }

        public event Action DragStarted;
        public event Action<Vector3> DragDeltaChanged;
        public event Action DragEnded;

        public void Initialize()
        {
            LogInfo(nameof(Initialize));
            _clickHandler.DragStartEvent += OnDragStart;
            _clickHandler.DragUpdateEvent += OnDragUpdate;
            _clickHandler.DragEndEvent += OnDragEnd;
        }

        public void Dispose()
        {
            LogInfo(nameof(Dispose));
            _clickHandler.DragStartEvent -= OnDragStart;
            _clickHandler.DragUpdateEvent -= OnDragUpdate;
            _clickHandler.DragEndEvent -= OnDragEnd;
        }
        
        private void OnDragStart(Vector3 startPosition)
        {
            if (IsConnecting)
                return;
            
            IsDragging = true;
            _startPosition = WorldToViewportPoint(startPosition);
            DragStarted?.Invoke();
        }
        
        private void OnDragUpdate(Vector3 position)
        {
            if (IsDragging == false)
                return;

            var previousPosition = _targetPosition;
            _targetPosition = WorldToViewportPoint(position);

            var positionsDistance = Vector3.Distance(previousPosition, _targetPosition);
            var arePositionsEqual = Mathf.Approximately(positionsDistance, 0);
            if (arePositionsEqual == false)
            {
                var delta = _targetPosition - _startPosition;
                delta.z = 0;
                DragDeltaChanged?.Invoke(delta);
            }
        }

        private void OnDragEnd(Vector3 finishPosition)
        {
            if (IsConnecting)
                return;
            
            IsDragging = false;
            DragEnded?.Invoke();
        }
        
        private Vector3 WorldToViewportPoint(Vector3 position)
        {
            return _cameraBase.WorldToViewportPoint(position);
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
        
        private void LogInfo(string message)
        {
            Log.Info(GetType(), message);
        }
    }
}