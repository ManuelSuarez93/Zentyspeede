using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZentySpeede.General;
using ZentySpeede.Obstacle;
using ZentySpeede.Player;

namespace ZentySpeede.Piece
{
    public class PieceMove : Spawnable
    {
        #region Varaibles
        Vector3 endPos;
        [SerializeField] Transform endSpawnPos;
        [SerializeField] List<SpawnerController> spawnerControllers;

        List<Spawnable> spawnables;
        List<Spawnable> s1;
        List<Spawnable> s2;
        List<Spawnable> s3;

        private static float moveSpeed = 20;
        public static float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }
        public Transform EndSpawnPos { get => endSpawnPos; set => endSpawnPos = value; }
        int previouspoint;
        int currentpoint;
        #endregion

        #region Unity Methods
        private void Awake()
        {
            spawnables = new List<Spawnable>();
            s1 = new List<Spawnable>();
            s2 = new List<Spawnable>();
            s3 = new List<Spawnable>();
        }
        private void Update()
        {
            transform.position = SetTrajectory;
            DoInEndPos();
        }
        #endregion

        #region Add Spawanable funcitonality
        public void AddToPoint()
        {
            int currentPieceAmount = 0;
            for(int i = 0; i < spawnerControllers.Count; i++)
            {
                
                for (int j = 0; j < spawnerControllers[i].spawnPoints.Count; j++)
                {
                    CheckRulesForSpawning(i, j);
                    switch (currentpoint)
                    {
                        case 0: break;
                        case 1: SpawnToPoint(s1, i, j); currentPieceAmount++; break;
                        case 2: SpawnToPoint(s2, i, j); currentPieceAmount++; break;
                        case 3: SpawnToPoint(s3, i, j); currentPieceAmount++; break;
                        default: break;
                    }
                        
                    
                }
                currentPieceAmount = 0;
            }
        }
        void CheckRulesForSpawning(int i, int j)
        {
            currentpoint = RandomPoint(4);
            if (previouspoint == 3 && (currentpoint == 3 || currentpoint == 2))
            {

                currentpoint = RandomPoint(2);
                if (i > 0)
                {
                    if (spawnerControllers[i - 1].spawnPoints[j].GetComponentInChildren<Spawnable>() is WallTrigger)
                    {
                        currentpoint = RandomPoint(3);
                    }
                }
            }
            else if (previouspoint == 2 && currentpoint == 3)
            {
                currentpoint = RandomPoint(3);
            }
            previouspoint = currentpoint;
        }
        void SpawnToPoint(List<Spawnable> l, int i, int j)
        {
            if(l.Count > 0)
            {
                l[0].transform.parent = spawnerControllers[i].spawnPoints[j].transform;
                l[0].transform.localPosition = Vector3.zero;
                l[0].transform.localEulerAngles = Vector3.zero;
                l.RemoveAt(0);
            }
        }
        public void AddSpawnableToLists(Spawnable spawnable)
        { 
            if (spawnable != null) 
            {
                if (spawnable is ConsumableScript || spawnable is NoHungerPowerUp) s1.Add(spawnable); 
                else if (spawnable is ObstacleScript) s2.Add(spawnable); 
                else if (spawnable is WallTrigger) s3.Add(spawnable);

                spawnables.Add(spawnable);
            } 
        }
        public void SetActiveSpawnable()
        {
            Debugger.instance.DebugMessage($"<color=yellow> Piece:{gameObject} starting spawn time = {Time.time}</color>", Debugger.DebugType.Log);
            foreach(Spawnable s in spawnables)
            {
                s.gameObject.SetActive(true);
            }
            Debugger.instance.DebugMessage($"<color=blue> Piece:{gameObject} finishing spawn time = {Time.time}</color>", Debugger.DebugType.Log);
        }
        #endregion

        #region Set trajectory/Moving functionality
        int RandomPoint(int count) => Random.Range(0, count);
        Vector3 SetTrajectory => Vector3.MoveTowards(transform.position, endPos, moveSpeed * Time.deltaTime);
        void DoInEndPos() { if (IsInEndPos) Deactivation(); }
        bool IsInEndPos => transform.position == endPos;
        public Vector3 EndPos { get => endPos; set => endPos = value; }
        
        public override void Deactivation()
        {
            foreach (Spawnable s in spawnables)
            {
                s.Deactivation();
            }
            s1.Clear(); s2.Clear(); s3.Clear(); spawnables.Clear();
            base.Deactivation();


        }
        #endregion


    }
}

