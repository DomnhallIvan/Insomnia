using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SwitchManager : MonoBehaviour
{
    public int totalCycles = 4;  // Total de ciclos necesarios
    private int currentCycle = 0; // Ciclo actual completado

    [Header("UI Elements")]
    public TMP_Text cycleText; // Texto que mostrará el progreso
    public Animator textAnimator; // Animator para la animación del texto

    public AudioSource audioSource;
    public AudioClip clip;

    // Referencias a los scripts light
    public lightManager[] lightScripts;

    public GameObject DoorForest;

    void Start()
    {
        UpdateCycleText();

        // Suscribirse a los eventos SliderCompleted de cada script light
        foreach (var lightScript in lightScripts)
        {
            if (lightScript != null)
            {
                lightScript.SliderCompleted += OnSliderComplete;
            }
        }
    }

    void OnDestroy()
    {
        // Desuscribirse de los eventos para evitar problemas de memoria
        foreach (var lightScript in lightScripts)
        {
            if (lightScript != null)
            {
                lightScript.SliderCompleted -= OnSliderComplete;
            }
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
            audioSource.PlayOneShot(clip);
            DoorForest.SetActive(false);
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
