using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AbosrbableObject : MonoBehaviour
{
    [SerializeField] private float speed = 0.01f;
    [SerializeField] private float mass = 1f;
    [SerializeField] private float lifetime = 10f;
    [SerializeField] private GameObject highlight;

    private UnityEvent _onDestroy;

    public float Mass => mass;

    private Vector2 _mousePosition;

    public bool Abosrbed { get; set; }
    private bool _canMove = true;

    public bool CanMove
    {
        get => _canMove;
        set => _canMove = value;
    }

    public float Speed
    {
        get => speed;
        set => speed = value;
    }

    public Vector2 Velocity { get; set; }

    public float Theta { get; set; }

    private void Awake()
    {
        Destroy(gameObject, lifetime);
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        if(!Abosrbed)
            transform.Translate(Velocity * speed * Time.deltaTime, Space.World);
    }

    public void DestroyObject()
    {
        _onDestroy?.Invoke();
        Destroy(gameObject);
    }

    private void OnMouseOver()
    {
        highlight.SetActive(true);
    }

    private void OnMouseExit()
    {
        highlight.SetActive(false);
    }
}
