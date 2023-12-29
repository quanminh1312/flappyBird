using UnityEngine;

public class Player : IntEventInvoker
{
    #region Fields
    private SpriteRenderer spriteRenderer;
    public Sprite[] sprites;
    private int spriteIndex;

    public float strength = 7f;
    public float gravity = -20f;
    public float tilt = 5f;

    private Vector3 direction;

    // collider dimensions
    float colliderHalfWidth;
    float colliderHalfHeight;

    // saved for efficient boundary checking
    float screenLeft;
    float screenRight;
    float screenTop;
    float screenBottom;

    public bool enable = false;
    bool die = false;
    #endregion

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        EventManager.AddListener(EventName.GameStartedEvent, HandleGameStartEvent);
    }

    private void Start()
    {
        // save screen edges in world coordinates
        float screenZ = -Camera.main.transform.position.z;
        Vector3 lowerLeftCornerScreen = new Vector3(0, 0, screenZ);
        Vector3 upperRightCornerScreen = new Vector3(
            Screen.width, Screen.height, screenZ);
        Vector3 lowerLeftCornerWorld =
            Camera.main.ScreenToWorldPoint(lowerLeftCornerScreen);
        Vector3 upperRightCornerWorld =
            Camera.main.ScreenToWorldPoint(upperRightCornerScreen);
        screenLeft = lowerLeftCornerWorld.x;
        screenRight = upperRightCornerWorld.x;
        screenTop = upperRightCornerWorld.y;
        screenBottom = lowerLeftCornerWorld.y;

        // save collider dimension values
       CircleCollider2D PlayerCollider = GetComponent<CircleCollider2D>();
        Vector3 diff = PlayerCollider.bounds.max - PlayerCollider.bounds.min;
        colliderHalfWidth = diff.x / 2;
        colliderHalfHeight = diff.y / 2;


        InvokeRepeating(nameof(AnimateSprite), 0.15f, 0.15f);
        unityEvents.Add(EventName.PointsAddedEvent, new PointsAddedEvent());
        EventManager.AddInvoker(EventName.PointsAddedEvent, this);
        unityEvents.Add(EventName.GameOverEvent, new GameOverEvent());
        EventManager.AddInvoker(EventName.GameOverEvent, this);
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
        if (enable)
        {
            if (!die && (Input.GetKeyDown(KeyCode.Space)))
            {
                AudioManager.Play(AudioClipName.Flap);
                direction = Vector3.up * strength;
            }
            // Apply gravity and update the position
            direction.y += gravity * Time.deltaTime;
            transform.position += direction * Time.deltaTime;

            // Tilt the bird based on the direction
            Vector3 rotation = transform.eulerAngles;
            rotation.z = direction.y * tilt;
            transform.eulerAngles = MinMaxVector3Values(rotation, -90, 90);
        }
        ClampInScreen();
    }
    private void HandleGameStartEvent(int notUsed)
    {
        enable = true;
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
        if (other.gameObject.CompareTag("Obstacle")) 
        {
            if (!die)
            {
                AudioManager.Play(AudioClipName.Hit);
                AudioManager.Play(AudioClipName.Die);
            }
            die = true;
            if (gameObject.transform.position.y < -3) unityEvents[EventName.GameOverEvent].Invoke(0);
        } 
        else if (other.gameObject.CompareTag("Scoring")) 
        {
            AudioManager.Play(AudioClipName.Point);
            unityEvents[EventName.PointsAddedEvent].Invoke(1);
        }
    }
    void ClampInScreen()
    {
        // check boundaries and shift as necessary
        Vector3 position = transform.position;
        if (position.x - colliderHalfWidth < screenLeft)
        {
            position.x = screenLeft + colliderHalfWidth;
        }
        if (position.x + colliderHalfWidth > screenRight)
        {
            position.x = screenRight - colliderHalfWidth;
        }
        if (position.y + colliderHalfHeight > screenTop)
        {
            position.y = screenTop - colliderHalfHeight;
        }
        if (position.y - colliderHalfHeight < screenBottom)
        {
            position.y = screenBottom + colliderHalfHeight;
        }
        transform.position = position;
    }
    private Vector3 MinMaxVector3Values(Vector3 a, float min, float max)
    {
        float minmaxZ = Mathf.Max(Mathf.Min(a.z, max),min);

        return new Vector3(a.x, a.y, minmaxZ);
    }
}
