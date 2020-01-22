using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Image[] lifeRobo;
    public Sprite robo1, robo2, robo3;
    public Text coinText;
    public GameObject gameOverPanel;
    public TMPro.TextMeshProUGUI scoreText;
    public GameObject healthPanel;

    public void UpdateLives(int lives)
    {
        for (int i = 0; i < lifeRobo.Length; i++)
        {
            if (lives > i + 1)
            {
                lifeRobo[i].enabled = false;
//                Debug.Log("lives = " +lives);
//                lifeRobo[i].color = Color.black;
            }
            /*if (lives == i)
            {
                lifeRobo[i].enabled = false;
            }*/
            else if (lives < i)
            {
                lifeRobo[i].enabled = false;
//                Debug.Log("lives = " +lives);
//                lifeRobo[i].color = Color.white;
//				Debug.Log("color change");
            }
        }
    }

    public void UpdateCoins(int coin)
    {
        coinText.text = coin.ToString();
    }

    public void UpdateScore(int score)
    {
        scoreText.text = score + "";
    }
}