using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileInstance : MonoBehaviour
{
    void Start()
    {
        Destroy(gameObject, 1.75f);
    }
}
