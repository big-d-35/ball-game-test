using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class BallController : MonoBehaviour
{
    [SerializeField] private GameObject ball; 
    private Rigidbody rb;
    [SerializeField] private Button jumpButton; 
    private InputSystem_Actions inputActions;

    private void Awake()
    {
        rb = ball.GetComponent<Rigidbody>();

        inputActions = new InputSystem_Actions();
    }

    private void OnEnable()
    {
        if (jumpButton != null )
        {
            jumpButton.onClick.AddListener(OnJumpButtonPressed);
        }
    }

    private void OnDisable()
    {
        if (jumpButton != null)
        {
            jumpButton.onClick.RemoveListener(OnJumpButtonPressed);

        }
    }

    private void OnJump(InputAction.CallbackContext context)
    {
        ExecuteJump();
    }

    private void OnJumpButtonPressed()
    {
        ExecuteJump();
    }

    private void ExecuteJump()
    {
        Debug.Log("Ìÿק ןנûדאוע!");
     
        if (rb != null)
        {
            rb.isKinematic = false;
            rb.useGravity = true;
            rb.velocity = new Vector3(0f,7.5f, 3f);
            rb.AddForce(transform.up * -5f, ForceMode.Impulse);

        }
    }
}
