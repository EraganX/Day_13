using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int money = 10000;
    public int kills = 0;
    public int survive = 10;


    public bool isGameOver = false;
    public bool isGameStart = true;

    public TMP_Text moneyText;
    public TMP_Text killText;
    public TMP_Text surviveText;

    [SerializeField] private GameObject GameOverPanel;
    

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Update()
    {
        moneyText.text = money.ToString("000");
        killText.text = kills.ToString("0");
        surviveText.text = survive.ToString("0");
    }

    public void EnableGameOver()
    {
        GameOverPanel.SetActive(true);
    }





}
