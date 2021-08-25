using UnityEngine;
using ZentySpeede.General;
using ZentySpeede.Obstacle;

namespace ZentySpeede.Player
{
    [RequireComponent(typeof(ObstacleGlow))]
    [RequireComponent(typeof(Collider))]
    public class NoHungerPowerUp : Spawnable
    {
        [SerializeField] float noHungerTime;
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
                other.gameObject.GetComponentInParent<HungerMeter>().SetTimer(noHungerTime);
                Deactivation();
            }
            
        }

    }


}
