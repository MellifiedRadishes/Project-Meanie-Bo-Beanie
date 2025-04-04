using System;
using Ink.Runtime;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class DialogueManager : MonoBehaviour {
    public static event Action<Story> OnCreateStory;

    [SerializeField]
    private TextAsset inkJSONAsset = null;
    public Story story;

    // UI Prefabs
    [SerializeField]
    private TextMeshProUGUI textObject = null;
    [SerializeField]
    private Button buttonPrefab = null;

    void Awake () {
		StartStory();
	}

    private void Update()
    {
		if (story != null) { // Refreshes text on input
			if (Input.GetMouseButtonDown(0)) {
				RefreshView();
            }
		}
    }

    // Creates a new Story object and starts
    void StartStory () {
		story = new Story (inkJSONAsset.text);
        if(OnCreateStory != null) OnCreateStory(story);
		RefreshView();
	}
	
	// Updates Text Object using Ink Story
	void RefreshView () {

		if (story.canContinue)
		{
			// Set text to the next line of the story
			textObject.text = story.Continue();
			// This removes any white space from the text.
			textObject.text = textObject.text.Trim();
		}
	}



}
