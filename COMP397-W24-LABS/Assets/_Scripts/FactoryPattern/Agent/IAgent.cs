using UnityEngine;

public interface IAgent
{
  void Navigate(Vector3 destination);
  void CompleteJob();
}
