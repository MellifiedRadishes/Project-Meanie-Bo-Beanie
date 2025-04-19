using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CutsceneManager : MonoBehaviour
{
    [SerializeField] GameObject DialogueCanvas;
    private DialogueManager dialogueManager;

    private PlayableDirector Director;
    [SerializeField] CutsceneAsset[] cutsceneList;

    Dictionary<string, PlayableAsset> CutsceneDictionary = new Dictionary<string, PlayableAsset>();
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Director = GetComponent<PlayableDirector>();
        dialogueManager = GameObject.Find("DialogueManager").GetComponent<DialogueManager>();
        CreateCutsceneDictionary();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void CreateCutsceneDictionary() {
        
        foreach (var cutscene in cutsceneList)
        {
            Debug.Log("ADDING: " + cutscene.GetCutsceneName() + " + " + cutscene.GetCutscenePlayable());
            CutsceneDictionary.Add(cutscene.GetCutsceneName(), cutscene.GetCutscenePlayable());
        }
    
    }

    public void PlayCutscene(string name) {
        CutsceneDictionary.TryGetValue(name, out PlayableAsset cutscene);
        Director.playableAsset = cutscene;
        Director.Play();
        StartCoroutine(HideDialogueCanvas((float) Director.playableAsset.duration));
    }

    IEnumerator HideDialogueCanvas(float seconds) { 
        DialogueCanvas.SetActive(false);

        yield return new WaitForSeconds(seconds);

        DialogueCanvas.SetActive(true);

        dialogueManager.SetPausedDialogue(false);
    }
}

/*====DATA CLASS TO HELP IMPORTING CUTSCENES TO DICTIONARY CLEARER====*/
[Serializable]
public class CutsceneAsset {
    [SerializeField] string name;
    [SerializeField] PlayableAsset cutscene;
    public PlayableAsset GetCutscenePlayable() {
        return cutscene;
    }
    public string GetCutsceneName()
    {
        return name;
    }
}
