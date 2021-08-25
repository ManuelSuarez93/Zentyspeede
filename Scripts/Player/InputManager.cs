using UnityEngine;

namespace ZentySpeede.Player
{
    public static class Inputs
    {
        public static readonly string INPUT_MORPH = "Jump";
        public static readonly string INPUT_UP = "Up";
        public static readonly string INPUT_LEFT = "Left";
        public static readonly string INPUT_RIGHT = "Right";
        public static readonly string INPUT_AH = "Horizontal";
        public static readonly string INPUT_AV = "Vertical";
        public static readonly string INPUT_JOYH = "JoystickHorizontal";
        public static readonly string INPUT_JOYV = "JoystickVertical";
        public static readonly string INPUT_JOYM = "JoystickMorph";
        public static readonly string PAUSE = "Pause";
    }

    public class InputManager: MonoBehaviour
    {
        public bool IsInMorph;
        public bool IsLeft;
        public bool IsRight;
        public bool IsUp;
        public bool IsPause;

        [SerializeField] float maxTimeForInput = 0.1f;
        float timer;
        private void Update()
        {
            if(Application.platform != RuntimePlatform.Android)
            {
                SetInput();
            }
        }
        public void SetInput()
        {
            IsInMorph = Input.GetButton(Inputs.INPUT_MORPH) 
                || Input.GetButton(Inputs.INPUT_JOYM);

            IsPause = Input.GetButtonDown(Inputs.PAUSE);

            if(CanPressButton())
            {
                IsLeft = Input.GetButtonDown(Inputs.INPUT_LEFT)
                    || Input.GetAxis(Inputs.INPUT_AH) <= -1
                    || Input.GetAxis(Inputs.INPUT_JOYH) <= -1;
                IsRight = Input.GetButtonDown(Inputs.INPUT_RIGHT) 
                    || Input.GetAxis(Inputs.INPUT_AH) >= 1
                    || Input.GetAxis(Inputs.INPUT_JOYH) >= 1;
                IsUp = Input.GetButtonDown(Inputs.INPUT_UP) 
                    || Input.GetAxis(Inputs.INPUT_AV) >= 1
                    || Input.GetAxis(Inputs.INPUT_JOYV) >= 1;

            }
            else
            {
                IsLeft = false;
                IsRight = false;
                IsUp = false;
                timer += Time.deltaTime;
            }
        }

        public bool CanPressButton() => timer >= maxTimeForInput;
        public void ResetTimer() => timer = 0;
        public void SetMorph(bool a) => IsInMorph = a;
        public void SetUp(bool a) => IsUp = a;
        public void SetLeft(bool a) => IsLeft = a;
        public void SetRight(bool a) => IsRight = a;

        public void Log(string s) { if (Debugger.instance.enableDebug) Debug.Log($"Pressed {s}"); }
    }
}
