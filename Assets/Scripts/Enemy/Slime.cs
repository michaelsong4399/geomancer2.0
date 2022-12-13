using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;

public class Slime : MonoBehaviour
{
    private AudioManager audio;
    //public int size = 0;
    private float hp;
    private float maxHp;
    private float speed;
    private GameObject player;
    public SkinnedMeshRenderer rend;
    public string rockTag = "Rock";
    private string playerBodyTag = "Player_Body";
    public ParticleSystem fire;
    private ParticleSystem particlePrefab;
    public Animator anim;
    private Color initColor;
    private StatsRecorder stats;
    public int pointValue;
    public int health;
    private bool reachedPlayer;
    private float attackTimer;
    private float ATTACK_DELAY = 2.633f;
    public bool onFire = false;
    private bool onDestroy = false;
    private bool attacked = false;
    private float FIRE_SPEED_MULTIPLIER = 0.5f;
    private float baseSpeed = 0.05f;

    // Start is called before the first frame update
    void Start()
    { 
        initStats();
        particlePrefab = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Particle_SlimeDestroy.prefab", typeof(ParticleSystem)) as ParticleSystem;
        player = GameObject.Find("XR Origin").transform.
        Find("Camera Offset").transform.
        Find("Main Camera").transform.
        Find("Player").gameObject;
        //rend = gameObject.GetComponentInChildren<MeshRenderer>();
        audio = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        gameObject.transform.LookAt(player.transform);
        //audio.Play("ZombieLive", this.gameObject.transform.position);
        anim.Play("zwalk", -1, Random.Range(0, 1f)); //randomizes anim start frame
        initColor = rend.material.color;    
        stats = GameObject.Find("StatsManager").GetComponent<StatsRecorder>();
        if (stats == null)
            Debug.Log("AAAA");
        fire = Instantiate(fire, gameObject.transform.position, Quaternion.identity);
        fire.transform.Rotate(-90.0f, 0.0f, 0.0f);
        fire.GetComponent <ParticleSystem>().Stop();
        reachedPlayer = false;
    }
    public void initStats()
    {
        //size = slimeSize;
        hp = health;
        maxHp = health;
        speed = baseSpeed;
        gameObject.transform.localScale *= 150f;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == rockTag && collision.gameObject.GetComponent<Rock>().thrown == true)
        {
            if (reachedPlayer)
            {
                attackTimer = ATTACK_DELAY;
            }
            //print("rock"); 
            applyDamage(1f);
            // smoothly transition color from green to red based on percent hp
            //rend.material.color = Color.Lerp(initColor, Color.red, (float)hp / maxHp);
            stats.hit();
        }

        if (collision.gameObject.tag == "Fireball" && collision.gameObject.GetComponent<Rock>().thrown == true)
        {
            if (reachedPlayer)
            {
                attackTimer = ATTACK_DELAY;
            }
            applyDamage(0.5f);
            // smoothly transition color from green to red based on percent hp
            //rend.material.color = Color.Lerp(initColor, Color.red, (float)hp / maxHp);
            stats.hit();
            fire.GetComponent<ParticleSystem>().Play();
            onFire = true;
        }

        
    }
    // private void OnTriggerEnter(Collider other) {
    //     if (other.gameObject.tag == playerBodyTag)
    //     {
    //         if (!reachedPlayer)
    //         {
    //             reachedPlayer = true;
    //             attackTimer = ATTACK_DELAY;
    //         }
    //     }
    // }
    // Update is called once per frame
    void Update()
    {
        if (onDestroy){
            return;
        }
        if (hp <= 0)
            {
                // Instantiate particle 
                ParticleSystem newParticle = Instantiate(particlePrefab, gameObject.transform.position, Quaternion.identity);
                newParticle.GetComponent<ParticleSystem>().Play();
                stats.increaseScore(pointValue);
                onDestroy = true;
                Destroy(gameObject,0.1f);
                fire.GetComponent<ParticleSystem>().Stop(); 
                Destroy(fire, 5f); 
            }


        if(onFire){
            applyDamage(0.5f * Time.deltaTime);
            speed = baseSpeed*FIRE_SPEED_MULTIPLIER;
        }else{
            speed = baseSpeed;
        }



        if (!reachedPlayer)
        {
            Vector3 direction = player.transform.position - gameObject.transform.position;
            direction = new Vector3(direction.x, 0f, direction.z);
            Vector3.Normalize(direction);
            gameObject.transform.position += direction*speed*Time.deltaTime;
            fire.transform.position = gameObject.transform.position;

            // Check if within range of player
            if (Vector3.Distance(gameObject.transform.position, player.transform.position) < 10f)
            {
                reachedPlayer = true;
                // Play attack animation
                anim.Play("zattack", -1, 0f);
                attackTimer = ATTACK_DELAY;
            }
        }
        else if (attackTimer > 0f)
        {
            attackTimer -= Time.deltaTime;
            if (!attacked && attackTimer < 1.5f){
                attacked = true;
                // audio.Play("ZombieLive", this.gameObject.transform.position);
                audio.Play("SlimeDestroy", this.gameObject.transform.position);
                Debug.Log(attacked);
            }
        }
        else
        {
            anim.Play("zattack", -1, 0f);
            attackTimer = ATTACK_DELAY;
            attacked = false;
        }
    }
    public void applyDamage (float damage)
    {
        hp -= damage;
        rend.material.color = Color.Lerp(initColor, Color.red, 1f - 0.5f * (float)hp / maxHp);
        if (hp <= 0)
        {
            // Instantiate particle 
            ParticleSystem newParticle = Instantiate(particlePrefab, gameObject.transform.position, Quaternion.identity);
            newParticle.GetComponent<ParticleSystem>().Play();
            stats.increaseScore(pointValue);
            onDestroy = true;
            Destroy(gameObject, 0.1f);
            fire.GetComponent<ParticleSystem>().Stop();
            Destroy(fire, 5f);
        }
    }
    private void OnDestroy()
    {
        audio.Play("SlimeDestroy", this.gameObject.transform.position);
    }
}
