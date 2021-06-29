using UnityEngine;
using ZentySpeede.General;

namespace ZentySpeede.Piece
{
    public class PieceFactory : MonoBehaviour
    {
        #region Variables
        [Header("Prefab pools")]
        [SerializeField] PrefabPool piecePool;
        [SerializeField] PrefabPool obstaclePool;
        [SerializeField] PrefabPool wallPool;
        [SerializeField] PrefabPool consumablePool;

        [Header("Prefab amount")]
        [Tooltip("Amount of obstacles")]
        [Range(0, 72)]
        [SerializeField] int ObstacleAmount;

        [Tooltip("Amount of walls")]
        [Range(0, 72)]
        [SerializeField] int WallAmount;

        [Tooltip("Amount of consumables")]
        [Range(0, 72)]
        [SerializeField] int ConsumableAmount;

        int RandomAmount(int count) => Random.Range(0, count);
        #endregion
        public Spawnable CreatePiece(Vector3 startPos, Vector3 endPos)
        {
            Spawnable newPiece = piecePool.InstanceObject(startPos, endPos);
            if (newPiece != null)
            {
                for (int i = 0; i < RandomAmount(ObstacleAmount); i++)
                {
                    Spawnable obstacle = obstaclePool.InstanceObject(startPos);
                    newPiece.GetComponent<PieceMove>().AddSpawnableToLists(obstacle);
                }
                for (int i = 0; i < RandomAmount(WallAmount); i++)
                {
                    Spawnable wall = wallPool.InstanceObject(startPos);
                    newPiece.GetComponent<PieceMove>().AddSpawnableToLists(wall);
                }
                for (int i = 0; i < RandomAmount(ConsumableAmount); i++)
                {
                    Spawnable consumable = consumablePool.InstanceObject(startPos);
                    newPiece.GetComponent<PieceMove>().AddSpawnableToLists(consumable);
                }

                newPiece.GetComponent<PieceMove>().AddToPoint();
            }

            return newPiece;
        } 

    }
}

