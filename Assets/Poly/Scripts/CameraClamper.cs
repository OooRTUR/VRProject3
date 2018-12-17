using UnityEngine;
using UnityEngine.UI;
public class CameraClamper : MonoBehaviour {

    [SerializeField] OVRPlayerController player;
    [SerializeField] GameObject chooseMenu;
    [SerializeField]Transform cameraTrans;
    [SerializeField] Image fader;
    Color alpha;

    private void Start()
    {
        alpha = fader.color;
    }

    void Update()
    {
        if ((cameraTrans.eulerAngles.x > 60 && cameraTrans.eulerAngles.x < 100) && OnLoadManager.instance.currentFox != null && !player.canMove)
            StartGame();
        if (cameraTrans.eulerAngles.x > 68 && cameraTrans.eulerAngles.x < 100)
            UpdateFader(1);
        else
            UpdateFader(0);
    }

    void UpdateFader (float alphaValue)
    {
        alpha.a = alphaValue;
        //fader.color = alpha;
        fader.color = Color.Lerp(fader.color, alpha, 0.2f);
    }

    void StartGame ()
    {
        player.canMove = true;
        chooseMenu.SetActive(false);
    }
}
