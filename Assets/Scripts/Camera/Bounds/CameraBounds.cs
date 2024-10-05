using UnityEngine;

namespace Camera
{
    public class CameraBounds : CameraBoundsBase
    {
        [SerializeField] private Vector2 size;
        [SerializeField] private Vector2 offset;

        public override float Left => transform.position.x + offset.x - size.x / 2;
        
        public override float Right => transform.position.x + offset.x + size.x / 2;
        
        public override float Top => transform.position.y + offset.y + size.y / 2;
        
        public override float Bottom => transform.position.y + offset.y - size.y / 2;
        
        private Vector3 Center => transform.position + new Vector3(offset.x, offset.y, 0);
        
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(Center, size);
        }
    }
}