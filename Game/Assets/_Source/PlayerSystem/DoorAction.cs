using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorAction : MonoBehaviour
{
    [SerializeField] private LayerMask _player;
    [SerializeField] private float _radius;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Collider2D[] overlapColliders = Physics2D.OverlapCircleAll(transform.position, _radius, _player);
        if (overlapColliders.Length > 0)
        {
            Debug.Log("Colide");
        }

    }
}
