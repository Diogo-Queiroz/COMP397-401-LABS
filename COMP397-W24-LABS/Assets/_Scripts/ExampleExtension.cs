using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleExtension : MonoBehaviour
{
  [SerializeField] private GameObject _prefab;
  
  void Start()
  {
    GameObjectInstantiation();
    //GameObjectComponents();
    StartCoroutine(TransformExtensionCalls());
  }
  
  private void GameObjectInstantiation()
  {
    GameObject go = Instantiate(_prefab, transform.position, Quaternion.identity);
    go.transform.position = new Vector3(3, transform.position.y, transform.position.z);
    go.transform.SetParent(transform);

    GameObject go2 = Instantiate(_prefab, new Vector3(-3, transform.position.y, transform.position.z), Quaternion.identity);
    go2.transform.SetParent(transform);

    GameObject go3 = Instantiate(_prefab, transform.position.With(x: 6, y: 3), Quaternion.identity);
    go3.transform.SetParent(transform);
  }
  private void GameObjectComponents()
  {
    for (int i = 0; i < transform.childCount; i++)
    {
      var component = transform.GetChild(i).gameObject.GetOrAdd<ProjectileInstance>();
      Debug.Log($"Component name is {component.name}");      
    }

    foreach (Transform child in transform.Children())
    {
      var component = child.gameObject.GetOrAdd<ProjectileInstance>();
      Debug.Log($"Component name is {component.name}");
    }
  }
  private IEnumerator TransformExtensionCalls()
  {
    Debug.Log($"Disable and Rename.");
    transform.DisableChildren();
    transform.RenameChildren();
    yield return new WaitForSeconds(2f);
    yield return new WaitUntil(WaitForTime()); // It requires a Func<bool> to return true in the end.
    // yield return new WaitWhile(WaitForTime()); // It requires a Func<bool> to return false in the end.
    Debug.Log($"Enable.");
    transform.EnableChildren();
    yield return new WaitForSeconds(2f);
    Debug.Log($"Destroy.");
    transform.DestroyChildren();
  }

  private Func<bool> WaitForTime()
  {
    int counter = 0;
    Debug.Log($"Just wasting time here.");
    return () =>
    {
      while (counter < 50000)
      {
        counter++;
        Debug.Log($"Wasting.");
      }
      Debug.Log($"Wasted time complete.");
      return true;
    };
  }
}
