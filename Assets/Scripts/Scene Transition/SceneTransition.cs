using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    [SerializeField] Animator transition;
    PersistentObject PersistentObjects;
    public float transitionDuration = 1f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PersistentObjects = GameObject.Find("PERSISTENTOBJECTS").GetComponent<PersistentObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TriggerEncounter(1);
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            LeaveEncounter();
        }
    }
    void TriggerEncounter(int sceneIndex)
    {
        StartCoroutine(LoadEncounter(sceneIndex, false));
    }
    void LeaveEncounter()
    {
        StartCoroutine(LoadEncounter(0, true));

    }

    IEnumerator LoadEncounter(int sceneIndex, Boolean poEnable) {
        transition.SetTrigger("START");
        
        yield return new WaitForSeconds(transitionDuration);

        SceneManager.LoadScene(sceneIndex);
        PersistentObjects.ToggleChildren(poEnable);
    }
}
