using System;
using UnityEngine;
using System.Collections;

namespace Assets.RunnerRun.Scripts
{
    public class Character : MonoBehaviour
    {
        public Material CharacterMaterial;
        public float Speed = 4;
        public float SprintSpeed = 10;
        public int RayCastLayer = 8;

        private Vector3 desiredLocation =- Vector3.zero;
        private bool fire1Pressed = false;
        private bool sprint = false;

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
                    }
                }
            }

            if (Input.GetAxisRaw("Fire1") == 0)
            {
                fire1Pressed = false;
            }

            var dir = desiredLocation - transform.position;
            if (Math.Abs(dir.sqrMagnitude) > 0.02f)
            {
                dir.Normalize();
                var spd = sprint ? SprintSpeed : Speed;
                dir *= spd * Time.deltaTime;
                transform.Translate(dir);
            }

            var camPos = cam.transform.position;
            camPos.x = transform.position.x;
            camPos.z = transform.position.z;
            cam.transform.position = camPos;
        }
    }
}
