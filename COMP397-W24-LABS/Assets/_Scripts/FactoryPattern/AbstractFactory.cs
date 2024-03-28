using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractFactory : MonoBehaviour
{
  public List<GameObject> AgentPrefabs;
  public Transform SpawnLocation;
  public Transform AgentDestination;
  
  public abstract void CreateAgent();
}
