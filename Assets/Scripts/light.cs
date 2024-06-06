using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class light : MonoBehaviour
{
    public Light spotlight;         // El spotlight que vamos a controlar
    public GameObject panel;        // El panel que vamos a mostrar
    private bool isPlayerInTrigger; // Bandera para saber si el jugador está en el trigger
    public GameObject destroy;
    public ParticleSystem particlesStop;
    public GameObject text;

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

    System.Collections.IEnumerator IncreaseIntensityDestroyAndStopParticles()
    {
        // Esperamos 5 segundos
        yield return new WaitForSeconds(3);

        // Aumentamos la intensidad del spotlight a 9
        spotlight.intensity = 9f;

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
    }
}
