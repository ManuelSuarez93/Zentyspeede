using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZentySpeede.Obstacle
{
    public class ObstacleDissolve : MonoBehaviour
    {
        MeshRenderer rend;
        [SerializeField] float maxTime;
        [SerializeField] Color startColor;
        [SerializeField] Color finalColor;
        [SerializeField] Material finalMaterial;
        [SerializeField] Material startMaterial;
        
        float timer;
        private void Awake()
        {
            
        }
        private void Start()
        {
            rend = GetComponentInChildren<MeshRenderer>();
            rend.material = startMaterial;
            timer = maxTime;
        }
        private void Update()
        {
            if (timer >= 0)
            {   timer -= Time.deltaTime;
                rend.material.SetFloat("_Amount", timer / maxTime);
                rend.material.SetColor("_EmissionColor", Color.Lerp(startColor, finalColor, timer / maxTime));
            }
            else
            {
                rend.material = finalMaterial;
            }
        }
    }
}

