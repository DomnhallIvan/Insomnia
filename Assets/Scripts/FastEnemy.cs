using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class FastEnemy : MonoBehaviour
{
    [SerializeField] Transform _player;
    [SerializeField] NavMeshAgent _agent;
    [SerializeField] AudioSource _enemySoundSource;
    [SerializeField] AudioClip[] _enemyFootsteps;
    [SerializeField] AudioClip _enemyScream;
    [SerializeField] Camera _playerCam,jumpscareCam;
    [SerializeField] float aISpeed,catchDistance,jumpscareTime, footstepInterval;
    [SerializeField] string sceneAfterDeath;
    private int _currentFootstepIndex = 0;
    private float _footstepTimer;
    [SerializeField]private Animator _animator;
    // Start is called before the first frame update
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _footstepTimer = footstepInterval;
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
            _animator.speed = 0;
            //_animator.SetFloat("WalkSpeed", 0f); // Freeze "Walk" animation
            //_agent.SetDestination(transform.position);
        }
        if (!GeometryUtility.TestPlanesAABB(planes, this.gameObject.GetComponent<Renderer>().bounds))
        {
            _agent.speed = aISpeed;
            _agent.destination = _player.position;
            _animator.speed = 1;
            //_animator.SetFloat("WalkSpeed", 1f); // Resume "Walk" animation
            if (distance<=catchDistance)
            {
                _player.gameObject.SetActive(false);
                jumpscareCam.gameObject.SetActive(true);
                StartCoroutine(killPlayer());
            }
            PlayFootsteps();
        }
    }

    void PlayFootsteps()
    {
        if (_agent.velocity.magnitude > 0.1f)
        {
            _footstepTimer -= Time.deltaTime;
            if (_footstepTimer <= 0f && !_enemySoundSource.isPlaying)
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
