using UnityEngine;

public class ShipSectionUI : MonoBehaviour
{
    // These values are assigned outside component. It's a bit icky, but this is how we roll for now
    public Canvas SoulsCanvas;
    

    public void OpenSoulsMenu()
    {
        if(SoulsCanvas != null)
        {
            SoulsCanvas.enabled = true;
        }
    }
}
