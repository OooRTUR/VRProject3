using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class Trap : MonoBehaviour {

    [SerializeField]Animator animator;


	void OnTriggerEnter (Collider other) {
        //SetPlayerUnmovable(other);
        MakeAnimation();
        MakeDamage();
	}

    void SetPlayerUnmovable(Collider other)
    {
        other.GetComponent<SDK_InputSimulator>().isMoveble = false;
        Debug.Log("You're Dead!");
    }

    void MakeAnimation()
    {
        if (animator != null)
        {
            animator.SetTrigger("ItsATrap!");
        }
    }

    void MakeDamage()
    {
        PlayerHealth playerHP = FindObjectOfType<PlayerHealth>();
        if (playerHP != null)
            playerHP.TakeDamage(70);
    }
}
