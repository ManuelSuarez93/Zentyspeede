using System.Collections;
using UnityEngine;

namespace ZentySpeede.Player
{
    public class MoveCharacter : MonoBehaviour
    {
        #region Variables
        [SerializeField] Transform pivot;
        [SerializeField] float angleTurn;
        [SerializeField] float rotationTime;
        [SerializeField] Transform vcam;

        InputManager inputManager;
        public bool rotating = false;
        public float currentAngle = 0;
        #endregion

        #region Unity Methods
        private void Awake() => inputManager = GetComponent<InputManager>();
        void Update()
        {
            if(!inputManager.IsInMorph)
            {
                Rotate();
            }
        }
        #endregion
        #region Methods
        void Rotate()
        {
            if(!rotating)
            {
                if (inputManager.IsLeft)
                {
                    StartCoroutine(Rotation(false));
                }
                if (inputManager.IsRight)
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
        #endregion

    }
}


