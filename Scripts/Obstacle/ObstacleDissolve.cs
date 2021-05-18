using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZentySpeede.Obstacle
{
    public class ObstacleDissolve : MonoBehaviour
    {
        [SerializeField] Renderer rend;
        [SerializeField] float maxTime;
        [SerializeField] Color startColor;
        [SerializeField] Color endColor;
        float timer;
        private void Start()
        {
            timer = maxTime;
        }
        private void Update()
        {
            if (timer >= 0)
            {
                timer -= Time.deltaTime;
                rend.material.SetFloat("_Amount", timer / maxTime);
                rend.material.SetColor("_EmissionColor", Color.Lerp(startColor, endColor, timer / maxTime));
            }
        }
    }
}

