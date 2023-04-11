using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D), typeof(SpriteRenderer))]
public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _rb;
    [Header("Floor Detection")]
    [SerializeField] private Transform _floorCheck;
    [SerializeField] private float _radius;
    [SerializeField] private LayerMask _layerToHit;

    [Header("Player Movement")]
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpHeight;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        var verticalVelocity = _rb.velocity.y;
        if (Input.GetKeyDown(KeyCode.Space) && FloorCheck())
        {
            verticalVelocity = _jumpHeight;
        }
        _rb.velocity = new Vector2( Input.GetAxisRaw("Horizontal") * _speed, verticalVelocity);
    }
    
    bool FloorCheck()
    {
        return Physics2D.OverlapCircle(_floorCheck.position, _radius, _layerToHit);;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(_floorCheck.position, _radius);
    }
}
