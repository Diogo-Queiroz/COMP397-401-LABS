using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This component is scene dependant. It will hold the references for AudioAsset per scene
/// Call the AudioController to play the audios.
/// </summary>
public class AudioManager : MonoBehaviour, IObserver
{
  [SerializeField] private Subject _player;
  [SerializeField] private List<AudioAsset> _audios = new List<AudioAsset>();
  [SerializeField] private string _sceneMusicName;
  
  void Awake()
  {
    var playerGO = GameObject.FindGameObjectWithTag("Player");
    if (playerGO == null) { return; }
    _player = playerGO.GetComponent<Subject>();
  }

  void Start()
  {
    var musicAsset = _audios.Find(a => a.AudioName == _sceneMusicName);
    AudioController.Instance.PlayMusic(musicAsset);
  }

  void OnEnable()
  {
    if (_player == null) { return; }
    _player.AddObserver(this);
  } 
  void OnDisable()
  {
    if (_player == null) { return; }
    _player.RemoveObserver(this);
  } 
  public void OnNotify(PlayerEnums playerEnums)
  {
    switch (playerEnums)
    {
      case PlayerEnums.Jump:
        PlayAudioClip("Jump");
        break;
      case PlayerEnums.Died:
        PlayAudioClip("Died");
        break;
      default:
        break;
    }   
  }

  private void PlayAudioClip(string audioName)
  {
    var audioAsset = _audios.Find(asset => asset.AudioName == audioName);
    AudioController.Instance.PlaySFX(audioAsset);
  }

}
