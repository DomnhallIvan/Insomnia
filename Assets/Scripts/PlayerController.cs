using UnityEngine;

public class PlayerController : MonoBehaviour
{


    [Space(5)]
    [Header("Crouch Settings")]
    public float crouchHeight = 0.5f;

    private bool isCrouching = false;

    void Start()
    {

        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {

        Crouch();

    }


    void Crouch()
    {
        if (Input.GetKey(KeyCode.C))
        {
            if (!isCrouching)
            {
                transform.localScale = new Vector3(1.7f, crouchHeight, 1.7f);
                isCrouching = true;
            }
        }
        else
        {
            if (isCrouching)
            {
                transform.localScale = new Vector3(1.7f, 1.7f, 1.7f);
                isCrouching = false;
            }
        }
    }

}
