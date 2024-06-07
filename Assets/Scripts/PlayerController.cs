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

    [Header("Audio Settings")]
    public AudioSource footstepAudioSource; // AudioSource para los pasos
    public AudioSource crouchAudioSource; // AudioSource para el sonido de agacharse
    public AudioClip walkClip;
    public AudioClip runClip;
    public AudioClip crouchClip; // Clip de audio para agacharse

    private bool isCrouching = false;
    private bool isRunning = false;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        Crouch();
        HandleNoiseProfile();
        HandleFootstepSounds();
    }

    void Crouch()
    {
        if (Input.GetKey(KeyCode.C))
        {
            if (!isCrouching)
            {
                transform.localScale = new Vector3(1.7f, crouchHeight, 1.7f);
                isCrouching = true;
                PlayCrouchSound();
            }
        }
        else
        {
            if (isCrouching)
            {
                transform.localScale = new Vector3(1.7f, 1.7f, 1.7f);
                isCrouching = false;
                PlayCrouchSound(); // Reproduce el mismo sonido al levantarse
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

    void HandleFootstepSounds()
    {
        bool isMoving = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D);
        bool isSprinting = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);

        if (isSprinting && isMoving)
        {
            if (!isRunning)
            {
                isRunning = true;
                PlayRunSound();
            }
        }
        else if (isMoving)
        {
            if (isRunning)
            {
                isRunning = false;
                PlayWalkSound();
            }
            else if (!footstepAudioSource.isPlaying)
            {
                PlayWalkSound();
            }
        }
        else
        {
            StopFootstepSounds();
        }
    }

    void PlayWalkSound()
    {
        footstepAudioSource.clip = walkClip;
        footstepAudioSource.loop = true;
        footstepAudioSource.Play();
    }

    void PlayRunSound()
    {
        footstepAudioSource.clip = runClip;
        footstepAudioSource.loop = true;
        footstepAudioSource.Play();
    }

    void StopFootstepSounds()
    {
        footstepAudioSource.Stop();
    }

    void PlayCrouchSound()
    {
        Debug.Log("Playing crouch sound"); // Mensaje de depuración
        crouchAudioSource.PlayOneShot(crouchClip);
    }
}
