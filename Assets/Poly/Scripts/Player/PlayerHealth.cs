using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {

	public float startHealth;
	public Image lowHPImage;
    Color hpColor;

	[SerializeField]float health;

	void Start () {
		health = startHealth;
        hpColor = lowHPImage.color;
	}

	void Update () {
        hpColor.a = (100 - health) / 100;
        lowHPImage.color = hpColor;
		if(health < startHealth)
			health += Time.deltaTime * 3;
	}

	public void TakeDamage (float damage) {
		health -= damage;
		if (health <= 0)
			Die ();
	}

	void Die () {
        OnLoadManager.instance.ReloadScene(false);
	}
}
