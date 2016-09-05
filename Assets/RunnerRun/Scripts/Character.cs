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
        public int RayCastLayer = 8;

        public AstarPath Pathfinder;

        private Vector3 desiredLocation =- Vector3.zero;
        private bool fire1Pressed = false;
        private bool sprint = false;
        private Path path = null;
        private Seeker seeker;
        private int currentWaypoint = 0;

        // Use this for initialization
        private void Start()
        {
            name = "Character";
            var mr = GetComponent<MeshRenderer>();
            mr.material = CharacterMaterial;

            seeker = GetComponent<Seeker>();
        }

        // Update is called once per frame
        private void Update()
        {
            var cam = Camera.main;

            if (Input.GetAxisRaw("Fire1") != 0)
            {
                sprint = Input.GetAxisRaw("Sprint") > 0;
                if (fire1Pressed == false)
                {
                    fire1Pressed = true;

                    var mp = Input.mousePosition;
                    var ray = cam.ScreenPointToRay(mp);
                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit, 1024, 1 << RayCastLayer))
                    {
                        desiredLocation = hit.point;
                        seeker.StartPath(transform.position, desiredLocation, OnPathFound);
                    }
                }
            }
            else
            {
                fire1Pressed = false;
            }

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

            var camPos = cam.transform.position;
            camPos.x = transform.position.x;
            camPos.z = transform.position.z;
            cam.transform.position = camPos;
        }

        public void OnPathFound(Path p)
        {
            if (!p.error)
            {
                path = p;
                currentWaypoint = 0;
            }
        }
    }
}
