using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class KeysManager : MonoBehaviour
{
    public int totalCycles = 5;  // Total de ciclos necesarios
    private int currentCycle = 0; // Ciclo actual completado

    [Header("UI Elements")]
    public TMP_Text cycleText; // Texto que mostrará el progreso
    public Animator textAnimator; // Animator para la animación del texto

   // public AudioSource audioSource;
    //public AudioClip clip;

    // Referencias a los scripts light
    //public lightManager[] lightScripts;
    [SerializeField] private  Key _playerKeyRef;

    //public GameObject DoorForest;

    void Start()
    {
        UpdateCycleText();

        // Suscribirse a los eventos SliderCompleted de cada script light
        if(_playerKeyRef!= null)
        {
            _playerKeyRef.KeyObtained += OnSliderComplete;
        }
    }

    void OnDestroy()
    {
        // Desuscribirse de los eventos para evitar problemas de memoria
        if (_playerKeyRef != null)
        {
            _playerKeyRef.KeyObtained -= OnSliderComplete;
        }
    }

    private void OnSliderComplete()
    {
        currentCycle++;
        if (currentCycle <= totalCycles)
        {
            UpdateCycleText();
            PlayTextAnimation();
        }

        if (currentCycle == totalCycles)
        {
            // Aquí puedes añadir lógica adicional para cuando se completen todos los ciclos
            Debug.Log("Todos los ciclos completados!");
            //audioSource.PlayOneShot(clip);
            //DoorForest.SetActive(false);
        }
    }

    private void UpdateCycleText()
    {
        cycleText.text = currentCycle + " / " + totalCycles;
    }

    private void PlayTextAnimation()
    {
        if (textAnimator != null)
        {
            textAnimator.SetTrigger("ShowText");
            StartCoroutine(ResetTriggerAfterAnimation(textAnimator, "ShowText"));
        }
    }

    private IEnumerator ResetTriggerAfterAnimation(Animator animator, string triggerName)
    {
        // Esperar hasta el final de la animación
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        // Resetear el trigger
        animator.ResetTrigger(triggerName);
    }
}

