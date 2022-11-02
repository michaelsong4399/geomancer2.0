using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    public GameObject target;
    private bool activated = false;
    private bool released = false;
    public Rigidbody rb;
    Vector3 direction;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }
    public void activate()
    {
        activated = true;
        rb.useGravity = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (activated)
        {
            //want this to speed up and then slow down
            direction = target.transform.position - gameObject.transform.position;
            Vector3.Normalize(direction);
            gameObject.transform.position += direction*0.01f*Time.deltaTime;
            //gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, target.transform.position, 0.01f);
        }
    }
}
