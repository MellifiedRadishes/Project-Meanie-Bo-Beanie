using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PersistentManagers : MonoBehaviour
{
    public static PersistentManagers instance;

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
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {

    }

    public void ToggleChildren(Boolean boolean)
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(boolean);
        }
    }
}
