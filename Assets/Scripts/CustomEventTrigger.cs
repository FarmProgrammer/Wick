using UnityEngine;
using UnityEngine.Events;

public class CustomEventTrigger : MonoBehaviour
{
    public UnityEvent TriggerEvent;

    void Start()
    {
        if (TriggerEvent == null)
        {
            TriggerEvent = new UnityEvent();
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag=="Player")
        {
            TriggerEvent.Invoke();
        }
    }
}
