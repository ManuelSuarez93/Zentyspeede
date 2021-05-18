using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace ZentySpeede.Piece
{
    public class TriggerPiece : MonoBehaviour
    {
        GameObject objectInTrigger;
        [SerializeField] UnityEvent triggerEvent;
        private void OnTriggerEnter(Collider other)
        {
            objectInTrigger = other.gameObject;
            triggerEvent.Invoke();
        }

        public void DestroyObject()
        {
            Destroy(objectInTrigger);
        }
    }
}

