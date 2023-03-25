using UnityEngine;
using UnityEngine.SceneManagement;


public class FinishedGame : MonoBehaviour
{
    public void DataReset()
    {
        PlayerPrefs.SetInt("Level", 1);
        SceneManager.LoadScene(PlayerPrefs.GetInt("Level"));
    }

    public void MainMenu() => SceneManager.LoadScene(0);    
}