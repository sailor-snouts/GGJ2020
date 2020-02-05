using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnyKeyScene : MonoBehaviour
{
    [SerializeField] private string nextScene;

    private void Update()
    {
        if (Input.anyKey)
        {
            this.GoToNextScene();
        }
    }

    public void GoToNextScene()
    {
        SceneManager.LoadScene(this.nextScene);
    }
}
