using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestGameManager : MonoBehaviour {

    [SerializeField] public bool mainMenuMode = true;
    [SerializeField] bool isInited;
	// Use this for initialization
	void Awake () {
        GameObject[] gms = GameObject.FindGameObjectsWithTag("GameManager");
        if (gms.Length > 1)
        {
            Destroy(gms[1]);
        }
        if (!isInited)
        {
            DontDestroyOnLoad(this);
            isInited = true;
        }
        Debug.Log("Завершился gm");
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("testLoadScene");
            mainMenuMode = true;
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            SceneManager.LoadScene("testLoadScene");
            mainMenuMode = false;
        }
	}
}
