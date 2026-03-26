using UnityEngine;
using UnityEngine.Events;

public class Goal : MonoBehaviour
{
    [SerializeField] string tagToCheck;
    [SerializeField] UnityEvent goalEvent;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == tagToCheck)
        {
            goalEvent?.Invoke();
        }
    }
}
