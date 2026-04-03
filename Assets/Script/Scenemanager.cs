using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Scenemanager : MonoBehaviour
{
    [SerializeField] float transitionTime;
    float timer;
    bool outTransitioning;
    bool inTransitioning;
    [SerializeField] Image panel;
    string toLoadName;

    void Start()
    {
        inTransitioning = true;
        timer = 0;
    }

    void Update()
    {
        if (outTransitioning)
        {
            timer -= Time.unscaledDeltaTime;
            UpdateColor(timer);
            if (timer <= 0f)
            {
                SceneManager.LoadScene(toLoadName);
            }
        }
        else if(inTransitioning)
        {
            timer += Time.unscaledDeltaTime;
            UpdateColor(timer);
            if (timer >= transitionTime)
            {
                inTransitioning = false;
            }
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            reloadcurrentscene();
        }
    }


    public void reloadcurrentscene()
    {
        change_scene(SceneManager.GetActiveScene().name);
    }

    public void change_scene(string Name_scene)
    {
        outTransitioning = true;
        timer = transitionTime;
        toLoadName = Name_scene;
    }

    void UpdateColor(float currentTimeLeft)
    {
        float alpha = Mathf.Sqrt(1f - (currentTimeLeft / transitionTime));
        Color color = Color.black;
        color.a = alpha;
        panel.color = color;
    }

}