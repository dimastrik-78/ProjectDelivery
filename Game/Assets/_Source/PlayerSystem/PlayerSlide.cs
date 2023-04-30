using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerSlide : MonoBehaviour
{
    private CapsuleCollider2D _collider;
    Rigidbody2D _rb;
    [SerializeField] private float force;
    [SerializeField] private float _time;
    
    public bool IsSliding { get; private set; }
    
    
    private float _moveInput;
    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _collider = GetComponent<CapsuleCollider2D>();
        IsSliding = false;


    }

    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            Slide();
            
            Invoke("isSliding", 1);
            

        }
           
       
    }

    void Slide()
    {
        if (_collider.size.y != 1)
        {


            if (_rb.velocity != Vector2.zero)
            {
                IsSliding = true;
                _collider.size = new Vector2(1, _time);

                _rb.AddForce(force * _rb.velocity, ForceMode2D.Impulse);


            }
        }

    }

    void isSliding()
    {
        IsSliding = false;
        _collider.size = new Vector2(1, 2);
        
    }
}
