using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour
{
    private static SceneManagerScript _instance;
    public static SceneManagerScript instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<SceneManagerScript>();
                if (_instance == null)
                {
                    GameObject container = new GameObject("SceneManager");
                    _instance = container.AddComponent<SceneManagerScript>();
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

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Play(int i)
    {
        SceneManager.LoadScene(i);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void ReturnToMainMenu(int i)
    {
        SceneManager.LoadScene(i);
    }
}
