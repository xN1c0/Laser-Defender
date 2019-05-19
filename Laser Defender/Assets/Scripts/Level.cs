using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        if (Input.GetKeyDown(KeyCode.Return) && currentScene.name.Equals("StartMenu"))
        {
            SceneManager.LoadScene("Core");
        }
        if (Input.GetKeyDown(KeyCode.Return) && currentScene.name.Equals("GameOver"))
        {
            LoadCoreScene();
            FindObjectOfType<GameSession>().ResetScore();
        }
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void LoadCoreScene()
    {
        FindObjectOfType<GameSession>().ResetScore();
        SceneManager.LoadScene("Core");
    }

    public void LoadGameOverScene()
    {
        StartCoroutine(WaitAndLoad());
    }

    IEnumerator WaitAndLoad()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("GameOver");
    }
}
