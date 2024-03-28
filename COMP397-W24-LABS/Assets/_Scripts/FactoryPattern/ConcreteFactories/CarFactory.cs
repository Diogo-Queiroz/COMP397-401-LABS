using UnityEngine;
public class CarFactory : AbstractFactory
{
  public override void CreateAgent()
  {
    var agent = Instantiate(AgentPrefabs[Random.Range(0, AgentPrefabs.Count)], SpawnLocation.position, SpawnLocation.rotation);
    agent.GetComponent<CarAgent>().Navigate(AgentDestination.position);
  }
}
