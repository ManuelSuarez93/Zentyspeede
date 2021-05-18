using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using ZentySpeede.Audio;
using ZentySpeede.General;

namespace ZentySpeede.Obstacle
{
    public class ObstacleSpawner : MonoBehaviour
    {
        [SerializeField] GameObject consumableObject;
        [SerializeField] GameObject obstacleObject;
        [SerializeField] Conductor conductor;
        [SerializeField] Transform endPos;

        ObstacleScript obstacle;
        MovingObject movingObj;

        private void Awake()
        {
            GameObject.Find("Conductor").TryGetComponent(out conductor);
        }
        public void SpawnObstacle()
        {
            GameObject o = Instantiate(obstacleObject, transform);
            o.TryGetComponent<ObstacleScript>(out obstacle);
            o.GetComponent<ObstacleScript>().EndPos = endPos.position;
        }

        public void SpawnConsumable()
        {
            GameObject o = Instantiate(consumableObject, transform);
            movingObj = o.GetComponentInChildren<MovingObject>();
            movingObj.EndPos = endPos.position;
        }





    }
}

