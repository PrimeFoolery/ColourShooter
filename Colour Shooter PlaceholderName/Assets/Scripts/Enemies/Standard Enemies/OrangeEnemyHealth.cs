﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrangeEnemyHealth : MonoBehaviour {

	//Private variables
	public int redHealth=3;
	public int yellowHealth=3;
	private float recoveryTimer = 5f;
	public GameObject sphere;

	public Material redJellyMaterial;
	public Material yellowJellyMaterial;
	public Material orangeJellyMaterial;

    private EnemyManager enemyManagerScript;

    void Start () {
        //Setting the current health to be the health variable
        //so that when we start the game, the enemy has full HP
        enemyManagerScript = GameObject.FindGameObjectWithTag("GameManager").GetComponent<EnemyManager>();
    }

	void Update () {
		//If the enemy reaches 0 HP, destroy the enemy
		if(redHealth<=0&&yellowHealth>0){
			GetComponent<Renderer> ().material = yellowJellyMaterial;
			sphere.GetComponent<Renderer> ().material = yellowJellyMaterial;
		}
		if(redHealth<0){
			redHealth=0;
		}
		if(yellowHealth<=0&&redHealth>0){
			GetComponent<Renderer> ().material = redJellyMaterial;
			sphere.GetComponent<Renderer> ().material = redJellyMaterial;
		}
		if(yellowHealth<0){
			yellowHealth=0;
		}
		if(yellowHealth<=0&&redHealth<=0){
		    enemyManagerScript.enemyList.Remove(gameObject);
            Destroy (this.gameObject);
		}
		recoveryTimer -= Time.deltaTime;
		if(recoveryTimer<=0){
			redHealth = 3;
			yellowHealth = 3;
			GetComponent<Renderer> ().material = orangeJellyMaterial;
			sphere.GetComponent<Renderer> ().material = orangeJellyMaterial;
		}
	}

	//Used to call this void in the bullet scripts
	//since currentHealth is a private variable
	public void EnemyDamaged (int damage) {
		//currentHealth -= damage;
		GetComponent<ParticleSystem> ().Play ();
	}
    public void PoisonDamaged()
    {
        GetComponent<ParticleSystem>().Play();
        if (redHealth > 0)
        {
            redHealth -= 1;
            recoveryTimer = 5f;
        }
        else
        {
            yellowHealth -= 1;
            recoveryTimer = 5f;
        }
    }
    public void OnCollisionEnter (Collision other){
		if(other.gameObject.CompareTag("RedBullet")){
			redHealth -= 1;
		    if (gameObject.GetComponent<StandardEnemyBehaviour>().isAggroPlayer == false)
		    {
		        gameObject.GetComponent<StandardEnemyBehaviour>().AggroToggle();
		    }
            recoveryTimer = 5f;
		}
		if(other.gameObject.CompareTag("YellowBullet")){
			yellowHealth -= 1;
		    if (gameObject.GetComponent<StandardEnemyBehaviour>().isAggroPlayer == false)
		    {
		        gameObject.GetComponent<StandardEnemyBehaviour>().AggroToggle();
		    }
            recoveryTimer = 5f;
		}
	}
}
