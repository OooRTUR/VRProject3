using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSettingUI : MonoBehaviour {

    [SerializeField] Sprite filled;
    [SerializeField] Sprite empty;
    [SerializeField] Image[] points;


    [SerializeField]int index = 4;
	// Use this for initialization
	void Start () {
        index = 4;	
	}
	
    public void SetIndexDown()
    {
        if (index > -1)
        {
            if (index == 5)
                index = 4;

            points[index].sprite = empty;
            index--;
        }
    }

    public void SetIndexUp()
    {
        if (index < 5)
        {
            if (index == -1)
                index = 0;

            points[index].sprite = filled;
            index++;
        }
    }
}
