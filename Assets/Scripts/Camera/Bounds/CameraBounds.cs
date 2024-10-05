using UnityEngine;

namespace Camera
{
    public class CameraBounds : CameraBoundsBase
    {
        [SerializeField] private Vector2 size;
        [SerializeField] private Vector2 offset;

        public override bool IsInsideBounds(Vector3 position, float orthographicSize)
        {
            var cameraHalfWidth = orthographicSize * Screen.width / Screen.height;
            var positionX = position.x;
            var positionY = position.y;
            
            var isIntersectingLeft = positionX - cameraHalfWidth < Left;
            var isIntersectingRight = positionX + cameraHalfWidth > Right;
            var isIntersectingTop = positionY + orthographicSize > Top;
            var isIntersectingBottom = positionY - orthographicSize < Bottom;
            
            return !isIntersectingLeft && !isIntersectingRight && !isIntersectingTop && !isIntersectingBottom;
        }
        
        private float Left => transform.position.x + offset.x - size.x / 2;
        
        private float Right => transform.position.x + offset.x + size.x / 2;
        
        private float Top => transform.position.y + offset.y + size.y / 2;
        
        private float Bottom => transform.position.y + offset.y - size.y / 2;
        
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            var center = transform.position + new Vector3(offset.x, offset.y, 0);
            Gizmos.DrawWireCube(center, size);
        }
    }
}