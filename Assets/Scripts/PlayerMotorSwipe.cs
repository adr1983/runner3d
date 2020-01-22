
using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class PlayerMotorSwipe : MonoBehaviour
{
    public float speed;
    public float laneSpeed;
    private Rigidbody rb;
    private float currentLaneX = 1f;
    private float currentLaneY = 1f;
    private Vector3 targetPositionX;
    private Vector3 targetPositionY;
    public int maxLife = 3;
    private int currentLife;
    public float minSpeed = 10f;
    public float maxSpeed = 30f;
    private bool invincible = false;
    public float invincibleTime;
    public GameObject model;
    private UIManager uiManager;
    [HideInInspector] public int coins;
    private static int blinkingValue;
    [HideInInspector] public float score;

    /*private bool isDead;*/
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentLife = maxLife;
        speed = minSpeed;
        blinkingValue = Shader.PropertyToID("_BlinkingValue");
        uiManager = FindObjectOfType<UIManager>();
        GameManager.gm.StartMissions();
    }

    void Update()
    {
        score += Time.deltaTime * speed;
        uiManager.UpdateScore((int) score);
//        Gather the inputs on which lane we shoud be
//        Debug.Log(MobileInput.Instance.SwipeDelta);
        if (Input.GetKeyDown(KeyCode.LeftArrow) || MobileInput.Instance.SwipeLeft)
        {
            ChangeLaneX(-2f);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) || MobileInput.Instance.SwipeRight)
        {
            ChangeLaneX(+2f);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) || MobileInput.Instance.SwipeDown)
        {
            ChangeLaneY(-1.5f);
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow) || MobileInput.Instance.SwipeUp)
        {
            ChangeLaneY(+1.5f);
        }

//        Calculate where we should be in the future
        Vector3 targetPosition = new Vector3(targetPositionX.x, targetPositionY.y, transform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, laneSpeed * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        rb.velocity = Vector3.forward * speed;
    }

    void ChangeLaneX(float direction)
    {
        float targetLane = currentLaneX + direction;
        if (targetLane < -1 || targetLane > 3)
            return;
        currentLaneX = targetLane;
        targetPositionX = new Vector3((currentLaneX - 1f), 0, 0);
    }

    void ChangeLaneY(float direction)
    {
        float targetLane = currentLaneY + direction;
        if (targetLane < 1 || targetLane > 2.5f)
            return;
        currentLaneY = targetLane;
        targetPositionY = new Vector3((0), currentLaneY - 1, 0);
    }

    /*private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.point.z > transform.position.z + 0.1f && hit.gameObject.tag == "Enemy")
            Death();
    }*/
    /*private void Death()
    {
        isDead = true;
        GetComponent<Score>().OnDeath();
        Debug.Log("Dead");
        FindObjectOfType<AudioManager>().Play("PlayerDeath");
    }*/
    public void SetSpeed(float modifier)
    {
        speed = 5.0f + modifier;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
            coins++;
            uiManager.UpdateCoins((coins));
            other.transform.parent.gameObject.SetActive(false);
        }

        if (invincible)
            return;
        if (other.CompareTag("Obstacle"))
        {
//            Debug.Log("colisao");
            currentLife--;
            Debug.Log(currentLife);
            uiManager.UpdateLives(currentLife);
            speed = 0;

            if (currentLife <= 0)
            {
                speed = 0;
                uiManager.gameOverPanel.SetActive(true);
                uiManager.healthPanel.SetActive(false);
//                Invoke("CallMenu", 2f);
            }
            else
            {
                StartCoroutine(Blinking(invincibleTime));
            }
        }
    }

    IEnumerator Blinking(float time)
    {
        invincible = true;
        float timer = 0;
        float currentBlink = 1f;
        float lastBlink = 0f;
        float blinkPeriod = 0.1f;
        bool enabled = false;
        yield return new WaitForSeconds(1f);
        speed = minSpeed;
        while (timer < time && invincible)
        {
//            Shader.SetGlobalFloat(blinkingValue, currentBlink);
            model.SetActive(enabled);
            yield return null;
            timer += Time.deltaTime;
            lastBlink += Time.deltaTime;
            if (blinkPeriod < lastBlink)
            {
                lastBlink = 0;
                currentBlink = 1f - currentBlink;
                enabled = !enabled;
            }
        }

        model.SetActive(true);
//        Shader.SetGlobalFloat(blinkingValue, 0);
        invincible = false;
    }

    void CallMenu()
    {
        GameManager.gm.EndRun();
    }

    public void IncreaseSpeed()
    {
        speed *= 1.15f;
        if (speed >= maxSpeed)
        {
            speed = maxSpeed;
        }
    }
}