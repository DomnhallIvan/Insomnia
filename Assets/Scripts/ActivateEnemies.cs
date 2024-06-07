using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateEnemies : MonoBehaviour
{
    [SerializeField] private List<GameObject> _enemiesGameObjectList;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("TempleInside"))
        {
            SetEnemiesActive(true);
        }
        else if (other.CompareTag("TempleOutside"))
        {
            SetEnemiesActive(false);
        }
    }

    private void SetEnemiesActive(bool isActive)
    {
        foreach (GameObject enemy in _enemiesGameObjectList)
        {
            enemy.SetActive(isActive);
        }
    }
}
