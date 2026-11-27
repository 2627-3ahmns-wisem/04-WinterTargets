using UnityEngine;

public class Target : MonoBehaviour
{
    public GameManager gameManager;

    public int points = 10;
    private bool alreadyHit = false;

    private void Start()
    {
        //create a reference to the GameManager script
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        Destroy(gameObject, 2f);
    }

    private void OnMouseDown()
    {
        if (alreadyHit) return;
        alreadyHit = true;

        //give the points of this target to the GameManager
        gameManager.AddScore(points);
        Destroy(gameObject);
    }
}