using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;

public class Slime : MonoBehaviour
{
    static int [] hpBySize = {1, 2, 10};
    static int [] scaleBySize = {70, 150, 600};
    static float [] speedBySize = {0.1f, 0.05f, 0.03f};
    public int size = 0;
    private int hp;
    private float speed;
    private GameObject player;
    private MeshRenderer rend;
    public string tagToCollideWith = "Rock";
    private ParticleSystem particlePrefab;

    // Start is called before the first frame update
    void Start()
    { 
        particlePrefab = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Particle_SlimeDestroy.prefab", typeof(ParticleSystem)) as ParticleSystem;
        player = GameObject.Find("XR Origin").transform.
        Find("Camera Offset").transform.
        Find("Main Camera").transform.
        Find("Player").gameObject;
        rend = gameObject.GetComponentInChildren<MeshRenderer>();
        gameObject.transform.LookAt(player.transform);
    }
    public void initStats(int slimeSize)
    {
        size = slimeSize;
        hp = hpBySize[size];
        speed = speedBySize[size];
        gameObject.transform.localScale *= scaleBySize[size];
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.gameObject.tag == tagToCollideWith && other.transform.gameObject.GetComponent<Rock>().thrown == true)
        {
            hp -= 1;
            // smoothly transition color from green to red based on percent hp
            rend.material.color = Color.Lerp(Color.green, Color.red, 1 - 0.5f * (float)hp / hpBySize[size]);
            
            Destroy(other.transform.gameObject);
            if (hp <= 0)
            {
                // Instantiate particle 
                ParticleSystem newParticle = Instantiate(particlePrefab, gameObject.transform.position, Quaternion.identity);
                newParticle.GetComponent<ParticleSystem>().Play();
                Destroy(gameObject, 0.1f);
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        Vector3 direction = player.transform.position - gameObject.transform.position;
        direction = new Vector3(direction.x, 0f, direction.z);
        Vector3.Normalize(direction);
        gameObject.transform.position += direction*speed*Time.deltaTime;
    }
}
