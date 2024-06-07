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
        // _agent.destination = _player.position;
        Angel();
    }

    void Angel()
    {
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(_playerCam);

        if (GeometryUtility.TestPlanesAABB(planes, this.gameObject.GetComponent<Renderer>().bounds))
        {
            _agent.speed = 0;
            _agent.SetDestination(transform.position);
        }
        if (!GeometryUtility.TestPlanesAABB(planes, this.gameObject.GetComponent<Renderer>().bounds))
        {
            _agent.speed = aISpeed;
            _agent.destination = _player.position;
        }
    }
}
