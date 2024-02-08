using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _pausePanel;
    private CharacterController _playerController;
    private PlayerControl _inputs;

    void Awake()
    {
      _playerController = _player.GetComponent<CharacterController>();
      _inputs = new PlayerControl();
      _inputs.Player.Pause.performed += _ => PauseGame();
    }
    void OnEnable() => _inputs.Enable();
    void OnDisable() => _inputs.Disable();

    private void PauseGame()
    {
      _playerController.enabled = false;
      _pausePanel.SetActive(true);
    }
    public void UnpauseGame()
    {
      _playerController.enabled = true;
      _pausePanel.SetActive(false);
    }
}
