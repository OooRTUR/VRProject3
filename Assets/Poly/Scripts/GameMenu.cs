using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameMenu : MonoBehaviour {

    public static GameMenu instance;

    [HideInInspector]public int mouses, rabbits, chickens;
    public Text mT, rT, cT;

    public Transform menuPoint;
    Canvas canvas;
    RectTransform rectTrans;

    private void Awake()
    {
        instance = this;
        canvas = GetComponent<Canvas>();
        rectTrans = GetComponent<RectTransform>();
    }

    private void Start()
    {
        UpdateMenu();
    }

    private void Update()
    {
        if(canvas.enabled) {
            float distance = Vec3Mathf.DistanceTo(rectTrans.position, menuPoint.position);
            if (distance > 20)
                canvas.enabled = false;
        }
        if(OVRInput.Get(OVRInput.Button.One)) {
            rectTrans.position = menuPoint.position;
            rectTrans.rotation = Quaternion.LookRotation(menuPoint.forward);
            canvas.enabled = true;
        }
    }

    public void UpdateMenu ()
    {
        mT.text = "Мыши: " + mouses.ToString();
        rT.text = "Зайцы: " + rabbits.ToString();
        cT.text = "Куропатки: " + chickens.ToString();
    }

    public void OnClickMainMenu()
    {
        OnLoadManager.instance.ReloadScene(true);
    }
}
