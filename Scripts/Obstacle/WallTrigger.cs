using UnityEngine;
using ZentySpeede.General;
using ZentySpeede.Player;

namespace ZentySpeede.Obstacle
{
    [RequireComponent(typeof(ObstacleDissolve))]
    public class WallTrigger : Spawnable
    {
        [SerializeField] ObstacleDissolve oDissolve;
        private void OnTriggerEnter(Collider other) => other.GetComponent<HungerMeter>().SetHunger(0);
        public override void Initialization()
        {
            oDissolve.ResetDissolve();
            base.Initialization();
        }
    }

}
