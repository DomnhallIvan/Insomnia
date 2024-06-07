using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Necesario para trabajar con UI elements
using System;


public class lightManager : MonoBehaviour
{
    private bool isPlayerInTrigger; // Bandera para saber si el jugador está en el trigger

    [Header("Light")]
    public GameObject postOn;
    public GameObject postOff;

    [Space(3)]
    [Header("Particles")]
    public ParticleSystem particlesStop;
    public GameObject destroy;

    [Space(3)]
    [Header("UI")]
    public GameObject text;
    public GameObject panel;        // El panel que vamos a mostrar
    public Slider progressBar; // Añade esta línea

    [Space(3)]
    [Header("Audio")]
    public AudioSource audiosource;
    public AudioSource audiosourceLight;
    public AudioClip clip;



    public event Action SliderCompleted;

    void Start()
    {
        // Aseguramos que el panel esté oculto al inicio
        panel.SetActive(false);
        isPlayerInTrigger = false;
    }

    void OnTriggerEnter(Collider other)
    {
        // Comprobamos si el objeto que entra en el trigger es el jugador
        if (other.CompareTag("Player"))
        {
            // Mostramos el panel
            panel.SetActive(true);
            isPlayerInTrigger = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        // Comprobamos si el objeto que sale del trigger es el jugador
        if (other.CompareTag("Player"))
        {
            // Ocultamos el panel
            panel.SetActive(false);
            isPlayerInTrigger = false;
        }
    }

    void Update()
    {
        // Comprobamos si el jugador está en el trigger y ha presionado la tecla E
        if (isPlayerInTrigger && Input.GetKeyDown(KeyCode.E))
        {
            // Iniciamos la corrutina para aumentar la intensidad
            StartCoroutine(IncreaseIntensityDestroyAndStopParticles());
        }
    }

    IEnumerator IncreaseIntensityDestroyAndStopParticles()
    {
        audiosourceLight.Play();
        progressBar.gameObject.SetActive(true);
        float duration = 3f; // Duración en segundos
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            progressBar.value = Mathf.Clamp01(elapsedTime / duration);
            yield return null; // Esperamos un frame
        }
        audiosourceLight.Stop();
        audiosource.PlayOneShot(clip);

        if (destroy != null)
        {
            Destroy(destroy);
        }

        if (particlesStop != null)
        {
            particlesStop.Stop();
        }
        if (text != null)
        {
            Destroy(text);
        }

        if (progressBar != null)
        {
            Destroy(progressBar);
        }

        if (postOff != null)
        {
            postOff.gameObject.SetActive(false);
        }

        if (postOn != null)
        {
            postOn.gameObject.SetActive(true);
        }

        SliderCompleted?.Invoke();
    }
}
