using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject schneemannTarget;
    public GameObject eisblockTarget;
    public GameObject geschenkTarget;
    public GameObject parentOfTargets;
    public GameObject winSprite;
    public Text scoreText;
    public Text timeText; 
    public Text resultText;
    public float roundDuration = 60f;
    public int winScore = 100;
    private int score = 0;
    private float timeLeft;
    private bool isRunning = false;

    public bool IsRunning
    {
        get { return isRunning; }
    }

    private void Start()
    {
        StartRound();
    }

    private void Update()
    {
        //every shoot (clicking left mouse button) produce a shooting sound.
        if (isRunning && Input.GetMouseButtonDown(0))
        {
            gameObject.GetComponent<AudioSource>().Play();
        }

        if (isRunning)
        {
            timeLeft -= Time.deltaTime;

            if (timeLeft <= 0f)
            {
                timeLeft = 0f;
                EndRound();
            }

            UpdateTimeText();
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                StartRound();
            }
        }
    }

    public void StartRound()
    {
        foreach (Transform child in parentOfTargets.transform)
        {
            Destroy(child.gameObject);
        }

        score = 0;
        timeLeft = roundDuration;
        isRunning = true;

        scoreText.text = score.ToString();
        UpdateTimeText();
        winSprite.SetActive(false);
        resultText.gameObject.SetActive(false);

        CancelInvoke("Spawn");
        InvokeRepeating("Spawn", 1f, 1f);
    }
    private void EndRound()
    {
        isRunning = false;
        CancelInvoke("Spawn");
        foreach (Transform child in parentOfTargets.transform)
        {
            Destroy(child.gameObject);
        }

        if (score >= winScore)
        {
            winSprite.SetActive(true);
            resultText.text = "Gewonnen! " + score + " Punkte\nR = neue Runde";
        }
        else
        {
            resultText.text = "Zeit abgelaufen! " + score + " Punkte\nR = neue Runde";
        }

        resultText.gameObject.SetActive(true);
    }

    private void UpdateTimeText()
    {
        timeText.text = Mathf.CeilToInt(timeLeft).ToString();
    }

    private void Spawn()
    {
        if (!isRunning) return;

        float randomX = Random.Range(-365f, 365f);
        float randomY = Random.Range(-202f, 202f);

        Vector2 random2DPosition = new Vector2(randomX, randomY);

        GameObject newTarget = Instantiate(ChooseTargetType(), parentOfTargets.transform);
        newTarget.transform.localPosition = random2DPosition;
    }

    private GameObject ChooseTargetType()
    {
        int randomNumber = Random.Range(0, 100);

        if (randomNumber < 60)
        {
            return schneemannTarget;
        }
        else if (randomNumber < 90)
        {
            return eisblockTarget;
        }
        else
        {
            return geschenkTarget;
        }
    }

    public void AddScore(int points)
    {
        if (!isRunning) return;

        score += points;
        scoreText.text = score.ToString();
    }
}