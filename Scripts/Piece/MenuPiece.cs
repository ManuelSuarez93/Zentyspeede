using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPiece : MonoBehaviour
{
    [SerializeField] Renderer render;
    [SerializeField] float speed = 1f;

    private void Update() 
        => render.material.mainTextureOffset = new Vector2(
            (render.material.mainTextureOffset.x), 
            (render.material.mainTextureOffset.y + Time.time) * speed
            );
}
