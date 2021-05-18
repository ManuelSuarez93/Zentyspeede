using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using ZentySpeede.Obstacle;
using ZentySpeede.Player;

public static class Inputs
{
    public static readonly string INPUT_MORPH = "Jump";
    public static readonly string INPUT_UP = "Up";
    public static readonly string INPUT_LEFT = "Left";
    public static readonly string INPUT_RIGHT = "Right";
}
public class InputAction : MonoBehaviour
{
    [SerializeField] ObstacleDetection obstacle;
    [SerializeField] HungerMeter hunger;
    [SerializeField] UnityEvent successEvent;
    [SerializeField] UnityEvent failEvent;
    [SerializeField] bool allowedOn;

    [SerializeField] MeshRenderer mesh;

    private void Awake()
    {
        TryGetComponent<ObstacleDetection>(out obstacle);
    }
    void Update()
    {
        IsTheCorrectInput();
    }

    public void IsTheCorrectInput()
    {
        bool allowed = false;
        if (Input.GetButton(Inputs.INPUT_MORPH))
        {
            
            if (Input.GetButtonDown(Inputs.INPUT_UP))
            {
                allowed = isCorrectInputForShape(Inputs.INPUT_UP, obstacle.DetectedObstacle);
                changeColorOnInput(Color.magenta);
            }
            else if (Input.GetButtonDown(Inputs.INPUT_RIGHT))
            {
                allowed = isCorrectInputForShape(Inputs.INPUT_RIGHT, obstacle.DetectedObstacle);
                changeColorOnInput(Color.green);
            }
            else if (Input.GetButtonDown(Inputs.INPUT_LEFT))
            {
                allowed = isCorrectInputForShape(Inputs.INPUT_LEFT, obstacle.DetectedObstacle);
                changeColorOnInput(Color.yellow);
            }
        }
        else
        {
            changeColorOnInput(Color.blue);
        }
        if (obstacle.DetectedObstacle && obstacle.ObstaclePass())
        {
            obstacle.DetectedObstacle.SetPass(allowed);
            if (allowed) successEvent.Invoke();
        }
        if(obstacle.DetectedObstacle && obstacle.ObstacleNotPass())
        {
           failEvent.Invoke();
        }
        Debug.Log(allowed);
        allowedOn = allowed;

        
    }

    private bool isCorrectInputForShape(string input, ObstacleScript detectedObstacle)
    {
        if (detectedObstacle == null) return false;
        else
        {
            return (input == Inputs.INPUT_UP && detectedObstacle.Type == ObstacleScript.ObstacleType.circleForm) ||
                   (input == Inputs.INPUT_LEFT && detectedObstacle.Type == ObstacleScript.ObstacleType.sForm) ||
                   (input == Inputs.INPUT_RIGHT && detectedObstacle.Type == ObstacleScript.ObstacleType.triangleForm);
        }
    }

    private void changeColorOnInput(Color color)
    {
        mesh.material.color = color;
    }
}
