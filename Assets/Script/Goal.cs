using UnityEngine;
using UnityEngine.Events;

public class Goal : MonoBehaviour
{
    [SerializeField] string tagToCheck;
    [SerializeField] UnityEvent goalEvent;

    private bool tocado = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (tocado) return;

        if (other.CompareTag(tagToCheck))
        {
            tocado = true;
            goalEvent?.Invoke();
        }
    }
}