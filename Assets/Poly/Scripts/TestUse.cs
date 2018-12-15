using UnityEngine;
using VRTK;

public class TestUse : VRTK_InteractUse
{
    [SerializeField] SwapFox sf;

    public override void OnControllerUseInteractableObject(ObjectInteractEventArgs e)
    {
        base.OnControllerUseInteractableObject(e);
        switch (e.target.tag)
        {
            case "FoxG":
                sf.SwapMaterial(1);
                break;
            case "FoxM":
                sf.SwapMaterial(2);
                break;
        }
    }
}
