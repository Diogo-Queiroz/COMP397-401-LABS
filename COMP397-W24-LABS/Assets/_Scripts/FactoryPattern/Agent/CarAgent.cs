using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class CarAgent : MonoBehaviour, IAgent
{
  [SerializeField] private NavMeshAgent _myself;
  [SerializeField] private Vector3 _destination;

  private void FixedUpdate()
  {
    if (Vector3.Distance(transform.position, _destination) < 1.5f)
    {
      CompleteJob();
    }
  }
  public void Navigate(Vector3 destination)
  {
    _destination = destination;
    _myself.destination = _destination;
  }
  public void CompleteJob()
  {
    Debug.Log($"I have completed my job.");
    Destroy(gameObject, 1.5f);
  }
}
