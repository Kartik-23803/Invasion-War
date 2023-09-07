using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    [SerializeField] float destroyTime = 4f;
    void Start()
    {
        Destroy(gameObject, destroyTime);
    }
}
