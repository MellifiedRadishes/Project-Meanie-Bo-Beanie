using UnityEngine;
using Ink.Runtime;
using TMPro;

public class InkDialogueFunctions
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Bind(Story story, TextMeshProUGUI speakerText, DialogueManager dialogueManager,
        CutsceneManager cutsceneManager, GameManager gameManager)
    {
        
        story.BindExternalFunction("ChangeSpeaker", (string name) =>
        {
            speakerText.text = name;
        });
        story.BindExternalFunction("PlayCutscene", (string cutsceneName) =>
        {
            dialogueManager.SetPausedDialogue(true);
            cutsceneManager.PlayCutscene(cutsceneName);
        });
        story.BindExternalFunction("ChangeStoryPoint", (int storypoint) =>
        {
            gameManager.SetStoryPoint((STORYPOINT) storypoint);
        });
    }

    public void Unbind(Story story)
    {
        story.UnbindExternalFunction("ChangeSpeaker");
        story.UnbindExternalFunction("PlayCutscene");
        story.UnbindExternalFunction("ChangeStoryPoint");
    }

}
