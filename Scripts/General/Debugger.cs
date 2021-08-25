using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Debugger : MonoBehaviour
{
    #region Singleton
    private static Debugger _instance;
    public static Debugger instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<Debugger>();
                if (_instance == null)
                {
                    GameObject container = new GameObject("Debugger");
                    _instance = container.AddComponent<Debugger>();
                }
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if (instance == null && _instance != this)
        {
            Destroy(this.gameObject);
        }

        else
        {
            _instance = this;
        }

        DontDestroyOnLoad(this.gameObject);

    }
    #endregion

    [SerializeField] List<Text> uiText;
    [SerializeField] Text textbox;
    public bool enableDebug;
    public enum DebugType
    {
        Log, Error, Warning
    }
    public void DebugMessage(string msg, DebugType debug)
    {
        if(enableDebug)
        {
            switch (debug)
            {
                case DebugType.Log: Debug.Log(msg); break;

                case DebugType.Warning: Debug.LogWarning(msg); break;

                case DebugType.Error: Debug.LogError(msg); break;

                default:
                    Debug.Log(msg);
                    break;
            }

            textbox.text = msg + "\n" + textbox.text;
        }
        
    }

    public void DebugTextUI(int i, string msg)
    {
        if(uiText.Count >= i && uiText[i] != null)
        {
            uiText[i].text = msg;
        }
    }
    
}
