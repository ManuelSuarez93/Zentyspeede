using UnityEngine;
using UnityEngine.Events;

namespace ZentySpeede.Piece
{
    [RequireComponent(typeof(Collider))]
    public class OnTriggerEnterEvent : MonoBehaviour
    {
        [SerializeField] UnityEvent triggerEvent;
        private void OnTriggerEnter(Collider other) => triggerEvent.Invoke();

    }

}

