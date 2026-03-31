using UnityEngine;
using UnityEngine.Events;
public class Timer : MonoBehaviour
{
    public float timer;
    public bool on;
    public bool timeout = true;
    public UnityEvent Ontimeout;
    void Update()
    {
        if(on)
        {
            timer -= Time.deltaTime;
            if(timer <= 0f && timeout == true)
            {
                Ontimeout.Invoke();
                timeout = false;
            }
        }
        else
        {
            return;
        }
    }
    public void start_timer()
    {
        on = true;
    }

    public void stop_timer()
    {
        on = false;
    }


}
