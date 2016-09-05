using UnityEngine;
using System.Collections;
using Pathfinding;
using Random = UnityEngine.Random;

namespace Assets.RunnerRun.Scripts
{
    public class Motivation : MonoBehaviour
    {
        public string RayCastLayer = "CityBlock";

        protected int rayCastLayer = 0;

        protected WorldGen worldGen;
        protected Seeker seeker;

        public bool Updated { get; protected set; }

        public bool Sprint { get; private set; }

        public Path Path { get; private set; }

        // Use this for initialization
        private void Start()
        {
            worldGen = (WorldGen) Object.FindObjectOfType<WorldGen>();

            seeker = GetComponent<Seeker>();
            rayCastLayer = LayerMask.NameToLayer(RayCastLayer);
        }

        // Update is called once per frame
        private void Update()
        {
            if (Updated == false && Random.value >= 0.0001)
            {
                return;
            }
            
            var pos = Vector3.zero;
            pos.x = Random.Range(0, worldGen.WorldWidth * 64);
            pos.z = Random.Range(0, worldGen.WorldHeight * 64);

            seeker.StartPath(transform.position, pos, OnPathFound);
        }

        private void OnPathFound(Path p)
        {
            if (!p.error)
            {
                Updated = true;
                Path = p;
                Sprint = Random.value < 0.025;
            }
        }

        public void ResetUpdated()
        {
            Updated = false;
        }
    }
}
