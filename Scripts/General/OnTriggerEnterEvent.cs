using UnityEngine;
using UnityEngine.Events;

namespace ZentySpeede.Piece
{
    [RequireComponent(typeof(Collider))]
    public class OnTriggerEnterEvent : MonoBehaviour
    {
        [SerializeField] UnityEvent triggerEvent;
        private void OnTriggerEnter(Collider other) => triggerEvent.Invoke();
        public void ShowDebugMessage()
        {
            Debugger.instance.DebugMessage($"<color=green> Trigger actiavted at time: {Time.time}</color>", Debugger.DebugType.Log);
        }
    }

}

