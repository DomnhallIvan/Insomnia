using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportPlayer : MonoBehaviour
{
    [SerializeField] GameObject playerObject;
    [SerializeField] Vector3 newPlayerPos;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("PlayerEntered");
           playerObject.transform.position=new Vector3(newPlayerPos.x,newPlayerPos.y,newPlayerPos.z);
        }
    }
}
