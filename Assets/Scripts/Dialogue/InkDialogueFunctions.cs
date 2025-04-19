using UnityEngine;
using Ink.Runtime;
using TMPro;

public class InkDialogueFunctions
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Bind(Story story, DialogueManager manager, TextMeshProUGUI speakerText, CutsceneManager cutsceneManager)
    {
        
        story.BindExternalFunction("ChangeSpeaker", (string name) =>
        {
            speakerText.text = name;
        });
        story.BindExternalFunction("PlayCutscene", (string cutsceneName) =>
        {
            manager.SetPausedDialogue(true);
            cutsceneManager.PlayCutscene(cutsceneName);
        });
    }

    public void Unbind(Story story)
    {
        story.UnbindExternalFunction("ChangeSpeaker");
        story.UnbindExternalFunction("PlayCutscene");
    }

}
