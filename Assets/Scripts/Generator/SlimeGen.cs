using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;

public class SlimeGen : MonoBehaviour
{
    private GameObject slimePrefab;
    public int numSlimes = 10;
    public float minSpawnRadius = 60f;
    public float maxSpawnRadius = 90f;


    // Start is called before the first frame update
    void Start()
    {
        slimePrefab = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Slime.prefab", typeof(GameObject)) as GameObject;
        // spawnSlimes(5, 3, 1);
    }
    // void spawnSlimes(int numSmall, int numMedium, int numLarge)
    // {
    //     spawnSlimesWithSize(1, numSmall);
    //     spawnSlimesWithSize(2, numMedium);
    //     spawnSlimesWithSize(5, numLarge);
    // }
    // void spawnSlimesWithSize(int slimeSize, int num)
    // {
    //     for (int i = 0; i < num; i++)
    //     {
    //         // instantiate on unit circle with random radius
    //         float angle = Random.Range(0, 2 * Mathf.PI);
    //         float radius = Random.Range(minSpawnRadius, maxSpawnRadius);
    //         Vector3 pos = new Vector3(radius * Mathf.Cos(angle), 0.5f, radius * Mathf.Sin(angle));
    //         GameObject newSlime = Instantiate(slimePrefab, pos, Quaternion.identity);
    //         newSlime.GetComponent<Slime>().initStats(slimeSize);
    //     }
    // }
    // Update is called once per frame
    void Update()
    {
        
    }
}
