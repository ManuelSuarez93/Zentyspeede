using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;
using ZentySpeede.Piece;
using UnityEngine.Events;

namespace ZentySpeede.General
{
    public class LevelScript : MonoBehaviour
    {
        #region Variables
        [SerializeField] Transform spawnPoint, player,endPoint;
        [SerializeField] float maxTime;
        [SerializeField] List<GameObject> pieces;
        [SerializeField] int MaxAllowedPieces;
        [SerializeField] UnityEvent spawnEvent;

        float currentTime = 0f;
        MovingObject movingObj;

        private int RandomNumber() => Random.Range(0, pieces.Count - 1);
        #endregion
        #region Unity Methods

        private void Update()
        {
            Timer();
        }

        private void Timer()
        {
            if(currentTime < maxTime)
            {
                currentTime += Time.deltaTime;
            }
            else
            {
                spawnEvent.Invoke();
                currentTime = 0;
            }
        }

        public void SpawnObject()
        {
            GameObject o = Instantiate(pieces[RandomNumber()].gameObject);
            movingObj = o.GetComponentInChildren<MovingObject>();
            movingObj.EndPos = endPoint.position;
        }
        #endregion
    }

}
