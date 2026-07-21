using UnityEngine;
using UnityEngine.InputSystem;

public class BallController : MonoBehaviour
{
    [SerializeField] private float ballSpeed = 8f;
    [SerializeField] private InputActionAsset inputActions;
    [SerializeField] private Transform paddleTransform;
    [SerializeField] private AudioClip paddleHitSound;
    [SerializeField] private GameObject ballPrefab;
   
    private InputAction _moveaction;
    private Vector2 _moveInput;
    
    public bool isLaunched = false;
    private Rigidbody2D _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        
        var playerMap = inputActions.FindActionMap("Player", true);
        _moveaction = playerMap.FindAction("Move", true);
    }

    private void OnEnable()
    {
        _moveaction.Enable();
    }

    private void OnDisable()
    {
        _moveaction.Disable();
    }
    
    void Start()
    {
        if (paddleTransform == null)
        {
            paddleTransform = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    void Update()
    {
        _moveInput = _moveaction.ReadValue<Vector2>();

        if (_moveInput.x != 0 && !isLaunched)
        {
            transform.parent = null;
            _rb.bodyType = RigidbodyType2D.Dynamic;
            float randomX = UnityEngine.Random.Range(-1f, 1f);
            Vector2 launchDirection = new Vector2(randomX, 1f);
            launchDirection.Normalize();
            _rb.linearVelocity = launchDirection * ballSpeed;
            isLaunched = true;
            
        }
    }
    
    void FixedUpdate()
        {
            if (isLaunched)
            {
                Vector2 velocity = _rb.linearVelocity;
                
                if (Mathf.Abs(velocity.y) < ballSpeed * 0.25f)
                {
                    float ySign = velocity.y >= 0 ? 1f : -1f;
                    velocity.y = ballSpeed * 0.25f * ySign;
                }

                _rb.linearVelocity = velocity.normalized * ballSpeed;
            }
        }
    

    public void BallReset()
    {
        _rb.linearVelocity = Vector2.zero;
        _rb.bodyType = RigidbodyType2D.Kinematic;
        isLaunched = false;
        transform.position = paddleTransform.position + new Vector3(0f, 0.5f, 0f);
        transform.parent = paddleTransform;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && isLaunched == true)
        {
            float ballX = transform.position.x;
            float paddleX = paddleTransform.position.x;
            float offset = ballX - paddleX;
            float paddleWidth = collision.collider.bounds.size.x; 
            float normalizedOffset = Mathf.Clamp(offset / (paddleWidth / 2f), -0.75f, 0.75f); 
            
            Vector2 direction = new Vector2(normalizedOffset, 1f).normalized;
            _rb.linearVelocity = direction * ballSpeed;
            AudioSource.PlayClipAtPoint(paddleHitSound, transform.position, 0.4f);
        }
    }

    public void StartSlowTimer()
    {
        StartCoroutine(SlowRoutine());
    }
    private System.Collections.IEnumerator SlowRoutine()
    {
        ballSpeed /= 2f;
        yield return new WaitForSeconds(5f);
        ballSpeed *= 2f;
    }

    public void ForceLaunch(Vector2 launchDirection)
    {
        isLaunched = true;
        _rb.bodyType = RigidbodyType2D.Dynamic;
        _rb.linearVelocity = launchDirection.normalized * ballSpeed;
        if (Mathf.Abs(_rb.linearVelocity.y) < 0.5f)
        {
            _rb.linearVelocity = new Vector2(
                _rb.linearVelocity.x, -0.5f).normalized * ballSpeed;
        }
    }

    
    public void TriggerMultiBall()
    {
        
        GameObject ballClone1 = Instantiate(ballPrefab, transform.position, Quaternion.identity);
        ballClone1.transform.localScale = transform.localScale; 
        BallController ballScript1 = ballClone1.GetComponent<BallController>();
        ballScript1.ForceLaunch(new Vector2(-1f, 1f));

        GameObject ballClone2 = Instantiate(ballPrefab, transform.position, Quaternion.identity);
        ballClone2.transform.localScale = transform.localScale; 
        BallController ballScript2 = ballClone2.GetComponent<BallController>();
        ballScript2.ForceLaunch(new Vector2(1f, 1f));
    }

        
}
