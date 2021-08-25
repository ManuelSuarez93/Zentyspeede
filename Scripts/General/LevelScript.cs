using UnityEngine;
using ZentySpeede.Piece;
using ZentySpeede.Audio;

namespace ZentySpeede.General
{
    public class LevelScript : MonoBehaviour
    {
        #region Variables
        [SerializeField] Transform spawnPoint, player,endPoint;
        [SerializeField] float maxTime;
        [SerializeField] PieceFactory factory;
        [SerializeField] float pieceLength;
        [SerializeField] AudioSource music;

        private Transform lastPieceTransform;
        float currentTime = 0f;

        #endregion

        #region Unity Methods
        private void Start()
        {
            AudioController.Instance.Music = music;
            currentTime = maxTime;
        }
        private void Update()
        {
            Timer();
        }

        #endregion

        #region Methods
        private void Timer()
        {
            if(currentTime < maxTime)
            {
                currentTime += Time.deltaTime;
            }
            else
            {
                if (lastPieceTransform == null)
                {
                    lastPieceTransform = factory.CreatePiece(spawnPoint.position, endPoint.position).transform;
                }
                else
                {
                    Spawnable spawnable = factory.CreatePiece(lastPieceTransform.GetComponent<PieceMove>().EndSpawnPos.position, endPoint.position);
                    if (spawnable == null)
                    {
                        return;
                    }
                    lastPieceTransform = spawnable.transform;
                }
                
                currentTime = 0;
            }
        }
        #endregion
    }

}
