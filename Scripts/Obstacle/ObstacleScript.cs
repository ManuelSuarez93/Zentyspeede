using UnityEngine;
using ZentySpeede.Audio;
using ZentySpeede.General;
using ZentySpeede.Player;

namespace ZentySpeede.Obstacle
{
    [RequireComponent(typeof(Collider))]
    [RequireComponent(typeof(ObstacleAudio))]
    [RequireComponent(typeof(ObstacleGlow))]
    [RequireComponent(typeof(ObstacleDissolve))]
    public class ObstacleScript : Spawnable
    {
        #region Variables
        [SerializeField] ObstacleGlow oGlow;
        [SerializeField] ObstacleType type;
        [SerializeField] Collider collider;
        [SerializeField] Renderer renderer;
        [SerializeField] ObstacleAudio oAudio;  

        private Color normalColor => renderer.material.color;

        public enum ObstacleType
        {
            circleForm, sForm, triangleForm, wall
        }
        public ObstacleType Type { get => type; }
        #endregion

        #region Methods
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
            oGlow.Resetcolor();
        }
        private void OnTriggerEnter(Collider other)
        {
            other.GetComponent<HungerMeter>().SetHunger(0);
            oAudio.PlayCrashClip();
        }
        #endregion

    }
}

