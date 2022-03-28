using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scence : MonoBehaviour
{
    public void ChangeScense(int numberScene)
    {
        SceneManager.LoadScene(numberScene);
    }  
    
    public void Exit()
    {
        Application.Quit();
    }
}
