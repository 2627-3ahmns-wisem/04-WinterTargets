using UnityEngine;

public class Target : MonoBehaviour
{
    public GameManager gameManager;
    public int points = 10;
    public float speed = 150f;   
    public float lifeTime = 2f;
    public float maxX = 365f;
    public float maxY = 202f;
    private Vector2 direction;
    private bool alreadyHit = false;

    private void Start()
    {
        //create a reference to the GameManager script
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        direction = Random.insideUnitCircle.normalized;

        Destroy(gameObject, lifeTime);
    }

    private void Update()
    {
        Vector2 position = transform.localPosition;
        position += direction * speed * Time.deltaTime;

        if (position.x > maxX || position.x < -maxX)
        {
            direction.x = -direction.x;
            position.x = Mathf.Clamp(position.x, -maxX, maxX);
        }

        if (position.y > maxY || position.y < -maxY)
        {
            direction.y = -direction.y;
            position.y = Mathf.Clamp(position.y, -maxY, maxY);
        }

        //localPosition, because the target is a child of ParentTargets
        transform.localPosition = new Vector3(position.x, position.y, transform.localPosition.z);
    }

    private void OnMouseDown()
    {
        if (alreadyHit || !gameManager.IsRunning) return;
        alreadyHit = true;

        gameManager.AddScore(points);
        Destroy(gameObject);
    }
}