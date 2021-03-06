using System.Collections.Generic;
using UnityEngine;
using ZentySpeede.General;
using ZentySpeede.Piece;

public class PrefabPool : MonoBehaviour
{
    #region Variables
    [SerializeField] List<Spawnable> instances;
    [SerializeField] List<Spawnable> availablePool;
    [SerializeField] bool setActiveOnStart;
    PieceMove piece;
    public List<Spawnable> PiecesPool { get => availablePool; }
    private int RandomNumber(int count) => Random.Range(0, count - 1);
    #endregion

    #region Methods
    private void Awake()
    {
        availablePool = new List<Spawnable>(); 
        foreach (Spawnable p in instances)
        {
            p.GetComponentInChildren<Spawnable>().Deactivate += DeactivateObject;
            availablePool.Add(p);
            p.gameObject.SetActive(false);
        }
    }
    public Spawnable InstanceObject(Vector3 position)
    {
        if (availablePool.Count < 1) return null;

        Spawnable sp = availablePool[RandomNumber(availablePool.Count)];
        sp.gameObject.SetActive(true);
        sp.transform.parent = null;
        sp.transform.position = position;
        availablePool.Remove(sp);
        sp.Initialization();

        if (!setActiveOnStart) sp.gameObject.SetActive(false);
        return sp;
    }
    public Spawnable InstanceObject(Vector3 position, Vector3 finalPosition)
    {
        if (availablePool.Count < 1) return null;

        Spawnable sp = availablePool[RandomNumber(availablePool.Count)];
        sp.gameObject.SetActive(true);
        sp.transform.parent = null;
        sp.transform.position = position;
        availablePool.Remove(sp);
        sp.Initialization();
        sp.GetComponent<PieceMove>().EndPos = finalPosition;

        if(!setActiveOnStart) sp.gameObject.SetActive(false);
        return sp;
    }
    public void DeactivateObject(Spawnable o)
    {
        if (availablePool.Contains(o)) return;
        o.gameObject.SetActive(false);
        availablePool.Add(o);
        o.transform.parent = gameObject.transform;
    }
    #endregion
}
