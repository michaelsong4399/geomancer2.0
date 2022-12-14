using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;

public class EnemyGen : MonoBehaviour
{
    private GameObject statsManager;
    private int score;

    private GameObject z_base;
    private GameObject z_silver;
    private GameObject z_gold;
    private GameObject ghostPrefab;
    private bool tutorialCompleted = false;
    private GameObject b_base;

    public float minSpawnRadius = 60f;
    public float maxSpawnRadius = 90f;

    public GameObject camera;

    // Start is called before the first frame update
    void Start()
    {
        z_base = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Slime_Base.prefab", typeof(GameObject)) as GameObject;
        z_silver = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Slime_Silver.prefab", typeof(GameObject)) as GameObject;
        z_gold = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Slime_Gold.prefab", typeof(GameObject)) as GameObject;
        b_base = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Bat_Base.prefab", typeof(GameObject)) as GameObject;
        // ghostPrefab = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Slime.prefab", typeof(GameObject)) as GameObject;
        statsManager = GameObject.Find("StatsManager");
        // score = statsManager.GetComponent<StatsRecorder>().getScore() + 500f;
        // score = 1000;
        // Debug.Log(score);
        // spawnSlimes(5, 3, 1);
        Vector3 pos = maxSpawnRadius*camera.transform.forward*(-1f);
        GameObject newSlime = Instantiate(z_base, pos, Quaternion.identity);
        //newSlime.GetComponent<Slime>().initStats();
    }

    int[] getSpawnCap(int s){
        // [zombie, silver zombie, gold zombie, ghost, silver ghost, gold ghost]
        // Point values: [100, 200, 500, 200 400 1000]
        int maxzCap = 10;
        int maxbCap = 3;
        int zCap = Mathf.Min(maxzCap,s/200);
        int bCap = Mathf.Min(maxbCap,s/500);
        // int gCap = s/200;
        float mutation = Mathf.Min(1f,s/2000f);
        int[] cap = new int[6];
        cap[2] = (int)(zCap * mutation*mutation);
        cap[1] = (int)((zCap-cap[2]) * mutation);
        cap[0] = (int)((zCap-cap[2]-cap[1]));
        cap[3] = (int)((bCap));
        //Debug.Log((zCap,mutation,cap[0]));
        return cap;
    }

    void spawnSlimesWithSize(int slimeSize, int num, GameObject obj)
    {
        for (int i = 0; i < num; i++)
        {
            // instantiate on unit circle with random radius
            float angle = Random.Range(0, 2 * Mathf.PI);
            float radius = Random.Range(minSpawnRadius, maxSpawnRadius);
            Vector3 pos = new Vector3(radius * Mathf.Cos(angle), 0.5f, radius * Mathf.Sin(angle));
            GameObject newSlime = Instantiate(obj, pos, Quaternion.identity);
            
        }
    }

    void spawnBatsWithSize(int batSize, int num, GameObject obj){
        for (int i = 0; i < num; i++)
        {
            // instantiate on unit circle with random radius
            float angle = Random.Range(0, 2 * Mathf.PI);
            float radius = Random.Range(minSpawnRadius, maxSpawnRadius);
            // Generate at random height between 5 and 10
            float height = Random.Range(6f, 10f);
            Vector3 pos = new Vector3(radius * Mathf.Cos(angle), height, radius * Mathf.Sin(angle));
            GameObject newBat = Instantiate(obj, pos, Quaternion.identity);
            //newBat.GetComponent<Slime>().initStats(batSize,true);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (!tutorialCompleted)
        {
            if (GameObject.FindGameObjectsWithTag("Slime_Base").Length == 0)
            {
                tutorialCompleted = true;
            }
        }
        else
        {
            GameObject[] zbCap = GameObject.FindGameObjectsWithTag("Slime_Base");
            GameObject[] zsCap = GameObject.FindGameObjectsWithTag("Slime_Silver");
            GameObject[] zgCap = GameObject.FindGameObjectsWithTag("Slime_Gold");
            GameObject[] bbCap = GameObject.FindGameObjectsWithTag("Bat_Base");
            // GameObject[] ghosts = GameObject.FindGameObjectsWithTag("Rock");
            score = statsManager.GetComponent<StatsRecorder>().getScore() + 500;
            int[] cap = getSpawnCap(score);
            // Debug.Log(cap[0]);
            if (zbCap.Length < cap[0])
            {
                spawnSlimesWithSize(1, cap[0] - zbCap.Length, z_base);
            }
            if (zsCap.Length < cap[1])
            {
                spawnSlimesWithSize(1, cap[1] - zsCap.Length, z_silver);
            }
            if (zgCap.Length < cap[2])
            {
                spawnSlimesWithSize(1, cap[2] - zgCap.Length, z_gold);
            }
            if (bbCap.Length < cap[3])
            {
                spawnBatsWithSize(1, cap[3] - bbCap.Length, b_base);
            }
        }
    }
}
