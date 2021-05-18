using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZentySpeede.Audio;
using ZentySpeede.General;

namespace ZentySpeede.Obstacle
{
    public class ObstacleScript : MovingObject
    {
        [SerializeField] ObstacleType type;
        [SerializeField] Collider collider;
        [SerializeField] MeshRenderer renderer;
        [SerializeField] ObstacleAudio oAudio;

        private bool canPass = false;



        public enum ObstacleType
        {
            circleForm, sForm, triangleForm
        }
        public ObstacleType Type { get => type; }

        public void SetPass(bool value)
        {
            canPass = value;
        }

        public void PassedAction()
        {
            oAudio.PlayEndClip();
            collider.enabled = false;
            renderer.material.color = Color.blue;
            
        }
    }
}

