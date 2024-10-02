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
        private Vector3 _aspectRationMultiplier;
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
            _aspectRationMultiplier = new Vector3((float)Screen.width / Screen.height, 1, 1);
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
            SetTargetPositionInWorldSpace(startPosition);
            DragStarted?.Invoke();
        }
        
        private void OnDragUpdate(Vector3 position)
        {
            if (IsDragging == false)
                return;

            var previousPosition = _targetPosition;
            SetTargetPositionInWorldSpace(position);

            var delta = _targetPosition - previousPosition;
            var arePositionsEqual = Mathf.Approximately(delta.magnitude, 0);
            if (arePositionsEqual == false)
            {
                var scaledDelta = Vector3.Scale(delta, _aspectRationMultiplier);
                DragDeltaChanged?.Invoke(scaledDelta);
            }
        }

        private void OnDragEnd(Vector3 finishPosition)
        {
            if (IsConnecting)
                return;
            
            IsDragging = false;
            DragEnded?.Invoke();
        }
        
        private void SetTargetPositionInWorldSpace(Vector3 positionWorld)
        {
            _targetPosition = _cameraBase.WorldToViewportPoint(positionWorld);
            _targetPosition.z = 0;
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