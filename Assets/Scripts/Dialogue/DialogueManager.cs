using System;
using Ink.Runtime;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class DialogueManager : MonoBehaviour {

    public static event Action<Story> OnCreateStory;
    public static DialogueManager instance; //Stores Instance for Persistant Game Object

    // Dialogue Objects
    [SerializeField] private GameObject dialogueBox = null;
    [SerializeField] private TextMeshProUGUI dialogueTitle = null;
    [SerializeField] private TextMeshProUGUI textObject = null;
    public Story story;

    // UI Prefabs

    [SerializeField] private Button buttonPrefab = null;

    private GameObject player;
    private PlayerMovementScript playerMovement;

    private void Awake()
    {
        player = GameObject.Find("Player");
        playerMovement = player.GetComponent<PlayerMovementScript>();
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
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
    public void StartStory (TextAsset dialogue, string dialogueName) {
		story = new Story (dialogue.text);
        if (OnCreateStory != null) OnCreateStory(story);
        ToggleDialogueBox(true);
        dialogueTitle.text = dialogueName;
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
		else {
            ToggleDialogueBox(false);
            playerMovement.SetCutscene(false);
        }
	}

    void ToggleDialogueBox(Boolean active) {
        dialogueBox.SetActive(active); 
    }



}
