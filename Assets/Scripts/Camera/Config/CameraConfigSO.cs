using UnityEngine;

namespace Camera.Config
{
    [CreateAssetMenu(menuName = "RedMachineTest/" + nameof(CameraConfigSO), fileName = nameof(CameraConfigSO))]
    public class CameraConfigSO : ScriptableObject, ICameraConfig
    {
        [SerializeField] private float followTime;
        [SerializeField] private float decelerationTime;

        public float FollowTime => followTime;
        public float DecelerationTime => decelerationTime;
    }
}