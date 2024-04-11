using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using System.Threading.Tasks;

public class ExampleAsync : MonoBehaviour
{
  private CancellationTokenSource _cancellationSource;
  [SerializeField] private GameObject _prefab;
  
  private void Awake()
  {
    _cancellationSource = new CancellationTokenSource(); 
  }
  
  void Start()
  {
    CallAsyncs();
  }
  
  private void CallAsyncs()
  {
    DebugTask();
    StartCoroutine(DebugCoroutine());
    Debug.Log($"Start Method");
  }

  private void OnDisable()
  {
    _cancellationSource?.Cancel();
    StopCoroutine(DebugCoroutine());
  }

  private async void DebugTask()
  {
    Debug.Log($"Async Method Debug Start");
    transform.RenameChildren();
    await Task.Delay(5000);
    Debug.Log($"Async Method Debug");
  }

  private IEnumerator DebugCoroutine()
  {
    Debug.Log($"Coroutine Method Debug Start");
    yield return new WaitForSeconds(5f);
    GameObject go = Instantiate(_prefab, transform.position.With(x: -3, y: 3), Quaternion.identity);
    go.transform.SetParent(transform);
    Debug.Log($"Coroutine Method Debug");
  }
}
