using UnityEngine;

namespace ZentySpeede.Obstacle
{
    public class ObstacleGlow : MonoBehaviour
    {
        [SerializeField] Renderer render;
        [SerializeField] Material material;
        Color originalColor;
        void Awake() => originalColor = material.GetColor("_EmissionColor");
        public void ModifyGlow(float amount) => render.material.SetColor("_EmissionColor", material.GetColor("_EmissionColor") * amount);
        public void Resetcolor() => render.material.SetColor("_EmissionColor", originalColor);
    }
   
}