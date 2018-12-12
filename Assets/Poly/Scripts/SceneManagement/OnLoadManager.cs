using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnLoadManager : MonoBehaviour {

    public static OnLoadManager instance;

    [SerializeField] public bool mainMenuMode = true;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(0);
            mainMenuMode = false;
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            SceneManager.LoadScene(0);
            mainMenuMode = true;
        }
    }


}
