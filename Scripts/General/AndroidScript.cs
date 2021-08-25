
using UnityEngine;
using UnityEngine.Events;

public class AndroidScript : MonoBehaviour
{
    public UnityEvent androidEvent;
    private void Awake()
    {
        if(Application.platform == RuntimePlatform.Android)
        {
            androidEvent.Invoke();
        }
    }
}
