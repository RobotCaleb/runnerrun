using System;
using UnityEngine;
using System.Collections;
using Pathfinding;

namespace Assets.RunnerRun.Scripts
{
    public class Character : MonoBehaviour
    {
        public Material CharacterMaterial;

        public float Speed = 4;
        public float SprintSpeed = 10;
        
        private bool sprint = false;
        private Path path = null;
        private int currentWaypoint = 0;

        // Use this for initialization
        private void Start()
        {
            name = "Character";
            var mr = GetComponent<MeshRenderer>();
            mr.material = CharacterMaterial;
        }

        // Update is called once per frame
        private void Update()
        {
            if (path != null)
            {
                if (currentWaypoint < path.vectorPath.Count)
                {
                    var wp = path.vectorPath[currentWaypoint];
                    var dir = wp - transform.position;
                    var dist = dir.sqrMagnitude;
                    dir.Normalize();

                    // not there yet
                    if (dist > 0.02f)
                    {
                        var spd = sprint ? SprintSpeed : Speed;
                        dir *= spd * Time.deltaTime;
                        transform.Translate(dir);
                    }
                    else
                    {
                        currentWaypoint++;
                    }
                }
            }
        }

        public void SetPath(Path p, bool s)
        {
            sprint = s;
            path = p;
            currentWaypoint = 0;
        }
    }
}
