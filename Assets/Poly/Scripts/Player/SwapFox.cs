using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwapFox : MonoBehaviour {

    public Material[] materials;
    public SkinnedMeshRenderer rend;
    public Toggle[] foxMarks;
    Material[] swapMaterials;

    private void Awake()
    {
        swapMaterials = new Material[2];
        swapMaterials[0] = materials[0];
    }

    private void OnEnable()
    {
        swapMaterials[1] = OnLoadManager.instance.currentFox != null ? OnLoadManager.instance.currentFox : materials[0];
        rend.materials = swapMaterials;
    }

    public void SwapMaterial (int matIndex)
    {
        swapMaterials[1] = materials[matIndex];
        foxMarks[matIndex - 1].isOn = true;
        OnLoadManager.instance.currentFox = swapMaterials[1];
        rend.materials = swapMaterials;
    }
}
