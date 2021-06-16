using UnityEngine;
using ZentySpeede.General;
using ZentySpeede.Player;

namespace ZentySpeede.Obstacle
{
    public class WallTrigger : Spawnable
    {
        private void OnTriggerEnter(Collider other)
        {
            other.GetComponent<HungerMeter>().SetHunger(0);
        }
        
    }

}
