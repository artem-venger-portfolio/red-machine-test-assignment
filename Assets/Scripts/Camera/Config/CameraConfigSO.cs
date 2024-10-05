using UnityEngine;

namespace Camera
{
    [CreateAssetMenu(menuName = "RedMachineTest/" + nameof(CameraConfigSO), fileName = nameof(CameraConfigSO))]
    public class CameraConfigSO : ScriptableObject, ICameraConfig
    {
        [SerializeField] private float followTime;
        [SerializeField] private float followDistance;
        [SerializeField] private float decelerationTime;
        [SerializeField] private float minDecelerationVelocity;

        public float FollowTime => followTime;
        public float DecelerationTime => decelerationTime;
        public float FollowDistance => followDistance;
        public float MinDecelerationVelocity => minDecelerationVelocity;
    }
}