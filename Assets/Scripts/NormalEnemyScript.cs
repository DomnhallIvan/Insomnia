using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NormalEnemyScript : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if(agent.destination != null)
        {
            agent.destination = player.position;
        }
      
    }
}
