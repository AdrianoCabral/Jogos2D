using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public int gameStartScene;

    private void Update()
    {

        if (Input.GetKeyDown("space")){
            StartGame();
        }
    }
    public void StartGame()
    {
        SceneManager.LoadScene(gameStartScene);
    }
}
