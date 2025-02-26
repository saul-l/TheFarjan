using UnityEngine;

public class ShipSectionUI : MonoBehaviour
{
    public Canvas SoulsCanvas;
    
    public void OpenSoulsMenu()
    {
        if(SoulsCanvas != null)
        {
            SoulsCanvas.enabled = true;
        }
    }
}
