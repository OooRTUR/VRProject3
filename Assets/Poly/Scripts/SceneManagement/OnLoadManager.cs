﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnLoadManager : MonoBehaviour {

    public static OnLoadManager instance;
    [HideInInspector]public int currentWeather = -1;
    [HideInInspector]public Material currentFox;
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

    public void ReloadScene (bool value)
    {
        FadeController.instance.Fader(1);
        mainMenuMode = value;
    }

    public void ExitGame ()
    {
        Application.Quit();
    }


}
