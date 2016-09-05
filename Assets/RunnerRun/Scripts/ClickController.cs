using System;
using UnityEngine;
using System.Collections;
using Pathfinding;

namespace Assets.RunnerRun.Scripts
{
    public class ClickController : MonoBehaviour
    {
        public string RayCastLayer = "CityBlock";

        private int rayCastLayer = 0;
        private bool fire1Pressed = false;
        private bool sprint = false;
        private Path path = null;
        private Seeker seeker;
        private Character character;

        // Use this for initialization
        private void Start()
        {
            seeker = GetComponent<Seeker>();
            character = GetComponent<Character>();
            rayCastLayer = LayerMask.NameToLayer(RayCastLayer);
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
                    if (Physics.Raycast(ray, out hit, 1024, 1 << rayCastLayer))
                    {
                        seeker.StartPath(transform.position, hit.point, OnPathFound);
                    }
                }
            }
            else
            {
                fire1Pressed = false;
            }
        }

        public void OnPathFound(Path p)
        {
            if (!p.error)
            {
                character.SetPath(p, sprint);
            }
        }
    }
}
