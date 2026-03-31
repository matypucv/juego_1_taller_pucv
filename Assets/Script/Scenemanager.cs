using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
public class Scenemanager : MonoBehaviour
{
    public void reloadcurrentscene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            reloadcurrentscene();
        }
    }

    public void change_scene(string Name_scene)
    {
        SceneManager.LoadScene(Name_scene);
    }

}
