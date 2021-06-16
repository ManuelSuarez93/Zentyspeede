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
        [SerializeField] PieceFactory factory;

        float currentTime = 0f;

        #endregion
        #region Unity Methods
        private void Start()
        {
            currentTime = maxTime;
        }
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
                factory.CreatePiece(spawnPoint.position, endPoint.position);
                currentTime = 0;
            }
        }

        private void SpecialSpawn()
        {

        }

        
        #endregion
    }

}
