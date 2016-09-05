using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.RunnerRun.Scripts
{
    public class WorldGen : MonoBehaviour
    {

        public int WorldWidth = 8;
        public int WorldHeight = 8;

        public GameObject CharacterPrefab;
        public GameObject NpcPrefab;
        public GameObject CityBlockPrefab;

        public AstarPath Pathfinder;

        protected bool pathGenerated = false;

        // Use this for initialization
        void Start ()
        {
            var blocks = (GameObject) Instantiate(new GameObject());
            blocks.name = "blocks";
            blocks.transform.parent = transform;
            for (var i = 0; i < WorldWidth * WorldHeight; i++)
            {
                var mr = CityBlockPrefab.GetComponent<MeshRenderer>();
                var width = mr.bounds.extents.x * 2;
                var height = mr.bounds.extents.z * 2;
                var x = (int)Math.Floor((double)i / (double)WorldWidth);
                var z = i % WorldHeight;
                var block = (GameObject)Instantiate(CityBlockPrefab,
                    new Vector3(32 + x * width, 0, 32 + z * height),
                    Quaternion.identity);
                block.name = "block-" + x + ":" + z;
                block.transform.parent = blocks.transform;
            }

            var character = (GameObject) Instantiate(CharacterPrefab, Vector3.zero, Quaternion.identity);
            character.transform.parent = transform;
            character.name = "Character";

            var npcGO = new GameObject();
            npcGO.name = "NPCs";
            npcGO.transform.parent = transform;

            for (var i = 0; i < 250; ++i)
            {
                var pos = Vector3.zero;
                pos.x = Random.Range(0, WorldWidth * 64);
                pos.z = Random.Range(0, WorldHeight * 64);
                var npc = (GameObject) Instantiate(NpcPrefab, pos, Quaternion.identity);
                npc.transform.parent = npcGO.transform;
                npc.name = "NPC-" + i;
            }

            var cam = Camera.main.GetComponent<FollowCamera>();
            cam.SetFollowTarget(character);
        }
    
        // Update is called once per frame
        void Update ()
        {
            if (!pathGenerated)
            {
                Pathfinder.Scan();
                pathGenerated = true;
            }
        }
    }
}
