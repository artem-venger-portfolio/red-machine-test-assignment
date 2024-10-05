using UnityEngine;

namespace Camera
{
    public class CameraBounds : CameraBoundsBase
    {
        [SerializeField] private Vector2 size;
        [SerializeField] private Vector2 offset;

        private CameraBase _camera;

        public override void Initialize(CameraBase cameraComponent)
        {
            _camera = cameraComponent;
        }

        public override Vector3 CorrectPosition(Vector3 position)
        {
            var cameraHalfHeight = OrthographicSize;
            var cameraHalfWidth = cameraHalfHeight * Screen.width / Screen.height;
            var positionX = position.x;
            var positionY = position.y;

            float minX;
            float maxX;
            float minY;
            float maxY;
            
            if (size.x < cameraHalfWidth * 2)
            {
                var centerX = Center.x;
                minX = centerX;
                maxX = centerX;
            }
            else
            {
                minX = Left + cameraHalfWidth;
                maxX = Right - cameraHalfWidth;
            }
            
            
            if (size.y < cameraHalfHeight * 2)
            {
                var centerY = Center.y;
                minY = centerY;
                maxY = centerY;
            }
            else
            {
                minY = Bottom + cameraHalfHeight;
                maxY = Top - cameraHalfHeight;
            }

            var x = Mathf.Clamp(positionX, minX, maxX);
            var y = Mathf.Clamp(positionY, minY, maxY);
            var z = position.z;
            
            return new Vector3(x, y, z);
        }

        private float Left => Center.x - size.x / 2;
        
        private float Right => Center.x+ size.x / 2;

        private float Top => Center.y + size.y / 2;

        private float Bottom => Center.y - size.y / 2;
        
        private Vector3 Center => transform.position + new Vector3(offset.x, offset.y, 0);
        
        private float OrthographicSize => _camera.OrthographicSize;
        
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(Center, size);
        }
    }
}