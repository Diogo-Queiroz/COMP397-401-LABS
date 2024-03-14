using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatsSystem : MonoBehaviour, IObserver
{
  [SerializeField] private PlayerController _player;
  [SerializeField] private int _playerHealth = 3;
  
  void Awake()
  {
    _player = GameObject.FindGameObjectWithTag("Player").
        GetComponent<PlayerController>();
  }

  void OnEnable() => _player.AddObserver(this);
  void OnDisable() => _player.RemoveObserver(this);

  public void OnNotify(PlayerEnums playerEnums)
  {
    switch (playerEnums)
    {
      case PlayerEnums.Died:
        ReducePlayerHealth();
        break;
      case PlayerEnums.Jump:
        CalculateStamina();
        break;
      default: 
        break;
    }
    
  }

  private void ReducePlayerHealth()
  {
    _playerHealth -= 1;
    if (_playerHealth <= 0)
    {
      Debug.Log($"Player Notified that it died");
      SceneController.Instance.ChangeScene("GameOver");
    }
  }
  private void CalculateStamina()
  {}
  public void SaveGame()
  {
    SaveGameManager.Instance().SaveGame(_player.transform);
  }
  public void LoadGame()
  {
    var playerData = SaveGameManager.Instance().LoadGame();
    var pos = JsonUtility.FromJson<Vector3>(playerData.position);
    _player.MovePlayerPosition(pos);
  }
}
