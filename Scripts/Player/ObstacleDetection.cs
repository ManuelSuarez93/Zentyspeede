using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using ZentySpeede.Obstacle;

namespace ZentySpeede.Player
{
    public class ObstacleDetection : MonoBehaviour
    {
        [SerializeField] float passDistance = 50;
        [SerializeField] LayerMask detectionLayer;
        [SerializeField] float greatPassDistance = .2f;
        [SerializeField] UnityEvent obstacleFeedbackOnPass;
        [SerializeField] UnityEvent obstacleFeedbackOnGreatPass;
        RaycastHit hit;

        private void Update()
        {
            Debug.DrawRay(transform.position, transform.right, Color.red);
        }
        public ObstacleScript GetDetectedObject()
        {
                if (Physics.Raycast(transform.position, transform.right, out hit, passDistance, detectionLayer))
                {
                    return hit.collider.gameObject.GetComponent<ObstacleScript>();
                }
                return null;
        }
        public bool ObstacleGreatPass() => hit.distance < greatPassDistance;
    }

    
}


