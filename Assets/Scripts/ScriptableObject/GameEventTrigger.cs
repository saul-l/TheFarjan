using UnityEngine;

public class GameEventTrigger : MonoBehaviour
{
    [SerializeField] GameEvent[] gameEvents;

    public void TriggerGameEvent(int eventIndex)
    {
        if (gameEvents[eventIndex] != null)
        {
            gameEvents[eventIndex].Raise();
        }
    }
}
