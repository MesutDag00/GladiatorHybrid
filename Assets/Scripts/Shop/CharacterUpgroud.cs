using System;
using DefaultNamespace;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterUpgroud : MonoBehaviour
{
    public ShopBuy[] CharacterPoroptys;

    public TMP_Text[] PriceTexts;

    public TMP_Text MoneyText;

    public GameObject[] SpeedCheckBox;
    public GameObject[] HealthCheckBox;
    public GameObject[] AttackCheckBox;

    public Button CharacterButton1, CharacterButton2;
    public GameObject Character1, Character2;


    private void Start()
    {
        if (PlayerPrefs.GetInt("Level") == 0) PlayerPrefs.SetInt("Level", 1);
        FirstLogin(PlayerData.ChracterNumber);
        UpgroudMoney();
        CharacterPoroptys = new[]
        {
            new ShopBuy()
            {
                SpeedBuy = new[] { 100, 200, 300, 400 },
                AttackBuy = new[] { 100, 200, 300, 400 },
                HealthBuy = new[] { 100, 200, 300, 400 },
            },
            new ShopBuy()
            {
                SpeedBuy = new[] { 200, 300, 400, 500 },
                AttackBuy = new[] { 200, 300, 400, 500 },
                HealthBuy = new[] { 200, 300, 400, 500 },
            },
        };

        CheckBoxController();
        AssingShop();
        CharecterController();
    }

    public void CharecterController()
    {
        if (PlayerData.ChracterNumber == 0)
        {
            CharacterButton1.onClick.Invoke();
            Character1.SetActive(true);
            Character2.SetActive(false);
        }
        else
        {
            CharacterButton2.onClick.Invoke();
            Character1.SetActive(false);
            Character2.SetActive(true);
        }
    }

    public void UpgroudMoney() => MoneyText.text = PlayerPrefs.GetInt("Money").ToString("n0");

    private void CheckBoxController()
    {
        AssingCheckBox("SpeedIndex", SpeedCheckBox);
        AssingCheckBox("HealthIndex", HealthCheckBox);
        AssingCheckBox("AttackIndex", AttackCheckBox);
    }

    public void AssingShop()
    {
        PriceTexts[0].text = PlayerPrefs.GetInt($"SpeedIndex{PlayerData.ChracterNumber}") >= 4
            ? String.Empty
            : CharacterPoroptys[PlayerData.ChracterNumber]
                .SpeedBuy[PlayerPrefs.GetInt($"SpeedIndex{PlayerData.ChracterNumber}")].ToString("n0");
        PriceTexts[1].text = PlayerPrefs.GetInt($"HealthIndex{PlayerData.ChracterNumber}") >= 4
            ? String.Empty
            : CharacterPoroptys[PlayerData.ChracterNumber]
                .HealthBuy[PlayerPrefs.GetInt($"HealthIndex{PlayerData.ChracterNumber}")].ToString("n0");
        PriceTexts[2].text = PlayerPrefs.GetInt($"AttackIndex{PlayerData.ChracterNumber}") >= 4
            ? String.Empty
            : CharacterPoroptys[PlayerData.ChracterNumber]
                .AttackBuy[PlayerPrefs.GetInt($"AttackIndex{PlayerData.ChracterNumber}")].ToString("n0");

        for (int i = 0; i < PriceTexts.Length; i++)
        {
            if (PriceTexts[i].text == String.Empty)
                PriceTexts[i].transform.parent.gameObject.SetActive(false);
            else
                PriceTexts[i].transform.parent.gameObject.SetActive(true);
        }
    }

    public void BuyItem(string nameBuy)
    {
        if (PlayerPrefs.GetInt($"{nameBuy}{PlayerData.ChracterNumber}") >= 4) return;
        if (PlayerPrefs.GetInt("Money") >= CharacterPoroptys[PlayerData.ChracterNumber]
                .SpeedBuy[PlayerPrefs.GetInt($"{nameBuy}{PlayerData.ChracterNumber}")])
        {
            PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") - CharacterPoroptys[PlayerData.ChracterNumber]
                .SpeedBuy[PlayerPrefs.GetInt($"{nameBuy}{PlayerData.ChracterNumber}")]);
            PlayerPrefs.SetInt($"{nameBuy}{PlayerData.ChracterNumber}",
                PlayerPrefs.GetInt($"{nameBuy}{PlayerData.ChracterNumber}") + 1);
        }

        CheckBoxController();
        AssingShop();
        UpgroudMoney();
    }

    private void AssingCheckBox(string nameBuy, GameObject[] objects)
    {
        for (int i = 0; i < objects.Length; i++)
        {
            if (i <= PlayerPrefs.GetInt($"{nameBuy}{PlayerData.ChracterNumber}") - 1)
                objects[i].transform.GetComponent<Image>().color = Color.green;
            else
                objects[i].transform.GetComponent<Image>().color = Color.white;
        }
    }

    public void SelectChracter(int index)
    {
        PlayerData.ChracterNumber = index;
        FirstLogin(PlayerData.ChracterNumber);
        CheckBoxController();
        AssingShop();
    }

    private void FirstLogin(int a)
    {
        if (PlayerPrefs.GetInt($"FirstLogin{a}") == 0)
        {
            PlayerPrefs.SetInt("SpeedIndex", 0);
            PlayerPrefs.SetInt($"AttackIndex{a}", 0);
            PlayerPrefs.SetInt($"HealthIndex{a}", 0);
            PlayerPrefs.SetInt($"FirstLogin{a}", 1);
        }
    }

    public void StartGame() => SceneManager.LoadScene(PlayerPrefs.GetInt("Level"));

    public void QuitButton() => Application.Quit();
}

public class ShopBuy
{
    public int[] SpeedBuy;
    public int[] AttackBuy;
    public int[] HealthBuy;
}