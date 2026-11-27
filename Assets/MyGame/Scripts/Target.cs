using System.Collections;
using UnityEngine;

public class Target : MonoBehaviour
{
    public GameManager gameManager;
    public int points = 10;
    public float speed = 150f;   
    public float lifeTime = 2f;
    public float maxX = 365f;
    public float maxY = 202f;

    //P05: hit feedback
    public AudioClip hitSound;              
    public float hitAnimationTime = 0.25f;  
    public float hitScaleFactor = 1.6f; 

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
        //P05: a hit target does not move anymore
        if (alreadyHit) return;

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

        
        gameManager.PlayHitSound(hitSound);

       
        StartCoroutine(HitAnimation());
    }

    
    private IEnumerator HitAnimation()
    {
       
        GetComponent<Collider2D>().enabled = false;

        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        Vector3 startScale = transform.localScale;
        Color startColor = spriteRenderer.color;

        float time = 0f;
        while (time < hitAnimationTime)
        {
            time += Time.deltaTime;
            float t = time / hitAnimationTime;

            transform.localScale = startScale * Mathf.Lerp(1f, hitScaleFactor, t);
            spriteRenderer.color = new Color(startColor.r, startColor.g, startColor.b, 1f - t);

            yield return null; 
        }

        Destroy(gameObject);
    }
}