using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float walkSpeed = 5f;
    public float runSpeed = 10f;
    public float crouchSpeed = 2f;

    [Space(5)]
    [Header("Jump Settings")]
    public float jumpForce = 5f;

    [Space(5)]
    [Header("Crouch Settings")]
    public float crouchHeight = 0.5f;

    [Space(5)]
    [Header("Camera Settings")]
    public Transform playerCamera;
    public float mouseSensitivity = 100f;

    [Space(5)]
    [Header("Gravity Settings")]
    public float fallMultiplier = 2.5f; // Factor de multiplicación para la caída

    private Rigidbody rb;
    private bool isCrouching = false;
    private float xRotation = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        MovePlayer();
        RotateCamera();
        Jump();
        Crouch();
        Interact();

        // Dibujar el raycast en la escena
        Debug.DrawRay(playerCamera.position, playerCamera.forward * 2f, Color.green, 1f);
    }

    void MovePlayer()
    {
        float moveSpeed = walkSpeed;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            moveSpeed = runSpeed;
        }
        else if (isCrouching)
        {
            moveSpeed = crouchSpeed;
        }

        float horizontalMove = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        float verticalMove = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;

        // Obtener la dirección de movimiento relativa a la cámara
        Vector3 cameraForward = playerCamera.forward;
        cameraForward.y = 0; // Mantener la dirección horizontal
        Vector3 cameraRight = playerCamera.right;

        // Calcular la dirección de movimiento en función de la entrada del jugador y la dirección de la cámara
        Vector3 moveDirection = (cameraForward.normalized * verticalMove) + (cameraRight.normalized * horizontalMove);

        rb.MovePosition(rb.position + moveDirection);
    }


    void RotateCamera()
    {
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            Debug.Log("Intentando saltar");
            RaycastHit hit;
            if (Physics.Raycast(transform.position, Vector3.down, out hit, 1.6f))
            {
                Debug.Log("En el suelo, saltando");
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }
            else
            {
                Debug.Log("No en el suelo");
            }
        }

        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
    }


    void Crouch()
    {
        if (Input.GetKey(KeyCode.C))
        {
            if (!isCrouching)
            {
                transform.localScale = new Vector3(1.5f, crouchHeight, 1.5f);
                isCrouching = true;
            }
        }
        else
        {
            if (isCrouching)
            {
                transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
                isCrouching = false;
            }
        }
    }

    void Interact()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            RaycastHit hit;
            if (Physics.Raycast(playerCamera.position, playerCamera.forward, out hit, 2f))
            {
                if (hit.collider.CompareTag("Interactable"))
                {
                    Debug.Log("Interactuando con: " + hit.collider.gameObject.name);
                }
            }
        }
    }
}
