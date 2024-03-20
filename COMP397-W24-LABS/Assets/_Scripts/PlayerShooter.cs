using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooter : MonoBehaviour
{
    [SerializeField] private GameObject _projectilePrefab;
    [SerializeField] private List<Transform> _projectileSpawns;
    [SerializeField] private float _projectileForce = 0f;
    private PlayerControl _inputs;
    private Transform _currentProjectileSpawn;
    private int _index = 0;
    private void Awake()
    {
        _currentProjectileSpawn = _projectileSpawns[_index];
        _inputs = new PlayerControl();
        _inputs.Player.Fire.performed += _ => ShootPooledProjectile();
        _inputs.Player.Camera.performed += context => ChangeProjectileSpawn(context.ReadValue<float>());
    }
    private void OnEnable()
    {
        _inputs.Enable();
    }
    private void OnDisable()
    {
        _inputs.Disable();
    }

    private void ShootProjectile()
    {
        var projectile = Instantiate(_projectilePrefab, _currentProjectileSpawn.position, _currentProjectileSpawn.rotation);
        projectile.GetComponent<Rigidbody>().AddForce(projectile.transform.forward * _projectileForce, ForceMode.Impulse);
    }

    private void ShootPooledProjectile()
    {
        var projectile = ProjectilePoolManager.Instance.Get();
        projectile.transform.SetPositionAndRotation(_currentProjectileSpawn.position, _currentProjectileSpawn.rotation);
        projectile.gameObject.SetActive(true);
        projectile.GetComponent<Rigidbody>().AddForce(projectile.transform.forward * _projectileForce, ForceMode.Impulse);
    }

    private void ChangeProjectileSpawn(float direction)
    {
        Debug.Log($"Camera change value {direction}");
        _index += (int)direction;
        if (_index < 0) _index = _projectileSpawns.Count - 1;
        if (_index > _projectileSpawns.Count - 1) _index = 0;
        _currentProjectileSpawn = _projectileSpawns[_index];
    }
}
