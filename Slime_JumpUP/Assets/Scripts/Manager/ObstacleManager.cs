using System.Collections.Generic;
using System.Linq;
using Obstacles;
using UnityEngine;

namespace Manager
{
    public class ObstacleManager
    {
        private List<Obstacle> _obstacles = new();

        private readonly string[] _rocks = 
        {
            "Stone_01","Stone_02", "Stone_03", "Stone_04", "Stone_05"
        };

        private const float OverlapRadius = 0.8f;
        private GameObject _obstacle;

        public GameObject BaseObstacle
        {
            get
            {
                if (_obstacle == null) _obstacle = GameObject.Find("@Obstacle") ?? new GameObject("@Obstacle");
                return _obstacle;
            }
        }

        public void SpawnObstacle()
        {
            string rock = SelectRocks();
            Vector3 spawnPosition = SpawnPosition();
            Collider[] overlaps = OverLaps(spawnPosition);
            if (ValidateOverlap(overlaps)) return;
            Obstacle obstacle = InstantiateObject(rock, spawnPosition);
            _obstacles.Add(obstacle);
        }

        private string SelectRocks()
        {
            return _rocks[Random.Range(0,_rocks.Length)];
        }

        private Obstacle InstantiateObject(string name, Vector3 spawnPosition)
        {
            GameObject obj = ServiceLocator.GetService<ResourceManager>().InstantiateObject(name, BaseObstacle.transform, pooling: true);
            obj.transform.position = spawnPosition;
            Obstacle obstacle = Utility.GetAddComponent<Obstacle>(obj);
            return obstacle;
        }

        private bool ValidateOverlap(Collider[] overlaps)
        {
            return overlaps.Any(overlap => overlap.CompareTag("Obstacle"));
        }

        private Vector3 SpawnPosition()
        {
            return new Vector3(Random.Range(-2f, 2f), Random.Range(-5, 5), 0);
        }

        private Collider[] OverLaps(Vector3 spawnPosition)
        {
            return Physics.OverlapSphere(spawnPosition, OverlapRadius, LayerMask.GetMask("Obstacles"));
        }
    }
}