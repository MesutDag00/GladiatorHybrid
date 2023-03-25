using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public static GameOver Instance;
    public EnemyManger[] EnemyMangers;
    private int _countEnemys;
    public GameObject WinnerPanel;
    public GameObject LostPanel;
    public GameObject[] Players;


    private void Awake() => Players[PlayerData.ChracterNumber].SetActive(true);

    void Start()
    {
        _countEnemys = EnemyMangers.Length;
        Instance = this;
    }

    public void MainButton() => SceneManager.LoadScene(0);

    public void NextLevel()
    {
        PlayerPrefs.SetInt("Level", SceneManager.GetActiveScene().buildIndex + 1);
        SceneManager.LoadScene(PlayerPrefs.GetInt("Level"));
        PlayerData.Health = PlayerData.HealthsCharacter[PlayerData.ChracterNumber,
            PlayerPrefs.GetInt($"HealthIndex{PlayerData.ChracterNumber}")];
    }

    public void AgainButton()
    {
        SceneManager.LoadScene(PlayerPrefs.GetInt("Level"));
        PlayerData.Health = PlayerData.HealthsCharacter[PlayerData.ChracterNumber,
            PlayerPrefs.GetInt($"HealthIndex{PlayerData.ChracterNumber}")];
    }


    #region FinishedGame

    public void CheckGameController()
    {
        bool gameController = EnemyAllDeadth();
        if (gameController)
            Invoke(nameof(WinnerPanelActive), 2f);
        else if (!gameController && PlayerData.Health <= 0)
            Invoke(nameof(LostPanelActive), 2f);
    }

    private void WinnerPanelActive()
    {
        WinnerPanel.SetActive(true);
        GameObject.Find("MoneyText").GetComponent<TMP_Text>().text = "+" +
                                                                     PlayerData.MoneysLevelUp[
                                                                             PlayerPrefs.GetInt("Level")]
                                                                         .ToString("n0");
        PlayerPrefs.SetInt("Money",
            PlayerPrefs.GetInt("Money") + PlayerData.MoneysLevelUp[PlayerPrefs.GetInt("Level")]);
    }

    private void LostPanelActive() => LostPanel.SetActive(true);

    private bool EnemyAllDeadth()
    {
        int Counter = 0;
        for (int i = 0; i < EnemyMangers.Length; i++)
            if (EnemyMangers[i].Health <= 0)
                Counter++;
        return _countEnemys == Counter;
    }

    #endregion
}