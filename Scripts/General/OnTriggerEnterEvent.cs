using UnityEngine;
using UnityEngine.Events;
using ZentySpeede.General;

namespace ZentySpeede.Piece
{
    public class OnTriggerEnterEvent : MonoBehaviour
    {
        [SerializeField] UnityEvent triggerEvent;

        private void OnTriggerEnter(Collider other)
        {
 
        }


    }

}

