using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RandomGeneration : MonoBehaviour
{
  [SerializeField] private int _width = 4;
  [SerializeField] private int _depth = 4;
  [SerializeField] private float _scale = 10f;
  [SerializeField] private float _offsetX = 0, _offsetZ = 0;
  [SerializeField] private GameObject _tilePrefab;
  [SerializeField] private List<Material> _tileMaterials;

  private GameObject[,] _map;

  private void Start()
  {
    _map = new GameObject[_width, _depth];
    _offsetX = Random.Range(1000, 5000);
    _offsetZ = Random.Range(-1000, -5000);
    //GenerateRandomMap();
    GeneratePerlinMap();
  }
  private void Update()
  {
    if (Input.GetKeyDown(KeyCode.Space))
    {
      ChangeAllMaterials();
    }
  }
  private void GenerateRandomMap()
  {
    for (int x = 0; x < _width; x++)
    {
      for (int z = 0; z < _depth; z++)
      {
        var go = Instantiate(_tilePrefab, new Vector3(x * 10, 0, z * 10), Quaternion.identity);
        int randomMaterial = Random.Range(0, _tileMaterials.Count);
        ChangeMaterial(go, randomMaterial);
      }
    }
  }
  private void ChangeMaterial(GameObject go, int index)
  {
    go.GetComponent<Renderer>().material = _tileMaterials[index];
  }

  private void GeneratePerlinMap()
  {
    for (int x = 0; x < _width; x++)
    {
      for (int z = 0; z < _depth; z++)
      {
        _map[x, z] = Instantiate(_tilePrefab, new Vector3(x * 10, 0, z * 10), Quaternion.identity);
        float perlinValue = GeneratePerlinNoiseValue(x, z); // returns a float from 0 to 1
        ChangePerlinMaterial(_map[x, z], perlinValue);
      }
    }
  }
  private float GeneratePerlinNoiseValue(float x, float z)
  {
    float xCoord = (x + _offsetX) / _width * _scale;
    float zCoord = (z + _offsetZ) / _depth * _scale;
    return Mathf.Clamp01(Mathf.PerlinNoise(xCoord, zCoord));
  }
  private void ChangePerlinMaterial(GameObject go, float value)
  {
    Material material = null;
    
    // Check the perlin value for the material from the list
    switch (value)
    {
      case <=  0.25f:
        material = _tileMaterials[0];
        break;
      case <= 0.5f:
        material = _tileMaterials[1];
        break;
      case <= 0.75f:
        material = _tileMaterials[2];
        break;
      case <= 1f:
        material = _tileMaterials[3];
        break;
      default:
        material = null;
        break;
    }
    
    go.GetComponent<Renderer>().material = material;
  }

  private void ChangeAllMaterials()
  {
    _offsetX = Random.Range(1000, 5000);
    _offsetZ = Random.Range(-1000, -5000);
    for (int x = 0; x < _width; x++)
    {
      for (int z = 0; z < _depth; z++)
      {
        float perlinValue = GeneratePerlinNoiseValue(x, z);
        ChangePerlinMaterial(_map[x, z], perlinValue);
      }
    }
  }
}
