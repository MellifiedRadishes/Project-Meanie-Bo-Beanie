using UnityEngine;
using Ink.Runtime;
using TMPro;

public class InkDialogueFunctions
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Bind(Story story)
    {
        
        story.BindExternalFunction("ChangeSpeaker", (string name) =>
        {
            GameObject.Find("SpeakerText").GetComponent<TextMeshProUGUI>().text = name;
        });
        story.BindExternalFunction("PlayCutscene", () =>
        {
            Debug.Log("cutscene");
        });
    }

    public void Unbind(Story story)
    {
        story.UnbindExternalFunction("ChangeSpeaker");
        story.UnbindExternalFunction("PlayCutscene");
    }

}
