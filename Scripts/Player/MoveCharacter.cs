using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace ZentySpeede.Player
{
    public class MoveCharacter : MonoBehaviour
    {
        [SerializeField] Transform pivot;
        [SerializeField] float angleTurn;
        [SerializeField] float rotationTime;
        [SerializeField] Transform vcam;
        
        public bool rotating = false;
        public float currentAngle = 0;

        // Update is called once per frame
        void Update()
        {
            if(!Input.GetButton(Inputs.INPUT_MORPH))
            {
                Rotate();
            }
        }

        void Rotate()
        {
            if(!rotating)
            {
                if (Input.GetButtonDown(Inputs.INPUT_LEFT))
                {
                    StartCoroutine(Rotation(false));
                }
                if (Input.GetButtonDown(Inputs.INPUT_RIGHT))
                {
                    StartCoroutine(Rotation(true));
                }
            }
        }

        public IEnumerator Rotation(bool isRight)
        {
            rotating = true;
            float targetAngle = currentAngle + (isRight ? -60 : 60);

            float timer = 0;
            while(timer < rotationTime)
            {
                float finalAngle = Mathf.Lerp(currentAngle, targetAngle, timer / rotationTime);
                pivot.localEulerAngles = new Vector3(finalAngle, 0, 0);
                vcam.localEulerAngles = new Vector3(vcam.localEulerAngles.x, vcam.localEulerAngles.y, finalAngle);
                timer += Time.deltaTime;
                yield return null;
            }

            pivot.localEulerAngles = new Vector3(targetAngle, 0, 0);
            vcam.localEulerAngles = new Vector3(vcam.localEulerAngles.x, vcam.localEulerAngles.y, targetAngle);
            currentAngle = targetAngle;
            rotating = false;
        }

    }
}


