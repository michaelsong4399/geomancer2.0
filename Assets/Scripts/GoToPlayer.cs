using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToPlayer : MonoBehaviour
{
    GameObject player;
    public float speed = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("XR Origin").transform.
        Find("Camera Offset").transform.
        Find("Main Camera").transform.
        Find("Player").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = player.transform.position - gameObject.transform.position;
        Vector3.Normalize(direction);
        direction = new Vector3(direction.x, 0f, direction.z);
        gameObject.transform.position += direction*speed*Time.deltaTime;
    }
}
