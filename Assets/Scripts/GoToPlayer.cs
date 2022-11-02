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
        gameObject.transform.LookAt(player.transform);
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
