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
        private Vector3 _targetPosition;
        private Vector3 _startPosition;
        private bool _isDragging;

        public InputWatcher(ClickHandler clickHandler)
        {
            _clickHandler = clickHandler;
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
            _startPosition = startPosition;
            DragStarted?.Invoke();
        }
        
        private void OnDragUpdate(Vector3 position)
        {
            if (IsDragging == false)
                return;

            var delta = position - _startPosition;
            delta.z = 0;
            var hasMoved = Mathf.Approximately(delta.magnitude, 0) == false;
            if (hasMoved)
            {
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
        
        private static bool IsConnecting => PlayerController.PlayerState == PlayerState.Connecting;
        
        private bool IsDragging
        {
            get => _isDragging;
            set
            {
                _isDragging = value;
                LogInfo($"IsDragging: {_isDragging}");
            }
        }
        
        private void LogInfo(string message)
        {
            Log.Info(GetType(), message);
        }
    }
}