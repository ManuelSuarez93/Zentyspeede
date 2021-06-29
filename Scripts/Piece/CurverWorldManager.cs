using AmazingAssets.CurvedWorld;
using UnityEngine;

public class CurverWorldManager : MonoBehaviour
{
    #region Variables
    [Header("CurvedWorld component")]
    [SerializeField] CurvedWorldController worldController;

    [Header("Curved world limits ")]
    [SerializeField] float limitHorizontalSize = 4;
    [SerializeField] float limitVerticalSize = 3;

    [Header("Wave seetings")]
    [SerializeField] float sizeChangeTime = 10;
    [SerializeField] float sizeChangeTimer = 10;
    [Header("Timer settings")]

    [SerializeField] float timeOnTakeCurve = 7;
    [SerializeField] float timerOnTakeCurve = 0;


    float lastHorizontalSize;
    float lastVerticalSize;
    float newHorizontalSize;
    float newVerticalSize;
    bool doCurve = false;
    #endregion

    #region Methods
    void Update()
    {
        if(doCurve)
        {
            lastHorizontalSize = newHorizontalSize;
            lastVerticalSize = newVerticalSize;
            if(lastHorizontalSize != 0 && lastVerticalSize != 0)
            {
                newHorizontalSize = 0;
                newVerticalSize = 0;
            }
            else
            {
                newHorizontalSize = RandomNumber().x;
                newVerticalSize = RandomNumber().y;
            }
            doCurve = false;
        }

        if(sizeChangeTimer > 0)
        {
            LerpWorldControllerSize(lastHorizontalSize, newHorizontalSize, lastVerticalSize, newVerticalSize);
            timerOnTakeCurve += Time.deltaTime;
            sizeChangeTimer -= Time.deltaTime;
        }

        else
        {
            doCurve = true;
            timerOnTakeCurve = 0;
            sizeChangeTimer = sizeChangeTime;
        }
    }

    private void LerpWorldControllerSize(float horizontal1, float horizontal2, float vertical1, float vertical2)
    {
        worldController.bendHorizontalSize = Mathf.Lerp(horizontal1, horizontal2, timerOnTakeCurve / timeOnTakeCurve);
        worldController.bendVerticalSize = Mathf.Lerp(vertical1, vertical2, timerOnTakeCurve / timeOnTakeCurve);
        if (timerOnTakeCurve > timeOnTakeCurve)
        {
            timerOnTakeCurve = timeOnTakeCurve;
        }
    }

    private Vector2 RandomNumber() => new Vector2((Random.Range(-limitHorizontalSize, limitHorizontalSize)), Random.Range(-limitVerticalSize, limitVerticalSize));

    #endregion
}
