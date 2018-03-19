﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenEnemyHealth : MonoBehaviour {

	//Private variables
	public int blueHealth=3;
	public int yellowHealth=3;
	private float recoveryTimer = 5f;
	public GameObject sphere;

	public Material blueJellyMaterial;
	public Material yellowJellyMaterial;
	public Material greenJellyMaterial;

    private EnemyManager enemyManagerScript;

    void Start () {
        //Setting the current health to be the health variable
        //so that when we start the game, the enemy has full HP
        enemyManagerScript = GameObject.FindGameObjectWithTag("GameManager").GetComponent<EnemyManager>();
    }

	void Update () {
		//If the enemy reaches 0 HP, destroy the enemy
		if(blueHealth<=0&&yellowHealth>0){
			GetComponent<Renderer> ().material = yellowJellyMaterial;
			sphere.GetComponent<Renderer> ().material = yellowJellyMaterial;
		}
		if(blueHealth<0){
			blueHealth=0;
		}
		if(yellowHealth<=0&&blueHealth>0){
			GetComponent<Renderer> ().material = blueJellyMaterial;
			sphere.GetComponent<Renderer> ().material = blueJellyMaterial;
		}
		if(yellowHealth<0){
			yellowHealth=0;
		}
		if(yellowHealth<=0&&blueHealth<=0){
		    enemyManagerScript.enemyList.Remove(gameObject);
            Destroy (this.gameObject);
		}
		recoveryTimer -= Time.deltaTime;
		if(recoveryTimer<=0){
			blueHealth = 3;
			yellowHealth = 3;
			GetComponent<Renderer> ().material = greenJellyMaterial;
			sphere.GetComponent<Renderer> ().material = greenJellyMaterial;
		}
	}

	//Used to call this void in the bullet scripts
	//since currentHealth is a private variable
	public void EnemyDamaged (int damage) {
		//currentHealth -= damage;
		GetComponent<ParticleSystem> ().Play ();
	}
	public void OnCollisionEnter (Collision other){
		if(other.gameObject.CompareTag("BlueBullet")){
			blueHealth -= 1;
			recoveryTimer = 5f;
		}
		if(other.gameObject.CompareTag("YellowBullet")){
			yellowHealth -= 1;
			recoveryTimer = 5f;
		}
	}
}
