using UnityEngine;

public class UIManagerSingleton : MonoBehaviour
{
    public static UIManagerSingleton instance;

    public Canvas shipCanvas;
    public Canvas soulsCanvas;
    public Canvas shipSectionBuffetCanvas;
    public Canvas portCanvas;
    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }

        DontDestroyOnLoad(gameObject);
    }

    public void ToggleShipCanvas()
    { 
        shipCanvas.enabled = !shipCanvas.enabled;
    }
    public void ToggleSoulsCanvas()
    {
        soulsCanvas.enabled = !soulsCanvas.enabled;     
    }
    public void ToggleShipSectionBuffetCanvas()
    {
        shipSectionBuffetCanvas.enabled = !shipSectionBuffetCanvas.enabled;
    }

    public void TogglePortCanvas()
    {
        portCanvas.enabled = !portCanvas.enabled;
    }
    
}
