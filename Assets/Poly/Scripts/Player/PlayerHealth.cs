using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {

	public float startHealth;
	public Image lowHPImage;

	[SerializeField]float health;

	void Start () {
		health = startHealth;
	}

	void Update () {
		lowHPImage.enabled = health < startHealth / 2 ? true : false;
		if(health < startHealth)
			health += Time.deltaTime * 2;
	}

	public void TakeDamage (float damage) {
		health -= damage;
		if (health <= 0)
			Die ();
	}

	void Die () {
		Time.timeScale = 0;
	}
}
