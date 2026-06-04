using UnityEngine;

public class FinishGate : MonoBehaviour
{
    public static event TimerEvent FinishRace;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            FinishRace?.Invoke();
        }
    }
}