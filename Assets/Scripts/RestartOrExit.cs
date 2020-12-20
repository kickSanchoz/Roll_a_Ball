using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Класс, отвечающий за взаимодействие игрока с элементами интерфейса во время игры
public class RestartOrExit : MonoBehaviour
{
    public string sceneName = "GameScene";

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            BackToMenu();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            TryAgain();
        }    
    }

    public void TryAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
