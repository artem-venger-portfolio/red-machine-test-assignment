namespace Camera
{
    public interface ICameraConfig
    {
        public float FollowTime { get; }
        public float DecelerationTime { get; }
        float FollowDistance { get; }
        float MinDecelerationVelocity { get; }
    }
}