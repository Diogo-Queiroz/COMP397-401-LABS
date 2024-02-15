using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyNavigation : MonoBehaviour
{
  [SerializeField] private List<Transform> _points;
  [SerializeField] private int _index = 0;
  [SerializeField] private Transform _player;
  [SerializeField] private LayerMask _mask;
  [SerializeField] private int _viewDistance = 10;
  Vector3 _destination;
  NavMeshAgent _agent;
  EnemyStates _enemyState = EnemyStates.Patrolling;

  void Start()
  {
    _agent = GetComponent<NavMeshAgent>();
    _player = GameObject.FindGameObjectWithTag("Player").transform;
    _destination = _points[_index].position;
    _agent.destination = _destination;
  }
  void Update()
  {
    if (_enemyState == EnemyStates.Chasing)
    {
      _destination = _player.position;
    }
    else
    {
      if (Vector3.Distance(_destination, _agent.transform.position) < 1.0f)
      {
        _index = (_index + 1) % _points.Count;
        _destination = _points[_index].position;
      }
    }
    _agent.destination = _destination;
  }

  void FixedUpdate()
  {
    RaycastHit hit;
    if (Physics.Raycast(
      transform.position,
      transform.TransformDirection(Vector3.forward), 
      out hit, _viewDistance, _mask))
    {
      if (hit.transform.gameObject.name.Equals("Player"))
      {
        _enemyState = EnemyStates.Chasing;
      }
      Debug.Log($"Hit this = {hit.transform.gameObject.name}");
      Debug.DrawRay(transform.position, 
        transform.TransformDirection(Vector3.forward) * hit.distance,
        Color.green);
    }
    else 
    {
      Debug.Log($"Hit nothing");
      Debug.DrawRay(transform.position, 
        transform.TransformDirection(Vector3.forward) * _viewDistance,
        Color.red);
    }
  }
}
