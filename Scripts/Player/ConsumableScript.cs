using UnityEngine;
using ZentySpeede.Audio;
using ZentySpeede.General;
using ZentySpeede.Obstacle;

namespace ZentySpeede.Player
{
    [RequireComponent(typeof(ObstacleGlow))]
    [RequireComponent(typeof(Collider))]
    public class ConsumableScript : Spawnable
    {
        [SerializeField] float hungerFillAmount;
        [SerializeField] ObstacleGlow oGlow;
        [SerializeField] Collider collider;
        [SerializeField] GameObject onDestroyPrefab;
        [SerializeField] float timeMultiplier = 5f;
        [SerializeField] float sinMultiplier = 20f;
        void Update() => oGlow.ModifyGlow(Mathf.Sin(Time.time * timeMultiplier) * sinMultiplier);

        private void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.CompareTag("Player"))
            {
                Instantiate(onDestroyPrefab);
                other.gameObject.GetComponentInParent<HungerMeter>().AddHunger(hungerFillAmount);
                Deactivation();
            }
            
        }

    }


}
