using UnityEngine;
using UnityEngine.SceneManagement;

public class EscapeToScene : MonoBehaviour
{
    public string sceneName;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}