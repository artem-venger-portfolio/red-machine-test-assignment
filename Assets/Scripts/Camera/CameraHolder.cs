using UnityEngine;
using Utils.Singleton;

namespace Camera
{
    public class CameraHolder : DontDestroyMonoBehaviourSingleton<CameraHolder>
    {
        [SerializeField] private CameraBase mainCamera;
        
        public CameraBase MainCamera => mainCamera;
    }
}