using UnityEngine;
using System.Collections;

namespace Assets.RunnerRun.Scripts
{
    public class FollowCamera : MonoBehaviour
    {
        protected GameObject followTarget;

        protected GameObject cam;

        // Use this for initialization
        private void Start()
        {
            cam = gameObject;
        }

        // Update is called once per frame
        private void Update()
        {
            var camPos = cam.transform.position;
            camPos.x = followTarget.transform.position.x;
            camPos.z = followTarget.transform.position.z;
            cam.transform.position = camPos;
        }

        public void SetFollowTarget(GameObject go)
        {
            followTarget = go;
        }
    }
}
