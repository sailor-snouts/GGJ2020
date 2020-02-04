using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Soul : MonoBehaviour
{
    private Rigidbody2D rb;
    private GameObject well;
    private Score score;
    private Vector2 direction;
    [SerializeField] private float speed = 2f;
    
    void Start()
    {
        this.rb = GetComponent<Rigidbody2D>();
        this.well = FindObjectOfType<Well>().gameObject;
        this.score = FindObjectOfType<Score>();
        this.direction = this.well.transform.position - this.transform.position;
        this.transform.rotation = Quaternion.AngleAxis(Mathf.Atan2(this.direction.y, this.direction.x) * Mathf.Rad2Deg, Vector3.forward);
    }

    private void FixedUpdate()
    {
        this.rb.MovePosition(this.rb.position + this.direction.normalized * (this.speed * Time.fixedDeltaTime));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Well"))
        {
            this.well.GetComponent<Well>().Souls += 1;
            this.score.Value += 100;
            Destroy(this.gameObject);
        }
    }
}
