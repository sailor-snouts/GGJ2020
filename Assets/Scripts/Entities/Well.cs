using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.SceneManagement;

public class Well : MonoBehaviour
{
    private float _souls = 1f;
    public float Souls
    {
        set
        {
            _souls = value;
            if(this._souls < 0)
            {
                Time.timeScale = 0;
                SceneManager.LoadScene("GameOver");
            }
            else
            {
                float scale = this._souls / 20f;
                this.light.transform.localScale = new Vector3(scale, scale, scale);
            }
        }
        get { return this._souls; }
    }

    [SerializeField] private GameObject light;

    private void Start()
    {
        this.Souls = 10f;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            this.Souls -= 1f;
            Destroy(other.gameObject);
        }
    }
}
