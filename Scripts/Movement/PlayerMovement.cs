using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private int routeToGo = 0;
    private float tiempo = 0f;
    private Vector3 playerPosition;
    private bool permitirCoroutine = true;
    private Vector3 prevPosition;

    [SerializeField]
    private float speedMod = 0.5f;
    [SerializeField]
    private Transform[] rutas;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(permitirCoroutine)
        {
            StartCoroutine(IrPorLaRuta(routeToGo));
        }
    }

    private IEnumerator IrPorLaRuta(int i)
    {
        permitirCoroutine = false;

        Vector3 p0 = rutas[i].GetChild(0).position;
        Vector3 p1 = rutas[i].GetChild(1).position;
        Vector3 p2 = rutas[i].GetChild(2).position;
        Vector3 p3 = rutas[i].GetChild(3).position;

        while (tiempo < 1)
        {
            Movement(p0, p1, p2, p3);
            //poner el foward a partir del frame actual y el siguiente
            yield return new WaitForEndOfFrame();
        }
        

        tiempo = 0f;
        routeToGo += 1;
        if(routeToGo > rutas.Length - 1) routeToGo = 0;
        permitirCoroutine = true;
    }

    private void Movement(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
    {
        tiempo += Time.deltaTime * speedMod;

        playerPosition = Mathf.Pow(1 - tiempo, 3) * p0 +
            3 * Mathf.Pow(1 - tiempo, 2) * tiempo * p1 +
            3 * (1 - tiempo) * Mathf.Pow(tiempo, 2) * p2 +
            Mathf.Pow(tiempo, 3) * p3;
        Vector3 direction = (playerPosition - prevPosition).normalized;
        transform.position = playerPosition;
        prevPosition = playerPosition;
        transform.forward = direction;
    }
}
