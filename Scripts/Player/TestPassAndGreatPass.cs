using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZentySpeede.Player
{
    public class TestPassAndGreatPass : MonoBehaviour
    {
        private MeshRenderer meshRenderer;
        // Start is called before the first frame update
        void Start()
        {
            meshRenderer = GetComponent<MeshRenderer>();
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void ChangeToGreen()
        {
            meshRenderer.material.color = Color.green;
        }

        public void ChangeToYellow()
        {
            meshRenderer.material.color = Color.yellow;
        }

        public void ChangeToRed()
        {
            meshRenderer.material.color = Color.red;
        }


    }
}

