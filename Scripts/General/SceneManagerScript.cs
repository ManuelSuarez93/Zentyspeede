using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour
{
    #region Singleton
    private static SceneManagerScript _instance;
    public static SceneManagerScript Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<SceneManagerScript>();

                if (_instance == null)
                {
                    GameObject container = new GameObject("SelectionManager");
                    _instance = container.AddComponent<SceneManagerScript>();
                }
            }

            return _instance;
        }
    }


    private void Awake()
    {
        if (Instance == null && _instance != this)
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

    #region Methods
    public void Play(int i) => SceneManager.LoadScene(i);

    public void Quit() => Application.Quit();

    public void ReturnToMainMenu(int i) => SceneManager.LoadScene(i);
    #endregion
}
