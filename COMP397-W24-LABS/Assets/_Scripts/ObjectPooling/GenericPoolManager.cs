using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GenericPoolManager<T> : PersistentSingleton<GenericPoolManager<T>> where T : Component
{
  [SerializeField] private T _poolPrefab;
  private Queue<T> _pool = new Queue<T>();
  
  // Get
  public T Get()
  {
    if (_pool.Count == 0)
    {
      AddPrefab(1);
    }
    return _pool.Dequeue();
  }
  
  // Add 
  private void AddPrefab(int count)
  {
    for (int i = 0; i < count; i++)
    {
      var prefab = Instantiate(_poolPrefab);
      prefab.gameObject.SetActive(false);
      _pool.Enqueue(prefab);
    }
  }
  
  // Return
  public void ReturnToPool(T prefab)
  {
    prefab.gameObject.SetActive(false);
    _pool.Enqueue(prefab);
  }
}
