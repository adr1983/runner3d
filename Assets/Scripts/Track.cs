using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Track : MonoBehaviour
{
    /*int[] randomLevels = new int[] {2, 3, 6, 9};
    Application.LoadLevel(randomLevels[Random.Range(0, randomLevels.Length)]);*/


    public GameObject[] obstacles;
    public Vector2 numberOfObstacles;
    public GameObject coin;
    public Vector2 numberOfCoins;

    public List<GameObject> newObstacles;
    public List<GameObject> newCoins;


    // Use this for initialization
    void Start()
    {
        int newNumberOfObstacles = (int) Random.Range(numberOfObstacles.x, numberOfObstacles.y);
        int newNumberOfCoins = (int) Random.Range(numberOfCoins.x, numberOfCoins.y);

        for (int i = 0; i < newNumberOfObstacles; i++)
        {
            newObstacles.Add(Instantiate(obstacles[Random.Range(0, obstacles.Length)], transform));
            newObstacles[i].SetActive(false);
        }

        for (int i = 0; i < newNumberOfCoins; i++)
        {
            newCoins.Add(Instantiate(coin, transform));
            newCoins[i].SetActive(false);
        }

        PositionateObstacles();
        PositionateCoins();
    }

    void PositionateObstacles()
    {
        for (int i = 0; i < newObstacles.Count; i++)
        {
            float posZMin = (500f / newObstacles.Count) + (500f / newObstacles.Count) * i;
            float posZMax = (500f / newObstacles.Count) + (500f / newObstacles.Count) * i + 1;
            newObstacles[i].transform.localPosition = new Vector3(0, 1, Random.Range(posZMin, posZMax));
            newObstacles[i].SetActive(true);
            if (newObstacles[i].GetComponent<ChangeLane>() != null)
                newObstacles[i].GetComponent<ChangeLane>().PositionLane();
        }
    }
    
    void PositionateCoins()
    {
        for (int i = 0; i < newCoins.Count; i++)
        {
            float posZMin = (500f / newCoins.Count) + (500f / newCoins.Count) * i;
            float posZMax = (500f / newCoins.Count) + (500f / newCoins.Count) * i + 1;
            newCoins[i].transform.localPosition = new Vector3(0, 1, Random.Range(posZMin, posZMax));
            newCoins[i].SetActive(true);
            if (newCoins[i].GetComponent<ChangeLane>() != null)
                newCoins[i].GetComponent<ChangeLane>().PositionLane();
        }
    }
    

    /*void PositionateCoins()
    {
        float minZPos = 10f;
        for (int i = 0; i < newCoins.Count; i++)
        {
            float maxZPos = minZPos + 500f;
            float randomZPos = Random.Range(minZPos, maxZPos);
            newCoins[i].transform.localPosition = new Vector3(transform.position.x, transform.position.y, randomZPos);
            newCoins[i].SetActive(true);
            newCoins[i].GetComponent<ChangeLane>().PositionLane();
            minZPos = randomZPos + 1;
        }
    }*/

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerMotorSwipe>().IncreaseSpeed();
            transform.position = new Vector3(0, 0, transform.position.z + 500 * 2);
            PositionateObstacles();
            PositionateCoins();
        }
    }
}