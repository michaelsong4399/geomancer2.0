using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using System.IO;
using UnityEditor;

public class Bomb : MonoBehaviour
{
    public GameObject target;
    //private ParticleSystem explosionParticle;
    private AudioManager audio;
    private bool selected = false;
    public static GameObject activated = null;
    private bool justActivated = false;
    public bool thrown = false;
    public int explosionRadius;
    //private bool released = false;
    public Rigidbody rb;
    Vector3 direction;
    float speed;
    float initDist;
    Vector3 prevPos;
    TriggerReader input;
    public AudioSource RockFall;
    private StatsRecorder stats;

    // Start is called before the first frame update
    void Start()
    {
        //need to add code to get target object so it does not need to be set in Inspector
        rb = gameObject.GetComponent<Rigidbody>();
        gameObject.GetComponent<XRSimpleInteractable>().interactionManager = GameObject.Find("XR Interaction Manager").GetComponent<XRInteractionManager>();
        target = GameObject.Find("XR Origin").transform.Find("Camera Offset").transform.Find("RightHand Controller").transform.Find("Target").gameObject;
        audio = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        speed = 1f;
        input = GameObject.Find("GetInput").GetComponent<TriggerReader>();
        stats = GameObject.Find("StatsManager").GetComponent<StatsRecorder>();
        //explosionParticle = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Particle_Explosion.prefab", typeof(ParticleSystem)) as ParticleSystem;
    }
    public void initStats(float rockSize)
    {
        gameObject.transform.localScale *= rockSize;
    }
    public void select()
    {
        selected = true;
    }
    public void deselect()
    {
        selected = false;
    }
    public void activate()
    {
        if (activated == null && !thrown)
        {
            activated = this.gameObject;
            rb.useGravity = false;
            rb.velocity = Vector3.zero;
            justActivated = true;
            initDist = Vector3.Distance(gameObject.transform.position, target.transform.position);
        }
    }
    public void deactivate()
    {
        activated = null;
        if (Vector3.Distance(prevPos, gameObject.transform.position) <= 0.01f || justActivated)
        {
            rb.useGravity = true;
            justActivated = false;
        }
        else
        {
            thrown = true;
            rb.useGravity = true;
            rb.velocity = direction * 70f;
            stats.rockThrown();
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (input.lastValue > 0.01f)
        {
            if (selected)
                activate();
        }
        else if (activated == this.gameObject)
        {
            deactivate();
        }
        prevPos = gameObject.transform.position;
        if (justActivated)
        {
            //use derivative of logistic function for speed such that it speeds up and then slows down
            float dist = Vector3.Distance(gameObject.transform.position, target.transform.position);
            speed = 20*Mathf.Exp(2/initDist*dist - initDist/2) / Mathf.Pow((1 + Mathf.Exp(2/initDist*dist - initDist/2)), 2) + 10f;

            direction = target.transform.position - gameObject.transform.position;
            Vector3.Normalize(direction);
            if (Vector3.Distance(gameObject.transform.position, target.transform.position) < speed * Time.deltaTime)
            {
                justActivated = false;
                gameObject.transform.position = target.transform.position;
            }
            else
            {
                gameObject.transform.position += direction * speed * Time.deltaTime;
            }
        }
        else if (activated == this.gameObject)
        {
            speed = 30f;
            direction = target.transform.position - gameObject.transform.position;
            Vector3.Normalize(direction);
            gameObject.transform.position += direction * speed * Time.deltaTime;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Floor")
        {
            if (thrown)
            {
                thrown = false;
            }
            else
            {
                audio.Play("RockFall", this.gameObject.transform.position);
            }
        }
    }
    private void OnDestroy()
    {
        audio.Play("RockDestroy", this.gameObject.transform.position);
        //explosion.Play();
        //play particle
        Collider[] hitColliders = Physics.OverlapSphere(gameObject.transform.position, explosionRadius, Physics.AllLayers);
        foreach (var hitCollider in hitColliders)
        {
            //Debug.Log(hitCollider.gameObject.name);
            if (hitCollider.gameObject.tag == "Slime_Base" || hitCollider.gameObject.tag == "Slime_Silver" || hitCollider.gameObject.tag == "Slime_Gold")
            {
                hitCollider.gameObject.GetComponent<Slime>().applyDamage(1f + 9f*(explosionRadius - Vector3.Distance(gameObject.transform.position, hitCollider.gameObject.transform.position)));
            }
        }
    }
}
