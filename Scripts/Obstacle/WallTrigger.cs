using UnityEngine;
using ZentySpeede.Audio;
using ZentySpeede.General;
using ZentySpeede.Player;

namespace ZentySpeede.Obstacle
{
    [RequireComponent(typeof(ObstacleDissolve))]
    [RequireComponent(typeof(ObstacleAudio))]
    public class WallTrigger : Spawnable
    {
        [SerializeField] ObstacleDissolve oDissolve;
        [SerializeField] ObstacleAudio obstacleAudio;
        private void OnTriggerEnter(Collider other)
        {
            other.GetComponent<HungerMeter>().SetHunger(0);
            obstacleAudio.PlayCrashClip();
        }
        public override void Initialization()
        {
            oDissolve.ResetDissolve();
            base.Initialization();
        }
    }

}
