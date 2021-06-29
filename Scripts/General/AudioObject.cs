using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioObject : MonoBehaviour
{
    [SerializeField] float maxTime;
    [SerializeField] AudioSource aSource;
    float timer;
    private void Start()
    {
        aSource.pitch = Random.Range(0.80f, 1.05f);
    }
    void Update()
    {
        if (timer < maxTime) timer += Time.deltaTime;
        else { Destroy(gameObject); }
    }
}
