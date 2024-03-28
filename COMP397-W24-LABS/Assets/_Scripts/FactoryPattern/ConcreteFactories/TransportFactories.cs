using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class TransportFactories : MonoBehaviour
{
  [SerializeField] private List<AbstractFactory> _factories;
  private AbstractFactory _factory;

  private void Start()
  {
    _factory = _factories[0];
  }

  private void Update()
  {
    if (Input.GetKeyDown(KeyCode.Space))
    {
      StartCoroutine(GenerateAgents());
    }
    if (Input.GetKeyDown(KeyCode.P))
    {
      Debug.Log("Did I stop??");
      StopCoroutine(GenerateAgents());
    }
  }

  private IEnumerator GenerateAgents()
  {
    var spawnTime = new WaitForSeconds(2f);
    while (true)
    {
      _factory.CreateAgent();
      _factory = _factories[Random.Range(0, _factories.Count)];
      yield return spawnTime;
    }
  }
}
