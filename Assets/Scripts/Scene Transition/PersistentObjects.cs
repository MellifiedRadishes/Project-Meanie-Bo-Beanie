using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PersistentObject : MonoBehaviour
{
    public static PersistentObject instance;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else {
            Destroy(gameObject);
        }
    }


    public void ToggleChildren(Boolean boolean)
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(boolean);
        }
    }
}
