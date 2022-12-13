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
    private GameObject b_base;

    public float minSpawnRadius = 60f;
    public float maxSpawnRadius = 90f;


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
    }

    int[] getSpawnCap(int s){
        // [zombie, silver zombie, gold zombie, ghost, silver ghost, gold ghost]
        // Point values: [100, 200, 500, 200 400 1000]
        int maxzCap = 15;
        int zCap = Mathf.Min(maxzCap,s/200);
        int bCap = Mathf.Min(maxzCap,s/500);
        // int gCap = s/200;
        float mutation = Mathf.Min(1f,s/5000f);
        int[] cap = new int[6];
        cap[2] = (int)(zCap * mutation*mutation);
        cap[1] = (int)((zCap-cap[2]) * mutation);
        cap[0] = (int)((zCap-cap[2]-cap[1]));
        cap[5] = (int)(bCap * mutation*mutation);
        cap[4] = (int)((bCap-cap[5]) * mutation);
        cap[3] = (int)((bCap-cap[5]-cap[4]));
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
            newSlime.GetComponent<Slime>().initStats(slimeSize,false);
        }
    }

    void spawnBatsWithSize(int batSize, int num, GameObject obj){
        for (int i = 0; i < num; i++)
        {
            // instantiate on unit circle with random radius
            float angle = Random.Range(0, 2 * Mathf.PI);
            float radius = Random.Range(minSpawnRadius, maxSpawnRadius);
            Vector3 pos = new Vector3(radius * Mathf.Cos(angle), 10f, radius * Mathf.Sin(angle));
            GameObject newBat = Instantiate(obj, pos, Quaternion.identity);
            newBat.GetComponent<Slime>().initStats(batSize,true);
        }
    }
    // Update is called once per frame
    void Update()
    {
        GameObject[] zbCap = GameObject.FindGameObjectsWithTag("Slime_Base");
        GameObject[] zsCap = GameObject.FindGameObjectsWithTag("Slime_Silver");
        GameObject[] zgCap = GameObject.FindGameObjectsWithTag("Slime_Gold");
        GameObject[] bbCap = GameObject.FindGameObjectsWithTag("Bat_Base");
        // GameObject[] bsCap = GameObject.FindGameObjectsWithTag("Slime_Silver");
        // GameObject[] bgCap = GameObject.FindGameObjectsWithTag("Slime_Gold");
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
