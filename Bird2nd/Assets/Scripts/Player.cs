using UnityEngine;

public class Player : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public Sprite[] sprites;
    private int spriteIndex;
    private Spawner spawner;

    public float strength = 5f;
    public float gravity = -9.81f;
    public float tilt = 5f;
    public float speedUp = 5f;
    public int hasSpeedUp = 0;

    private Vector3 direction;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spawner = FindObjectOfType<Spawner>();
    }

    private void Start()
    {
        InvokeRepeating(nameof(AnimateSprite), 0.15f, 0.15f);
    }

    private void OnEnable()
    {
        Vector3 position = transform.position;
        position.y = 0f;
        transform.position = position;
        direction = Vector3.zero;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (hasSpeedUp > 0 && Trunks.tmpspeed == 0)
            {
                Debug.Log("AAAA");
                hasSpeedUp -= 1;
                Trunks.tmpspeed = speedUp;
                spawner.SetSpawnRate(Trunks.speed, Trunks.speed + Trunks.tmpspeed);
            }
        }
        else {
            if (Trunks.tmpspeed != 0 && Input.GetKeyUp(KeyCode.Return))
            {
                Debug.Log("BBBB");
                Trunks.tmpspeed = 0;
                spawner.SetSpawnRate(Trunks.speed + speedUp, Trunks.speed);
            }
            if (Trunks.tmpspeed == 0 && (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))) {
                direction = Vector3.up * strength;
            }
            else if (Trunks.tmpspeed > 0)
            {
                direction = Vector3.up * 1;
            }
        }

        // Apply gravity and update the position
        direction.y += gravity * Time.deltaTime;
        transform.position += direction * Time.deltaTime;

        // Tilt the bird based on the direction
        Vector3 rotation = transform.eulerAngles;
        if (Trunks.tmpspeed > 0)
        {
            rotation.z = direction.y;
        }
        else {
            rotation.z = direction.y * tilt;
        }
        transform.eulerAngles = rotation;
    }

    private void AnimateSprite()
    {
        spriteIndex++;

        if (spriteIndex >= sprites.Length) {
            spriteIndex = 0;
        }

        if (spriteIndex < sprites.Length && spriteIndex >= 0) {
            spriteRenderer.sprite = sprites[spriteIndex];
        }
    }
     private void OnTriggerEnter2D(Collider2D other)
    {   
        if (other.gameObject.CompareTag("Obstacle")) {
            FindObjectOfType<GameManager>().GameOver();
        } else if (other.gameObject.CompareTag("Scoring")) {
            FindObjectOfType<GameManager>().IncreaseScore();
        }
    }

    
}