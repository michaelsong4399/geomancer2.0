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
    public bool isDead = false;
    Color32 blood;

    // Start is called before the first frame update
    void Start()
    { 
        health = maxHealth;
        blood = new Color32 (90,0,0,255);
    }

    // Update is called once per frame
    void Update()
    {
        health += 0.1f*Time.deltaTime;
        Color32 c = Color.Lerp(blood, Color.clear, health/maxHealth);
        ui.updateOverlay(c);
    }

    public void takeDamage(float damage)
    {
        health -= damage;
        Color32 c = Color.Lerp(blood, Color.clear, health/maxHealth);
        ui.updateOverlay(c);
        if (health <= 0f)
        {
            ui.gameOver();
            isDead = true;
        }
    }
}