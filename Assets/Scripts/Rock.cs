using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Rock : MonoBehaviour
{
    public GameObject target;
    private bool selected = false;
    private bool activated = false;
    private bool justActivated = false;
    private bool thrown = false;
    //private bool released = false;
    public Rigidbody rb;
    Vector3 direction;
    float speed;
    float initDist;
    Vector3 prevPos;
    TriggerReader input;

    // Start is called before the first frame update
    void Start()
    {
        //need to add code to get target object so it does not need to be set in Inspector
        rb = gameObject.GetComponent<Rigidbody>();
        speed = 1f;
        input = GameObject.Find("GetInput").GetComponent<TriggerReader>();
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
        if (!activated)
        {
            activated = true;
            rb.useGravity = false;
            rb.velocity = Vector3.zero;
            justActivated = true;
            initDist = Vector3.Distance(gameObject.transform.position, target.transform.position);
        }
    }
    public void deactivate()
    {
        activated = false;
        justActivated = false;
        if (Vector3.Distance(prevPos, gameObject.transform.position) <= 0.001f)
        {
            rb.useGravity = true;
        }
        else
        {
            thrown = true;
            rb.useGravity = true;
            rb.velocity = direction * 70f;
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
        else if (activated)
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
        else if (activated)
        {
            speed = 30f;
            direction = target.transform.position - gameObject.transform.position;
            Vector3.Normalize(direction);
            gameObject.transform.position += direction * speed * Time.deltaTime;
            //gameObject.transform.position = target.transform.position;
        }
    }
}
