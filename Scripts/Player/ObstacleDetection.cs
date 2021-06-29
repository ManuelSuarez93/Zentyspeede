using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using ZentySpeede.Obstacle;

namespace ZentySpeede.Player
{
    public class ObstacleDetection : MonoBehaviour
    {
        #region Variables
        [Header("Distances")]
        [Tooltip("Minimum distance needed to pass")]
        [SerializeField] float passDistance = 50;
        [Tooltip("Minimum distance needed to do a perfect pass")]
        [SerializeField] float greatPassDistance = .2f;

        [Tooltip("Intensity of detected obstacle's glow")]
        [SerializeField] float intensity;
        [SerializeField] float glowMultiplier;
        [SerializeField] LayerMask detectionLayer;

        [Header("Debug options")]
        [SerializeField] bool debug;
        [SerializeField] Text textDebugDistance;
        [SerializeField] Text finalDistance;

        ObstacleGlow oGlow;
        RaycastHit hit;
        #endregion

        #region Methods
        private void Start()
        {
            StartCoroutine(PerformRaycast());
        }
        private void Update()
        {
            if (debug) Debugging();
        }
        public ObstacleScript GetDetectedObject()
        {
            if (hit.collider != null && GetObstacleScript() != null) return GetObstacleScript();
            else return null;
        }
       IEnumerator PerformRaycast()
        {
            while(true)
            {
                DoRaycast();
                if (hit.collider != null) 
                {
                    oGlow = GetObstacleGlow();
                    if (oGlow != null)
                    {
                        oGlow.ModifyGlow((passDistance / hit.distance) + (intensity * (Time.deltaTime * glowMultiplier)));
                    }
                    else
                    {
                        Debugger.instance.DebugMessage(hit.collider.name + "no tiene collider", Debugger.DebugType.Log);
                    }
                }

                yield return null;
            }
        }
        #endregion

        #region One Line Methods
        ObstacleScript GetObstacleScript() => hit.collider.GetComponent<ObstacleScript>();
        ObstacleGlow GetObstacleGlow() => hit.collider.gameObject.GetComponent<ObstacleGlow>();
        void DoRaycast() => Physics.Raycast(transform.position, transform.right, out hit, passDistance, detectionLayer);
        public bool ObstacleGreatPass() => hit.distance < greatPassDistance;
        #endregion

        void Debugging()
        {
            if(Debugger.instance.enableDebug)
            {
                Debug.DrawRay(transform.position, transform.right, Color.red);
                textDebugDistance.text = $"Distance:{(passDistance / hit.distance)}";
                finalDistance.text = $"Glow Dist:{((passDistance / hit.distance) + (intensity * Time.deltaTime))}";
            }
        }
    }


}


