using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] 
    private GameObject VisualWidget;

    [SerializeField] 
    private TextAsset Dialogue;

    private bool IsInRange;

    private void Awake()
    {

        IsInRange = false;
        // VisualWidget.SetActive(false);
    }

    private void Update()
    {
        if (IsInRange)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log(Dialogue.text);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            IsInRange = true;
            // VisualWidget.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            IsInRange = false;
            // VisualWidget.SetActive(false);
        }
    }
}
