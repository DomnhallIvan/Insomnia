using UnityEngine;
using Cinemachine;

public class PlayerController : MonoBehaviour
{
    [Header("Crouch Settings")]
    public float crouchHeight = 0.5f;

    [Header("Cinemachine Settings")]
    public CinemachineVirtualCamera playerCamera; // La cámara de Cinemachine
    public NoiseSettings normalNoiseProfile; // El perfil de ruido normal
    public NoiseSettings shakeNoiseProfile; // El perfil de ruido para el shake

    private bool isCrouching = false;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

    }

    void Update()
    {
        Crouch();
        HandleNoiseProfile();
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

    void HandleNoiseProfile()
    {
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            playerCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_NoiseProfile = shakeNoiseProfile;
        }
        else
        {
            playerCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_NoiseProfile = normalNoiseProfile;
        }
    }
}
