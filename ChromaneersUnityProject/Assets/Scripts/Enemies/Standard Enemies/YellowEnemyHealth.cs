﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class YellowEnemyHealth : MonoBehaviour {

	public GameObject splat;
	public GameObject EnemyEmpty;
    public GameObject TopEmpty;
    public int health;
	public float deathTimer = 1;

    public GameObject coin;

    public GameObject deathSplatter;

    //Private variables
    private int currentHealth;
    private EnemyManager enemyManagerScript;
    private GameObject mainCamera;
    private GameObject thisEnemiesSpawnPoint;

    void Start () {
        //Setting the current health to be the health variable
        //so that when we start the game, the enemy has full HP
        currentHealth = health;
        enemyManagerScript = GameObject.FindGameObjectWithTag("GameManager").GetComponent<EnemyManager>();
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        thisEnemiesSpawnPoint = gameObject.GetComponent<StandardEnemyBehaviour>().thisEnemiesSpawnPoint;
    }

    void Update () {
        //If the enemy reaches 0 HP, destroy the enemy
        if (currentHealth <= 0) {
			gameObject.GetComponent<ParticleSystem> ().Play();
            
            deathTimer -= Time.deltaTime;
			if (deathTimer >= 0) {
                Instantiate(deathSplatter, TopEmpty.gameObject.transform.position, TopEmpty.transform.rotation);
                Instantiate (splat, EnemyEmpty.gameObject.transform.position, EnemyEmpty.gameObject.transform.rotation);
			    Instantiate(coin, transform.position, Quaternion.identity);
                mainCamera.GetComponent<CameraScript>().SmallScreenShake();
			    if (thisEnemiesSpawnPoint.GetComponent<InfiniteSpawnPoint>() != null)
			    {
			        thisEnemiesSpawnPoint.GetComponent<InfiniteSpawnPoint>().ThisSpawnpointsEnemyList.Remove(gameObject);
			    }
			    else if (thisEnemiesSpawnPoint.GetComponent<newSpawner>() != null)
			    {
			        thisEnemiesSpawnPoint.GetComponent<newSpawner>().ThisSpawnpointsEnemyList.Remove(gameObject);
			    }
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
