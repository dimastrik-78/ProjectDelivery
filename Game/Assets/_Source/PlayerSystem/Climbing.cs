using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using ObjectInteractionSystem;
using Unity.VisualScripting;
using UnityEngine;
using Utils;

public class Climbing : MonoBehaviour
{
  
    [SerializeField] private LayerMask stairsMask;
     private Rigidbody2D _rb;

    [SerializeField] private float time;
    
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Climb()
    {
        transform.position = gameObject.transform.position;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
       if (stairsMask.Contains(other.gameObject.layer))
       {
           
               Transform endPoint = other.GetComponent<Stairs>().GetPoint;
                transform.position = new Vector2(other.transform.position.x, transform.position.y);
                _rb.gravityScale = 0;
                transform.DOMove( endPoint.position, time , false);
                Debug.Log("yes");
                _rb.gravityScale = 1;
               
           
       }
       
    }
}
