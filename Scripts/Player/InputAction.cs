using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
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
    [SerializeField] ObstacleDetection obstacleDetector;
    [SerializeField] HungerMeter hunger;
    [SerializeField] UnityEvent successEvent;
    [SerializeField] UnityEvent greatSuccessEvent;
    [SerializeField] UnityEvent failEvent;
    [SerializeField] UnityEvent onChangeEvent;
    [SerializeField] bool allowedOn;

    [SerializeField] Renderer mesh;
    [SerializeField] AnimationController animController;

    private void Awake()
    {
        TryGetComponent<ObstacleDetection>(out obstacleDetector);
    }
    void Update()
    {
        IsTheCorrectInput();
    }

    public void IsTheCorrectInput()
    {
        if (Input.GetButton(Inputs.INPUT_MORPH))
        {
            if (Input.GetButtonDown(Inputs.INPUT_UP))
            {
                ProcessInput(Color.magenta, Inputs.INPUT_UP);
                onChangeEvent.Invoke();
                animController.ChangeTo();
            }
            else if (Input.GetButtonDown(Inputs.INPUT_RIGHT))
            {
                ProcessInput(Color.green, Inputs.INPUT_RIGHT);
                onChangeEvent.Invoke();
                animController.ChangeTo();
            }
            else if (Input.GetButtonDown(Inputs.INPUT_LEFT))
            {
                ProcessInput(Color.yellow, Inputs.INPUT_LEFT);
                onChangeEvent.Invoke();
                animController.ChangeTo();
            }
        }
        else
        {
            changeColorOnInput(Color.blue);
        }
       
    }

    private void ProcessInput(Color color, string input)
    {
        changeColorOnInput(color);
        var detectedObject = obstacleDetector.GetDetectedObject();
        if (isCorrectInputForShape(input, detectedObject))
        {
            detectedObject.PassedAction();
            if (obstacleDetector.ObstacleGreatPass())
            {
                greatSuccessEvent.Invoke();
                Debug.Log("GreatPass");
            }
            else
            {
                successEvent.Invoke();
                Debug.Log("Pass");
            }
        }
        else
        {
            failEvent.Invoke();
        }
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
