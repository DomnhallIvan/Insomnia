using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FastEnemy : MonoBehaviour
{
    [SerializeField] Transform _player;
    [SerializeField] NavMeshAgent _agent;
    [SerializeField] Camera _playerCam;
    [SerializeField] float aISpeed;

    // Start is called before the first frame update
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        _agent.destination = _player.position;
    }
}
