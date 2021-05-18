using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.UI;
using ZentySpeede.Obstacle;

namespace ZentySpeede.Player
{
    public class ObstacleDetection : MonoBehaviour
    {
        [SerializeField] float maxDistanceDetection = 50;
        [SerializeField] float minDistanceDetection = 30;
        [SerializeField] LayerMask detectionLayer;
        [SerializeField] float passDistance = .2f;
        [SerializeField] float deathDistance = .1f;
        [SerializeField] Text distanceText;
        RaycastHit hit;

        [SerializeField]
        private ObstacleScript detectedObstacle;

        public ObstacleScript DetectedObstacle { get => detectedObstacle; }

        void Update()
        {
            DebugRay();
            DetectObstacle();
            Debug.Log("Obstacle not pass: " + ObstacleNotPass());
            distanceText.text = hit.distance.ToString();
        }

        private void DetectObstacle()
        {
            if (detectedObject)
            {
                if (detectedObject.GetComponentInParent<ObstacleScript>())
                {
                    SetDetectedObstacle(detectedObject);
                    //ChangeObstacleColor(detectedObject);
                }
                else
                {
                    detectedObstacle = null;
                }
            }
        }
        private GameObject detectedObject
        {
            get
            {
                if (Physics.Raycast(transform.position, transform.right, out hit, maxDistanceDetection, detectionLayer))
                {
                    return hit.collider.gameObject;
                }
                return null;
            }
        }
        private void ChangeObstacleColor(GameObject o) => o.GetComponent<MeshRenderer>().material.color = Color.red;
        private void SetDetectedObstacle(GameObject o) => detectedObstacle = o.GetComponentInParent<ObstacleScript>();
        private void DebugRay() => Debug.DrawRay(transform.position, transform.right, Color.green);
        public bool ObstaclePass() => hit.distance >= passDistance && hit.distance <= minDistanceDetection;
        public bool ObstacleNotPass() => hit.distance >= deathDistance && hit.distance < passDistance;

        public void ObstaclePassAction()
        {
            detectedObstacle.PassedAction();
        }
    }

    
}


