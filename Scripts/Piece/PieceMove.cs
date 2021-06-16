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
        [SerializeField] Vector3 endPos;
        [SerializeField] float moveSpeed;
        [SerializeField] List<SpawnerController> spawnerControllers;
        [SerializeField] int maxPiecePerLane = 4;

        [SerializeField] List<Spawnable> spawnables;
        [SerializeField] List<Spawnable> s1;
        [SerializeField] List<Spawnable> s2;
        [SerializeField] List<Spawnable> s3;
        
        int previouspoint;
        int currentpoint;
        #endregion
        private void Update()
        {
            transform.position = SetTrajectory;
            DoInEndPos();
        }

        #region Add Spawanable funcitonality
        public void AddToPoint()
        {
            int currentPieceAmount = 0;
            for(int i = 0; i < spawnerControllers.Count; i++)
            {
                
                for (int j = 0; j < spawnerControllers[i].spawnPoints.Count; j++)
                {
                    CheckRulesForSpawning(i, j);
                    if(currentPieceAmount <= maxPiecePerLane)
                    {
                        switch (currentpoint)
                        {
                            case 0: break;
                            case 1: SpawnToPoint(s1, i, j); currentPieceAmount++; break;
                            case 2: SpawnToPoint(s2, i, j); currentPieceAmount++; break;
                            case 3: SpawnToPoint(s3, i, j); currentPieceAmount++; break;
                            default: break;
                        }
                        
                    }
                }
                currentPieceAmount = 0;
            }
        }
        void CheckRulesForSpawning(int i, int j)
        {
            currentpoint = RandomPoint(4);
            //Chequeo que en el spot previo hay una pared o obstaculo
            if (previouspoint == 3 && (currentpoint == 3 || currentpoint == 2))
            {

                currentpoint = RandomPoint(2);
                print("Wall was before, chaning to new point: " + currentpoint);
                //Si esta no es la primera fila chequeo si hay para los costados
                if (i > 0)
                {
                    if (spawnerControllers[i - 1].spawnPoints[j].GetComponentInChildren<Spawnable>() is WallTrigger)
                    {
                        print("Wall was next in lane, chaning to new point: " + currentpoint);
                        currentpoint = RandomPoint(3);
                    }
                }
            }
            //Si el anterior es obstaculo chequea que el siguiente no sea una pared
            else if (previouspoint == 2 && currentpoint == 3)
            {
                currentpoint = RandomPoint(3);
                print("Obstacle was before, chagning to new point" + currentpoint);
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
                if (spawnable is ConsumableScript) s1.Add(spawnable); 
                else if (spawnable is ObstacleScript) s2.Add(spawnable); 
                else if (spawnable is WallTrigger) s3.Add(spawnable);

                spawnables.Add(spawnable);
            } 
        }
        public void SetActiveSpawnable()
        {
            foreach(Spawnable s in spawnables)
            {
                s.gameObject.SetActive(true);
            }
        }
        #endregion

        #region Set trajectory/Moving functionality
        int RandomPoint(int count) => Random.Range(0, count);
        Vector3 SetTrajectory => Vector3.MoveTowards(transform.position, endPos, moveSpeed * Time.deltaTime);
        void DoInEndPos() { if (IsInEndPos) Deactivation(); }
        bool IsInEndPos => transform.position == endPos;
        public Vector3 EndPos { get => endPos; set => endPos = value; }
        #endregion
        public override void Deactivation()
        {
            foreach (Spawnable s in spawnables)
            {
                s.Deactivation();
            }
            s1.Clear(); s2.Clear(); s3.Clear(); spawnables.Clear();
            base.Deactivation();
            
            
        }

    }
}

