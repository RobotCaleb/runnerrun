using System;
using UnityEngine;

namespace Assets.RunnerRun.Scripts
{
    public class WorldGen : MonoBehaviour
    {

        public int WorldWidth = 8;
        public int WorldHeight = 8;

        public GameObject CharacterPrefab;
        public GameObject CityBlockPrefab;

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
                    new Vector3(x * width, 0, z * height),
                    Quaternion.identity);
                block.name = "block-" + x + ":" + z;
                block.transform.parent = blocks.transform;
            }

            var character = (GameObject) Instantiate(CharacterPrefab, Vector3.zero, Quaternion.identity);
            character.transform.parent = transform;
            character.name = "Character";
        }
    
        // Update is called once per frame
        void Update () {
    
        }
    }
}
