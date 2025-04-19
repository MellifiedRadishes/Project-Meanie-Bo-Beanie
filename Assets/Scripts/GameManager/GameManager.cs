using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    Dictionary<string, bool> CutsceneTriggersActive = new Dictionary<string, bool>();
    void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CheckCutsceneTriggers() {
        GameObject TriggersParentObject = GameObject.Find("CUTSCENETRIGGERS");
        if (TriggersParentObject != null) {
            foreach (Transform child in TriggersParentObject.transform) {
                CutsceneTriggersActive.TryGetValue(child.name, out bool TriggerActive);
                if (CutsceneTriggersActive.ContainsKey(child.name))
                {
                    if (!TriggerActive) {
                        Destroy(child.gameObject);
                    }
                }
                else {
                    CutsceneTriggersActive.Add(child.name, true);
                }
            }
        }
    }

    public void DisableTrigger(string triggerName)
    {
        if (!CutsceneTriggersActive.ContainsKey(triggerName)) {
            Debug.Log("ERROR: Cannot disable CutsceneTrigger that does not exist");
        }
        CutsceneTriggersActive[triggerName] = false;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        CheckCutsceneTriggers();
    }

}
