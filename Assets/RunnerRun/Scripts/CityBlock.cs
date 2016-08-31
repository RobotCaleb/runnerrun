using System.Collections.Generic;
using UnityEngine;

namespace Assets.RunnerRun.Scripts
{
    public class CityBlock : MonoBehaviour
    {
        public List<GameObject> BuildingsPrefabs;
        public GameObject Building1;
        public GameObject Building2;
        public GameObject Building3;
        public GameObject Building4;

        // Use this for initialization
        void Start ()
        {
            var bld1 = (GameObject)Instantiate(
                    BuildingsPrefabs[Random.Range(0, BuildingsPrefabs.Count)],
                    Building1.transform.position, Quaternion.identity);
            bld1.transform.parent = transform;

            var bld2 = (GameObject)Instantiate(
                    BuildingsPrefabs[Random.Range(0, BuildingsPrefabs.Count)],
                    Building2.transform.position, Quaternion.identity);
            bld2.transform.parent = transform;

            var bld3 = (GameObject)Instantiate(
                    BuildingsPrefabs[Random.Range(0, BuildingsPrefabs.Count)],
                    Building3.transform.position, Quaternion.identity);
            bld3.transform.parent = transform;

            var bld4 = (GameObject)Instantiate(
                    BuildingsPrefabs[Random.Range(0, BuildingsPrefabs.Count)],
                    Building4.transform.position, Quaternion.identity);
            bld4.transform.parent = transform;
        }
    
        // Update is called once per frame
        void Update () {
    
        }
    }
}
