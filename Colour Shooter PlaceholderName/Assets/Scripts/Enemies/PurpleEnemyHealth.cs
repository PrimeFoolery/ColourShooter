﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurpleEnemyHealth : MonoBehaviour {

    public int health;

    //Private variables
    private int currentHealth;

    void Start () {
        //Setting the current health to be the health variable
        //so that when we start the game, the enemy has full HP
        currentHealth = health;
    }

    void Update () {
        //If the enemy reaches 0 HP, destroy the enemy
        if (currentHealth <= 0) {
            Destroy(gameObject);
        }
    }

    //Used to call this void in the bullet scripts
    //since currentHealth is a private variable
    public void EnemyDamaged (int damage) {
        currentHealth -= damage;
    }
}
