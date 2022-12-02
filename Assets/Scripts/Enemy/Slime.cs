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
    private MeshRenderer rend;
    public string tagToCollideWith = "Rock";
    private ParticleSystem particlePrefab;
    public Animator anim;

    // Start is called before the first frame update
    void Start()
    { 
        particlePrefab = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Particle_SlimeDestroy.prefab", typeof(ParticleSystem)) as ParticleSystem;
        player = GameObject.Find("XR Origin").transform.
        Find("Camera Offset").transform.
        Find("Main Camera").transform.
        Find("Player").gameObject;
        rend = gameObject.GetComponentInChildren<MeshRenderer>();
        audio = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        gameObject.transform.LookAt(player.transform);
        anim.Play("mixamo_com", -1, Random.Range(0, 1f)); //randomizes anim start frame
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
            hp -= 1;
            // smoothly transition color from green to red based on percent hp
            rend.material.color = Color.Lerp(Color.green, Color.red, 1 - 0.5f * (float)hp / maxHp);
            
            // Destroy(collision.gameObject);
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
    private void OnDestroy()
    {
        audio.Play("SlimeDestroy", this.gameObject.transform.position);
    }
}
