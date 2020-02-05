using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class SpriteSort : MonoBehaviour
{
    private SpriteRenderer sprite;
    [SerializeField] private bool runOnUpdate = true;
    
    void Awake ()
    {
        this.sprite = GetComponent<SpriteRenderer>();
        SetPosition();
    }

    void Update ()
    {
        if (!this.runOnUpdate && !Application.isEditor) return;
        SetPosition();
    }

    void SetPosition () {
        Vector3 newPosition = this.transform.position;
        if (this.sprite)
        {
            this.sprite.sortingOrder = (int) -this.transform.position.y;
        }
    }
}