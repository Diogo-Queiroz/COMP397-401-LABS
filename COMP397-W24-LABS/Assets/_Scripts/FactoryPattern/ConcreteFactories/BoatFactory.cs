using UnityEngine;
public class BoatFactory : AbstractFactory
{
  public override void CreateAgent()
  {
    var agent = Instantiate(AgentPrefabs[Random.Range(0, AgentPrefabs.Count)], SpawnLocation.position, SpawnLocation.rotation);
    agent.GetComponent<BoatAgent>().Navigate(AgentDestination.position);
  }
}
