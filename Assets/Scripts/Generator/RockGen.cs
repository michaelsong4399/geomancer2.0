using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;

public class RockGen : MonoBehaviour
{
    private GameObject rockPrefab;
    private GameObject fireballPrefab;
    private GameObject bombPrefab;
    private GameObject statsManager;
    private int score;
    public int numRocks = 5;
    public int numFireballs = 5;
    public int numBombs = 5;
    // Start is called before the first frame update
    void Start()
    {
        rockPrefab = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Rock.prefab", typeof(GameObject)) as GameObject;
        fireballPrefab = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Fireball.prefab", typeof(GameObject)) as GameObject;
        bombPrefab = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Bomb.prefab", typeof(GameObject)) as GameObject;
        statsManager = GameObject.Find("StatsManager");
        // Load rockprefab
        spawnRocks(numRocks-1, rockPrefab);
    }
    void spawnRocks(int num, GameObject obj)
    {
        for (int i = 0; i < num; i++)
        {
            float angle = Random.Range(0, 2 * Mathf.PI);
            float height = Random.Range(3, 4);
            float radius = Random.Range(2, 4);
            Vector3 pos = this.transform.position + new Vector3(radius * Mathf.Cos(angle), height, radius * Mathf.Sin(angle));

            GameObject newRock = Instantiate(obj, pos, Quaternion.identity);
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
        GameObject[] fireballs = GameObject.FindGameObjectsWithTag("Fireball");
        GameObject[] bombs = GameObject.FindGameObjectsWithTag("Bomb");
        score = statsManager.GetComponent<StatsRecorder>().getScore();
        if (rocks.Length < numRocks)
        {
            spawnRocks(numRocks - rocks.Length, rockPrefab);
        }
        if (score > 500 && fireballs.Length < numRocks)
        {
            spawnRocks(numFireballs - fireballs.Length, fireballPrefab);
        }
        if (score > 1000 && bombs.Length < numRocks)
        {
            spawnRocks(numBombs - bombs.Length, bombPrefab);
        }

        

        
    }
}
