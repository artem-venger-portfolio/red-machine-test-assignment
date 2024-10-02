using UnityEngine;
using UnityCamera = UnityEngine.Camera;

namespace Camera
{
    [RequireComponent(typeof(UnityCamera))]
    public class CameraDefault : CameraBase
    {
        private UnityCamera _camera;

        public override Vector3 Position
        {
            get => transform.position;
            set => transform.position = value;
        }

        private void Awake()
        {
            _camera = GetComponent<UnityCamera>();
        }

        public override Vector3 ScreenToWorldPoint(Vector3 position)
        {
            return _camera.ScreenToWorldPoint(position);
        }
    }
}