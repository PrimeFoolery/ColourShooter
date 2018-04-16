﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RedEnemyHealth : MonoBehaviour {

	public GameObject splat;
	public GameObject EnemyEmpty;
	public int health;
	public float deathTimer = 1;


	//Private variables
	private int currentHealth;
    private EnemyManager enemyManagerScript;

    void Start () {
		//Setting the current health to be the health variable
		//so that when we start the game, the enemy has full HP
		currentHealth = health;
        enemyManagerScript = GameObject.FindGameObjectWithTag("GameManager").GetComponent<EnemyManager>();
    }

	void Update () {
		//If the enemy reaches 0 HP, destroy the enemy
		if (currentHealth <= 0) {
			gameObject.GetComponent<ParticleSystem> ().Play();
			deathTimer -= Time.deltaTime;
			if (deathTimer >= 0) {
				Instantiate (splat, EnemyEmpty.gameObject.transform.position, EnemyEmpty.gameObject.transform.rotation);
			    enemyManagerScript.enemyList.Remove(gameObject);
                Destroy (gameObject);
			}
		}
	}

	//Used to call this void in the bullet scripts
	//since currentHealth is a private variable
	public void EnemyDamaged (int damage) {
		gameObject.GetComponent<ParticleSystem> ().Play();
	    if (gameObject.GetComponent<StandardEnemyBehaviour>().isAggroPlayer == false)
	    {
	        gameObject.GetComponent<StandardEnemyBehaviour>().AggroToggle();
	    }
        currentHealth -= damage;

	}
}
