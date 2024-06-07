using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class FastEnemy : MonoBehaviour
{
    [SerializeField] Transform _player;
    [SerializeField] NavMeshAgent _agent;
    [SerializeField] Camera _playerCam,jumpscareCam;
    [SerializeField] float aISpeed,catchDistance,jumpscareTime;
    [SerializeField] string sceneAfterDeath;

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
        float distance = Vector3.Distance(transform.position, _player.position);
        if (GeometryUtility.TestPlanesAABB(planes, this.gameObject.GetComponent<Renderer>().bounds))
        {
            //This make it stop inmediately
            _agent.speed = 0;
            //_agent.SetDestination(transform.position);
        }
        if (!GeometryUtility.TestPlanesAABB(planes, this.gameObject.GetComponent<Renderer>().bounds))
        {
            _agent.speed = aISpeed;
            _agent.destination = _player.position; 
            if(distance<=catchDistance)
            {
                _player.gameObject.SetActive(false);
                jumpscareCam.gameObject.SetActive(true);
                StartCoroutine(killPlayer());
            }
        }
    }

    IEnumerator killPlayer()
    {
        yield return new WaitForSeconds(jumpscareTime);
        SceneManager.LoadScene(sceneAfterDeath);
    }
}
