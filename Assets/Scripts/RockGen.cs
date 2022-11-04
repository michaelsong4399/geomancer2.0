using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;

public class RockGen : MonoBehaviour
{
    private GameObject rockPrefab;
    public int numRocks = 10;
    // Start is called before the first frame update
    void Start()
    {
        rockPrefab = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Rock.prefab", typeof(GameObject)) as GameObject;
        // Load rockprefab
        spawnRocks(numRocks-1);
    }
    void spawnRocks(int num)
    {
        for (int i = 0; i < num; i++)
        {
            float angle = Random.Range(0, 2 * Mathf.PI);
            float height = Random.Range(5, 10);
            float radius = Random.Range(0, 2);
            Vector3 pos = new Vector3(radius * Mathf.Cos(angle), height, radius * Mathf.Sin(angle));
            GameObject newRock = Instantiate(rockPrefab, pos, Quaternion.identity);
            // Randomize rock size
            float rockSize = Random.Range(0.8f, 1.2f);
            newRock.GetComponent<Rock>().initStats(rockSize);
        }   
    }

    // Update is called once per frame
    void Update()
    {
        // find the amount of rocks in the scene
        // if less than num, spawn more
        GameObject[] rocks = GameObject.FindGameObjectsWithTag("Rock");
        if (rocks.Length < numRocks)
        {
            spawnRocks(numRocks - rocks.Length);
        }

        
    }
}
