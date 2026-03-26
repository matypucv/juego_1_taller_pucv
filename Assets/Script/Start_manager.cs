using UnityEngine;
using TMPro;
public class Start_manager : MonoBehaviour
{
    public bool pausing;
    public TextMeshProUGUI Textbutton;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pause_game();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void pause_game()
    {
        Time.timeScale = 0f;
        pausing = true;
        Textbutton.text = "start";
    }

    public void start_game()
    {
        Time.timeScale = 1f;
        pausing = false;
        Textbutton.text = "pause";
    }

    public void change_state()
    {
      if(pausing)
        {
            start_game();
        }
        else
        {
            pause_game();
        }
    }
}
