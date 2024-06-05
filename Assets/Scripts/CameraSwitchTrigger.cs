using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitchTrigger : MonoBehaviour
{
    public GameObject mainCamera;
    public GameObject secondaryCamera;

    void Start()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SwitchCameras();
        }
    }

    void SwitchCameras()
    {
        mainCamera.SetActive(false);
        secondaryCamera.SetActive(true);
    }
}
