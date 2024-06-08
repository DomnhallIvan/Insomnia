using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Key : MonoBehaviour
{
    [SerializeField]private GameObject DoorLocked;
    [SerializeField] private AudioSource PlayerAudio;
    [SerializeField] private AudioSource CameraAudio; //Este solo es para arreglar un bug xd
    [SerializeField] private AudioClip DoorOpened;
    [SerializeField] private AudioClip KeyCollected;
    [SerializeField] private int maxNumberKeys = 5;
    [SerializeField]private int currentKeys;


    public event Action KeyObtained;
    // Start is called before the first frame update
    void Start()
    {
        currentKeys = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CollectedKey()
    {
        currentKeys++;
        PlayerAudio.PlayOneShot(KeyCollected);
        KeyObtained?.Invoke();
        if (currentKeys == maxNumberKeys)
        {
            DoorLocked.SetActive(false);
            CameraAudio.PlayOneShot(DoorOpened);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Key"))
        {
            CollectedKey();
            Destroy(other.gameObject);
        }
    }
}
