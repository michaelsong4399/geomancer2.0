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
        for (int i = 0; i < numSlimes; i++)
        {
            // instantiate on unit circle with random radius
            float angle = Random.Range(0, 2 * Mathf.PI);
            float radius = Random.Range(minSpawnRadius, maxSpawnRadius);
            Vector3 pos = new Vector3(radius * Mathf.Cos(angle), 0, radius * Mathf.Sin(angle));
            Instantiate(slimePrefab, pos, Quaternion.identity);

            // Vector3 spawnPos = transform.position + Random.insideUnitSphere * spawnRadius;
            // spawnPos = new Vector3(spawnPos.x, 0, spawnPos.z);
            // Instantiate(slimePrefab, spawnPos, Quaternion.identity);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
