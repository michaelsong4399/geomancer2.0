using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;

public class Slime : MonoBehaviour
{
    private AudioManager audio;
    public int size = 0;
    private int hp;
    private int maxHp;
    private float speed;
    private GameObject player;
    public SkinnedMeshRenderer rend;
    public string tagToCollideWith = "Rock";
    public ParticleSystem fire;
    private ParticleSystem particlePrefab;
    public Animator anim;
    private Color initColor;
    private StatsRecorder stats;
    public int pointValue;

    // Start is called before the first frame update
    void Start()
    { 
        particlePrefab = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Particle_SlimeDestroy.prefab", typeof(ParticleSystem)) as ParticleSystem;
        player = GameObject.Find("XR Origin").transform.
        Find("Camera Offset").transform.
        Find("Main Camera").transform.
        Find("Player").gameObject;
        //rend = gameObject.GetComponentInChildren<MeshRenderer>();
        audio = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        gameObject.transform.LookAt(player.transform);
        anim.Play("mixamo_com", -1, Random.Range(0, 1f)); //randomizes anim start frame
        initColor = rend.material.color;    
        stats = GameObject.Find("StatsManager").GetComponent<StatsRecorder>();
        fire = Instantiate(fire, gameObject.transform.position, Quaternion.identity);
        fire.transform.Rotate(-90.0f, 0.0f, 0.0f);
        fire.GetComponent <ParticleSystem>().Stop();
    }
    public void initStats(int slimeSize)
    {
        size = slimeSize;
        hp = size;
        maxHp = size;
        speed = 0.05f / (float)size;
        gameObject.transform.localScale *= 150*size;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == tagToCollideWith && collision.gameObject.GetComponent<Rock>().thrown == true)
        {
            print("rock"); 
            hp -= 1;
            // smoothly transition color from green to red based on percent hp
            rend.material.color = Color.Lerp(initColor, Color.red, 1 - 0.5f * (float)hp / maxHp);
            stats.hit();
            // Destroy(collision.gameObject);
            if (hp <= 0)
            {
                // Instantiate particle 
                ParticleSystem newParticle = Instantiate(particlePrefab, gameObject.transform.position, Quaternion.identity);
                newParticle.GetComponent<ParticleSystem>().Play();
                stats.increaseScore(pointValue);
                Destroy(gameObject, 0.1f);
            }
        }

        if (collision.gameObject.tag == "fire")
        {
            fire.GetComponent<ParticleSystem>().Play(); 
        }
    }
    // Update is called once per frame
    void Update()
    {
        Vector3 direction = player.transform.position - gameObject.transform.position;
        direction = new Vector3(direction.x, 0f, direction.z);
        Vector3.Normalize(direction);
        gameObject.transform.position += direction*speed*Time.deltaTime;
        fire.transform.position = gameObject.transform.position; 

    }
    private void OnDestroy()
    {
        audio.Play("SlimeDestroy", this.gameObject.transform.position);
    }
}
