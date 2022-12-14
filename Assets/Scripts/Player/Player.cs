using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;



public class Player : MonoBehaviour
{
    public UIUpdater ui;
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
        health += 0.1f*Time.deltaTime;
        Color32 c = Color.Lerp(Color.red, Color.clear, health/maxHealth);
        ui.updateOverlay(c);
    }

    public void takeDamage(float damage)
    {
        health -= damage;
        Color32 c = Color.Lerp(Color.red, Color.clear, health/maxHealth);
        ui.updateOverlay(c);
    }
}