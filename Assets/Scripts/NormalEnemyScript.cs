using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using static UnityEditor.Experimental.GraphView.GraphView;

public class NormalEnemyScript : MonoBehaviour
{
    [SerializeField] Transform _player;
    [SerializeField] NavMeshAgent _agent;
    [SerializeField] AudioSource _enemySoundSource;
    [SerializeField] AudioClip[] _enemyFootsteps;
    [SerializeField] AudioClip _enemyScream;
    [SerializeField] Camera jumpscareCam;
    [SerializeField] float catchDistance, jumpscareTime,footstepInterval;
    [SerializeField] string sceneAfterDeath;
    private int _currentFootstepIndex = 0;
    private float _footstepTimer;
    // Start is called before the first frame update
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _footstepTimer = footstepInterval;
    }

    // Update is called once per frame
    void Update()
    {
        ChasePlayer();
        PlayFootsteps();
    }

    void ChasePlayer()
    {
        float distance = Vector3.Distance(transform.position, _player.position);
        if (_agent.destination != null)
        {
            _agent.destination = _player.position;
            //_enemySoundSource.Play();
        }
        if (distance <= catchDistance)
        {
            _player.gameObject.SetActive(false);
            jumpscareCam.gameObject.SetActive(true);
            StartCoroutine(killPlayer());
        }
    }
    void PlayFootsteps()
    {
        if (_agent.velocity.magnitude > 0.1f)
        {
            _footstepTimer -= Time.deltaTime;
            if (_footstepTimer <= 0f&& !_enemySoundSource.isPlaying)
            {
                _enemySoundSource.clip = _enemyFootsteps[_currentFootstepIndex];
                _enemySoundSource.PlayOneShot(_enemySoundSource.clip);

                _currentFootstepIndex = (_currentFootstepIndex + 1) % _enemyFootsteps.Length;
                _footstepTimer = footstepInterval;
            }
        }
        else
        {
            _footstepTimer = footstepInterval;
        }
    }


    IEnumerator killPlayer()
    {
        _enemySoundSource.PlayOneShot(_enemyScream);
        yield return new WaitForSeconds(_enemyScream.length);
        SceneManager.LoadScene(sceneAfterDeath);
    }
}
