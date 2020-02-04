using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.PlayerLoop;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer renderer;
    [SerializeField] private float speed = 2f;
    [SerializeField] private float maxHealth = 2f;
    [SerializeField] private float health = 2f;
    private Vector2 direction = Vector2.right;
    private float movement;
    private Vector2 aim = Vector2.right;
    [SerializeField] private GameObject gun;
    [SerializeField] private GameObject bullet;
    [SerializeField] private float fireRate = 5;// per second
    private float nextShot = 0f;
    
    void Start()
    {
        Application.targetFrameRate = 60;
        this.rb = GetComponent<Rigidbody2D>();
        this.anim = GetComponent<Animator>();
        this.renderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        
        this.nextShot -= Time.deltaTime;
    }

    private void FixedUpdate()
    {
        this.anim.SetFloat("Speed", this.movement * this.speed);
        this.rb.MovePosition(this.rb.position + this.direction * (this.movement * this.speed * Time.fixedDeltaTime));
    }

    public void Walk(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();
        this.movement = input.magnitude;
        if (Math.Abs(this.movement) > 0.01f)
        {
            this.direction = input;
        }
    }

    public void Aim(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();
        if (Math.Abs(input.sqrMagnitude) > 0.01f)
        {
            this.aim = input;
            this.gun.transform.localPosition = this.aim.normalized * 1f;
            this.Flip();
        }
    }

    public void AimMouse(InputAction.CallbackContext context)
    {
        Vector2 input = Camera.main.ScreenToWorldPoint(new Vector3(context.ReadValue<Vector2>().x, context.ReadValue<Vector2>().y, Camera.main.nearClipPlane));
        input = input - (Vector2) this.transform.position;
        if (Math.Abs(input.sqrMagnitude) > 0.01f)
        {
            this.aim = input;
            this.gun.transform.localPosition = this.aim.normalized * 2f;
            this.Flip();
        }
    }

    private void Flip()
    {
        // if rotating player instead of setting gun position
        // this.rb.rotation = Mathf.Atan2(input.y, input.x) * Mathf.Rad2Deg - 90f;
        this.renderer.flipX = this.aim.x < 0f;
    }
    
    public void Shoot(InputAction.CallbackContext context)
    {
        if (this.nextShot > 0f) 
            return;
        
        GameObject obj = Instantiate(this.bullet);
        obj.transform.position = this.gun.transform.position;
        obj.GetComponent<Bullet>().Fire(this.gun.transform.localPosition.normalized);
        this.nextShot = 1 / this.fireRate;
    }
}
