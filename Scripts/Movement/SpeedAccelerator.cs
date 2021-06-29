using UnityEngine;
using ZentySpeede.Piece;

public class SpeedAccelerator : MonoBehaviour
{
    [SerializeField]
    private float variableSpeed = 0;

    void Start()
    {
        PieceMove.MoveSpeed = 20;
        variableSpeed = 0;
    }
    void Update()
    {
        variableSpeed += Time.deltaTime;
        PieceMove.MoveSpeed = 20 + variableSpeed;
    }

    
}
