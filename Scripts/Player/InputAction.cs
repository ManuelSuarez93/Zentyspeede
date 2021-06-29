using UnityEngine;
using UnityEngine.Events;
using ZentySpeede.Obstacle;
using ZentySpeede.Animations;

namespace ZentySpeede.Player
{
    [RequireComponent(typeof(ObstacleDetection))]
    [RequireComponent(typeof(AnimationController))]
    public class InputAction : MonoBehaviour
    {
        #region Variables
        
        [SerializeField] UnityEvent successEvent;
        [SerializeField] UnityEvent greatSuccessEvent;
        [SerializeField] UnityEvent failEvent;
        [SerializeField] UnityEvent onChangeEvent;
        
        [SerializeField] ObstacleDetection obstacleDetector;
        [SerializeField] Renderer mesh;
        [SerializeField] AnimationController animController;

        bool allowedOn;
        #endregion

        #region Unity Methods
        private void Awake() => TryGetComponent<ObstacleDetection>(out obstacleDetector);
        void Update() => IsTheCorrectInput();
        #endregion

        #region Methods
        public void IsTheCorrectInput()
        {
            if (Input.GetButton(Inputs.INPUT_MORPH))
            {
                if (Input.GetButtonDown(Inputs.INPUT_UP))
                {
                    ProcessInput(Color.magenta, Inputs.INPUT_UP);
                    onChangeEvent.Invoke();
                    animController.ChangeTo("BallTransform");
                }
                else if (Input.GetButtonDown(Inputs.INPUT_RIGHT))
                {
                    ProcessInput(Color.green, Inputs.INPUT_RIGHT);
                    onChangeEvent.Invoke();
                    animController.ChangeTo("BallTransform");
                }
                else if (Input.GetButtonDown(Inputs.INPUT_LEFT))
                {
                    ProcessInput(Color.yellow, Inputs.INPUT_LEFT);
                    onChangeEvent.Invoke();
                    animController.ChangeTo("STransform");
                }
            }
            else
            {
                changeColorOnInput(Color.blue);
            }

        }

        private void ProcessInput(Color color, string input)
        {
            changeColorOnInput(color);
            var detectedObject = obstacleDetector.GetDetectedObject();
            if (isCorrectInputForShape(input, detectedObject))
            {
                detectedObject.PassedAction();
                if (obstacleDetector.ObstacleGreatPass())
                {
                    greatSuccessEvent.Invoke();
                    Debugger.instance.DebugMessage($"Great Pass on: {detectedObject.name}", Debugger.DebugType.Log);
                }
                else
                {
                    successEvent.Invoke();
                    Debugger.instance.DebugMessage($"Pass on:{detectedObject.name}", Debugger.DebugType.Log);
                }
            }
            else
            {
                failEvent.Invoke();
            }
        }

        private bool isCorrectInputForShape(string input, ObstacleScript detectedObstacle)
        {
            if (detectedObstacle == null) return false;
            else
            {
                return (input == Inputs.INPUT_UP && detectedObstacle.Type == ObstacleScript.ObstacleType.circleForm) ||
                       (input == Inputs.INPUT_LEFT && detectedObstacle.Type == ObstacleScript.ObstacleType.sForm) ||
                       (input == Inputs.INPUT_RIGHT && detectedObstacle.Type == ObstacleScript.ObstacleType.triangleForm);
            }
        }

        private void changeColorOnInput(Color color)
        {
            mesh.material.color = color;
        }
        #endregion
    }
}