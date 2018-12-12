using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayTimeController : MonoBehaviour {

    [SerializeField] Material morning;
    [SerializeField] Material afternoon;
    [SerializeField] Material evening;
    [SerializeField] Material night;

	
	void Start () {
        if (OnLoadManager.instance.currentWeather != null)
            RenderSettings.skybox = OnLoadManager.instance.currentWeather;
        else
            RenderSettings.skybox = morning;
	}
	
    void OnGUI()
    {
        if (GUI.Button(new Rect(10, 10, 150, 100), "morning"))
        {
            SetMorning();
        }

        if (GUI.Button(new Rect(10, 120, 150, 100), "afternoon"))
        {
            SetAfternoon();
        }

        if (GUI.Button(new Rect(10, 230, 150, 100), "evening"))
        {
            SetEvening();
        }

        if (GUI.Button(new Rect(10, 340, 150, 100), "night"))
        {
            SetNight();
        }
    }

    public void SetMorning()
    {
        RenderSettings.skybox = morning;
        OnLoadManager.instance.currentWeather = morning;
    }
    public void SetAfternoon()
    {
        RenderSettings.skybox = afternoon;
        OnLoadManager.instance.currentWeather = afternoon;
    }
    public void SetEvening()
    {
        RenderSettings.skybox = evening;
        OnLoadManager.instance.currentWeather = evening;
    }
    public void SetNight()
    {
        RenderSettings.skybox = night;
        OnLoadManager.instance.currentWeather = night;
    }

}
