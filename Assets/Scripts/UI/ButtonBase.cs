using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.LowLevel;

enum InterActableUIState { Enabled, Disabled, SuccessEnabled, SuccessDisabled, Failure };
public class ButtonBase : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{

    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }
}
