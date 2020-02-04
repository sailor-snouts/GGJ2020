using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(SpriteRenderer))]
public class EnemyController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private AudioSource audio;
    private SpriteRenderer renderer;
    private bool isAlive = true;
    private bool beginMovement = false;
    private bool pauseAnimation = false;
    [SerializeField] private GameObject soul;
    [SerializeField] private float speed = 2f;
    [SerializeField] private float health = 5f;
    private GameObject well;
    private Score score;
    [SerializeField] private AudioClip[] hitSFX;
    [SerializeField] private AudioClip[] crumbleSFX;
    
    void Start()
    {
        this.rb = GetComponent<Rigidbody2D>();
        this.anim = GetComponent<Animator>();
        this.audio = GetComponent<AudioSource>();
        this.renderer = GetComponent<SpriteRenderer>();
        this.well = FindObjectOfType<Well>().gameObject;
        this.score = FindObjectOfType<Score>();
    }

    public void Pause()
    {
        this.anim.enabled = false;
    }
    
    private void FixedUpdate()
    {
        if (!this.beginMovement)
            return;
        if (!this.isAlive)
        {
            this.anim.speed = 0;
            return;
        }

        Vector2 direction = this.well.transform.position - this.transform.position;
        this.rb.MovePosition(this.rb.position + direction.normalized * (this.speed * Time.fixedDeltaTime));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            this.health--;
            if (this.isAlive)
            {
                this.audio.PlayOneShot(this.hitSFX[Random.Range(0, this.hitSFX.Length)]);
                
                if (this.health <= 0)
                {
                    this.Freeze();
                }

                this.health = 5f;
            }
            else
            {
                this.audio.PlayOneShot(this.crumbleSFX[Random.Range(0, this.crumbleSFX.Length)]);
                
                if (this.health <= 0)
                {
                    this.Crumble();
                }
            }
        }
    }

    public void BeginMovement()
    {
        this.beginMovement = true;
    }
    
    private void Freeze()
    {
        this.score.Value += 10;
        this.isAlive = false;
        this.rb.bodyType = RigidbodyType2D.Static;
        if (this.beginMovement)
        {
            float frame = this.anim.GetCurrentAnimatorStateInfo(0).normalizedTime;
            this.anim.SetBool("Frozen", true);
            this.anim.Play("ZombieIdleStatue", 0, frame);
            Debug.Log(frame);
        }
        else
        {
            float frame = this.anim.GetCurrentAnimatorStateInfo(0).normalizedTime;
            this.anim.SetBool("Frozen", true);
            this.anim.Play("ZombieSpawnStatue", 0, frame);
        }
    }

    private void Crumble()
    {
        GameObject soul = Instantiate(this.soul);
        soul.transform.position = this.transform.position;
        this.GetComponent<Collider2D>().enabled = false;
        this.rb.isKinematic = true;
        this.anim.enabled = true;
        this.anim.SetTrigger("Crumble");
    }
}
