using UnityEngine;

public class ShipUI : MonoBehaviour
{
    [SerializeField] Canvas canvas;
    [SerializeField] CanvasManager canvasManager;

    private void Start()
    {
        canvasManager.AddCanvasToList(canvas);
    }

    private void CloseCanvas()
    {

    }
}
