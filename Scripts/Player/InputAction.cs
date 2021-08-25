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
        [SerializeField] Renderer mesh;

        ObstacleDetection obstacleDetector;
        AnimationController animController;
        InputManager inputManager;

        #endregion

        #region Unity Methods
        private void Awake()
        {
            obstacleDetector = GetComponent<ObstacleDetection>();
            animController = GetComponent<AnimationController>();
            inputManager = GetComponent<InputManager>();
        }

        void Update() => IsTheCorrectInput();
        #endregion

        #region Methods


        private void IsTheCorrectInput()
        {
            if (inputManager.IsInMorph)
            {
                if (inputManager.IsUp && !inputManager.IsLeft && !inputManager.IsRight)
                {
                    ProcessInput(Inputs.INPUT_UP);
                    onChangeEvent.Invoke();
                    animController.ChangeTo("BallTransform");
                    inputManager.ResetTimer();
                }
                else if (inputManager.IsRight && !inputManager.IsLeft && !inputManager.IsUp)
                {
                    ProcessInput(Inputs.INPUT_RIGHT);
                    onChangeEvent.Invoke();
                    animController.ChangeTo("LTransform");
                    inputManager.ResetTimer();
                }
                else if (inputManager.IsLeft && !inputManager.IsUp && !inputManager.IsRight)
                {
                    ProcessInput(Inputs.INPUT_LEFT);
                    onChangeEvent.Invoke();
                    animController.ChangeTo("STransform");
                    inputManager.ResetTimer();
                }
            }

            else
            {
                if (inputManager.IsLeft)
                {
                    animController.ChangeTo("Izq");
                    inputManager.ResetTimer();
                }

                else if (inputManager.IsRight)
                {
                    animController.ChangeTo("Der");
                    inputManager.ResetTimer();
                }
            }
        }
        public void ProcessInput(string input)
        {

            var detectedObject = obstacleDetector.GetDetectedObject();
            if (isCorrectInputForShape(input, detectedObject))
            {
                detectedObject.PassedAction();
                if (obstacleDetector.ObstacleGreatPass())
                {
                    greatSuccessEvent.Invoke();
                    //Debugger.instance.DebugMessage($"Great Pass on: {detectedObject.name}", Debugger.DebugType.Log);
                }
                else
                {
                    successEvent.Invoke();
                    //Debugger.instance.DebugMessage($"Pass on:{detectedObject.name}", Debugger.DebugType.Log);
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
        #endregion
    }
}