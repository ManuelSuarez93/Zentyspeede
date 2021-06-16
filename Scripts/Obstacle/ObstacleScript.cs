using UnityEngine;
using ZentySpeede.Audio;
using ZentySpeede.General;
using ZentySpeede.Player;

namespace ZentySpeede.Obstacle
{
    public class ObstacleScript : Spawnable
    {
        [SerializeField] ObstacleType type;

        Collider collider;
        Renderer renderer;
        ObstacleAudio oAudio;  

        private Color normalColor => renderer.material.color;

        private void Awake()
        {
            renderer = GetComponentInChildren<Renderer>();
            collider = GetComponent<Collider>();
            oAudio = GetComponent<ObstacleAudio>();
        }

        public enum ObstacleType
        {
            circleForm, sForm, triangleForm, wall
        }
        public ObstacleType Type { get => type; }

        public void PassedAction()
        {
            oAudio.PlayEndClip();
            collider.enabled = false;
            renderer.material.color = Color.blue;
        }

        public override void Initialization()
        {
            collider.enabled = true;
            renderer.material.color = normalColor;
        }

        private void OnTriggerEnter(Collider other)
        {
            other.GetComponent<HungerMeter>().SetHunger(0);
        }

    }
}

