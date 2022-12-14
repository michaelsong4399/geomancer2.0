using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;



public class Player : MonoBehaviour
{
    public float maxHealth;
    private float health;

    // Start is called before the first frame update
    void Start()
    { 
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void takeDamage(float damage)
    {
        health -= damage;
    }
}