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

    //P01: points needed to win (adjustable in the Inspector)
    public int winScore = 100;

    private int score = 0;
    private bool win = false;

    private void Start()
    {
        scoreText.text = score.ToString();
        InvokeRepeating("Spawn", 1f, 1f);
    }

    private void Update()
    {
        //if the player reaches winScore, the player has won and the Spawn method will not be called further.
        if (win)
        {
            CancelInvoke("Spawn");
        }

        //every shoot (clicking left mouse button) produce a shooting sound.
        if (Input.GetMouseButtonDown(0))
        {
            gameObject.GetComponent<AudioSource>().Play();
        }
    }

    //Spawn a target at a random position within a specified x and y range.
    private void Spawn()
    {
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

    //is called in the class Target
    public void AddScore(int points)
    {
        if (win) return;

        score += points;
        print(score);

        scoreText.text = score.ToString();

        if (score >= winScore)
        {
            win = true;
            winSprite.SetActive(true);
        }
    }
}