using Pathfinding;
using UnityEngine;

namespace Assets.RunnerRun.Scripts
{
    public class NpcController : MonoBehaviour
    {
        private Character character;
        private Motivation motivation;

        // Use this for initialization
        private void Start()
        {
            character = GetComponent<Character>();
            motivation = GetComponent<Motivation>();
        }

        // Update is called once per frame
        private void Update()
        {
            if (!motivation.Updated)
            {
                return;
            }

            var sprint = motivation.Sprint;
            var path = motivation.Path;

            character.SetPath(path, sprint);

            motivation.ResetUpdated();
        }
    }
}
