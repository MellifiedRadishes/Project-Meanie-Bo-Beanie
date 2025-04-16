using System.Collections;
using UnityEngine;
using UnityEngine.Playables;

public class CutsceneManager : MonoBehaviour
{
    [SerializeField] GameObject DialogueCanvas;
    private DialogueManager dialogueManager;

    private PlayableDirector Director;
    [SerializeField] PlayableAsset[] cutsceneList;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Director = GetComponent<PlayableDirector>();
        dialogueManager = GameObject.Find("DialogueManager").GetComponent<DialogueManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayCutscene(int index) {

        Director.playableAsset = cutsceneList[index];
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
