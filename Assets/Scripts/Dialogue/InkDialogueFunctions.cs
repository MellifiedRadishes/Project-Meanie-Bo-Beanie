using UnityEngine;
using Ink.Runtime;
using TMPro;
using NUnit.Framework.Constraints;
using UnityEngine.SceneManagement;

public class InkDialogueFunctions
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Bind(Story story, TextAsset dialogueFile, TextMeshProUGUI speakerText, DialogueManager dialogueManager,
        CutsceneManager cutsceneManager, GameManager gameManager, SceneTransition sceneManager)
    {
        
        story.BindExternalFunction("ChangeSpeaker", (string name) =>
        {
            speakerText.text = name;
        });
        story.BindExternalFunction("PlayCutscene", (string cutsceneName) =>
        {
            dialogueManager.SetPausedDialogue(true);

            if (cutsceneName.Substring(0, 7).Equals("CENTRAL")) {
                cutsceneManager.PlayCutscene(cutsceneName);
            }
            else
            {
                CutsceneManager localManager = GameObject.Find("CutsceneManager").GetComponent<CutsceneManager>();
                localManager.PlayCutscene(cutsceneName);
            }
        });
        story.BindExternalFunction("ChangeStoryPoint", (int storypoint) =>
        {
            gameManager.SetStoryPoint((STORYPOINT) storypoint);
        });

        story.BindExternalFunction("LoadScene", (int sceneIndex) =>
        {
            SceneManager.LoadScene(sceneIndex);
        });
        story.BindExternalFunction("CombatTransition", () =>
        {
            sceneManager.TriggerCombat();
        });
        story.BindExternalFunction("UpdateDialogueState", (int stateIndex) =>
        {
            gameManager.SetDialogueState(dialogueFile.name, stateIndex);
        });
        story.BindExternalFunction("UpdateCakeFlavor", (string cakeFlavor) =>
        {
            gameManager.SetCakeFlavor(cakeFlavor);
        });
    }

    public void Unbind(Story story)
    {
        story.UnbindExternalFunction("ChangeSpeaker");
        story.UnbindExternalFunction("PlayCutscene");
        story.UnbindExternalFunction("ChangeStoryPoint");
        story.UnbindExternalFunction("LoadScene");
        story.UnbindExternalFunction("CombatTransition");
        story.UnbindExternalFunction("UpdateDialogueState");
        story.UnbindExternalFunction("UpdateCakeFlavor");
    }

}
