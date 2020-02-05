using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(SpriteRenderer))]
public class Bullet : MonoBehaviour
{
    private Rigidbody2D rb;
    private AudioSource audio;
    private SpriteRenderer renderer;
    private bool fired = false;
    private float lifetime = 2f;
    [SerializeField] private float speed = 5f;
    [SerializeField] private AudioClip[] fireSFX;

    void Awake()
    {
        this.rb = GetComponent<Rigidbody2D>();
        this.audio = GetComponent<AudioSource>();
        this.renderer = GetComponent<SpriteRenderer>();
        this.rb.velocity = Vector2.zero;
    }

    private void Update()
    {
        if (!fired)
            return;
        
        this.lifetime -= Time.deltaTime;
        
        if (this.lifetime <= 0f)
            Destroy(this.gameObject);
    }

    public void Fire(Vector2 direction)
    {
        this.audio.PlayOneShot(this.fireSFX[Random.Range(0, this.fireSFX.Length)]);
        this.rb.velocity = direction * this.speed;
        this.transform.rotation = Quaternion.AngleAxis(Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg, Vector3.forward);
        this.fired = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        this.renderer.enabled = false;
        this.rb.isKinematic = true;
        this.GetComponent<Collider2D>().enabled = false;
        Destroy(this.gameObject, 1f);
    }
}
