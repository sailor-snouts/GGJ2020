using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Score : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;
    private int _value;
    public int Value
    {
        set
        {
            this._value = value; 
            this.scoreText.SetText(this._value.ToString());
        }
        get { return this._value; }
    }

    private float scoreTimer = 5f;

    void Start()
    {
        this.Value = 0;
    }
    
    void Update()
    {
        this.scoreTimer -= Time.deltaTime;
        if (this.scoreTimer <= 0f)
        {
            this.Value += 10;
            this.scoreTimer = 5f;
        }
    }
}
