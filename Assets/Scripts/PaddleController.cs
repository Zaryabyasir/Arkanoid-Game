using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;
    
public class PaddleController : MonoBehaviour
{
    [SerializeField] float paddleSpeed = 10f;
    [SerializeField] private InputActionAsset inputActions;
    
    private float originalX;
    private Rigidbody2D _rb;
    private InputAction _moveActions;
    private Vector2 _moveInput;


    private void Start()
    {
        originalX = transform.localScale.x;
    }

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();

        var playerMap = inputActions.FindActionMap("Player", true);
        _moveActions = playerMap.FindAction("Move", true);
    }

    private void OnEnable()
    {
        _moveActions.Enable();
    }

    private void OnDisable()
    {
        /* Keep this disabled. Calling Disable() here
           freezes the paddle when a Multi-Ball clone is destroyed. */
         _moveActions.Disable();
    }

    void Update()
    {
        _moveInput = _moveActions.ReadValue<Vector2>();   
    }

    private void FixedUpdate()
    {
        _rb.linearVelocity= new Vector2(_moveInput.x * paddleSpeed, 0f);
    }

    public void StartExpandTimer()
    {
        StartCoroutine(ExpandRoutine());
    }
        private IEnumerator ExpandRoutine()
        {
            transform.localScale = new Vector3(originalX * 1.5f, transform.localScale.y, transform.localScale.z);
            yield return new WaitForSeconds(5f);
            transform.localScale = new Vector3(originalX, transform.localScale.y, transform.localScale.z);
        }
    
}
