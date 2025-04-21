using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CutsceneManager : MonoBehaviour
{
    private PlayerMovementScript PlayerMovement;
    private GameObject DialogueCanvas;
    private DialogueManager dialogueManager;

    private PlayableDirector Director;
    [SerializeField] CutsceneAsset[] cutsceneList;

    Dictionary<string, PlayableAsset> CutsceneDictionary = new Dictionary<string, PlayableAsset>();
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Director = GetComponent<PlayableDirector>();
        dialogueManager = GameObject.Find("DialogueManager").GetComponent<DialogueManager>();
        DialogueCanvas = dialogueManager.gameObject.transform.Find("DialogueCanvas").gameObject;
        PlayerMovement = GameObject.Find("Player").GetComponent<PlayerMovementScript>();
        CreateCutsceneDictionary();
    }


    void CreateCutsceneDictionary() {
        
        foreach (var cutscene in cutsceneList)
        {
            CutsceneDictionary.Add(cutscene.GetCutsceneName(), cutscene.GetCutscenePlayable());
        }
    
    }

    public void PlayCutscene(string name) {
        CutsceneDictionary.TryGetValue(name, out PlayableAsset cutscene);
        Director.playableAsset = cutscene;
        Director.Play();

        if (!PlayerMovement.GetCutscene())
        {
            StartCoroutine(DisablePlayerMovement((float)Director.playableAsset.duration));
        }
        if (DialogueCanvas.activeSelf && dialogueManager.CanStoryContinue()) {
            StartCoroutine(HideDialogueCanvas((float)Director.playableAsset.duration));
        }
        
    }

    IEnumerator HideDialogueCanvas(float seconds) {
        DialogueCanvas.SetActive(false);

        yield return new WaitForSeconds(seconds);

        DialogueCanvas.SetActive(true);
        dialogueManager.SetPausedDialogue(false);
    }

    IEnumerator DisablePlayerMovement(float seconds)
    {
        PlayerMovement.SetCutscene(true); // Stop Player Movement

        yield return new WaitForSeconds(seconds);

        PlayerMovement.SetCutscene(false); // Reactivate Player Movement
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
