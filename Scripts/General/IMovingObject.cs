using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZentySpeede.General
{
    public abstract class MovingObject : MonoBehaviour
    {
        [SerializeField] Vector3 endPos;
        [SerializeField] float moveSpeed;
        private void Update()
        {
            transform.position = SetTrajectory;
            DestroyObstacle();
        }
        protected float speed { get; set; }
        protected Vector3 SetTrajectory => Vector3.MoveTowards(transform.position, endPos, moveSpeed * Time.deltaTime);
        protected bool IsInEndPos => transform.position == endPos;
        protected void DestroyObstacle() { if (IsInEndPos) Destroy(gameObject); }
        public Vector3 EndPos { get => endPos; set => endPos = value; }
    }
}

