using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour
{
    static int [] hpBySize = {2, 2, 3};
    static int [] scaleBySize = {50, 150, 600};
    static float [] speedBySize = {0.1f, 0.05f, 0.03f};
    public int size = 0;
    private int hp;
    private float speed;
    private GameObject player;
    private MeshRenderer rend;
    public string tagToCollideWith = "Rock";

    // Start is called before the first frame update
    void Start()
    { 
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
        if (other.transform.gameObject.tag == tagToCollideWith) //&& other.transform.gameObject.GetComponent<Rigidbody>().velocity.magnitude > 0.01f)
        {
            hp -= 1;
            rend.material.SetColor("_Color", Color.red); //set color not working
            Destroy(other.transform.gameObject);
            if (hp <= 0)
            {
                Destroy(gameObject);
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