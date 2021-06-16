using UnityEngine;
using ZentySpeede.General;

namespace ZentySpeede.Player
{
    public class ConsumableScript : Spawnable
    {
        [SerializeField] float hungerFillAmount;

        private void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.CompareTag("Player"))
            {
                other.gameObject.GetComponentInParent<HungerMeter>().AddHunger(hungerFillAmount);
                Deactivation();
            }
            
        }

    }


}
