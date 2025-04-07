using System;
using Ink.Runtime;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class DialogueManager : MonoBehaviour {

    public Story story;
    public static event Action<Story> OnCreateStory;
    public static DialogueManager instance; //Stores Instance for Persistant Game Object

    // Dialogue Objects
    [SerializeField] private GameObject dialogueBox = null;
    [SerializeField] private GameObject choiceContainer = null;
    [SerializeField] private TextMeshProUGUI dialogueTitle = null;
    [SerializeField] private TextMeshProUGUI textObject = null;
    

    // Button Prefab
    [SerializeField] private Button buttonPrefab = null;

    // Player Game Object
    private GameObject player;
    private PlayerMovementScript playerMovement;

    // Dialogue State Variables
    private bool choicePoint;

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
			if (Input.GetMouseButtonDown(0) && !choicePoint) {
				RefreshView();
            }
		}
    }

    /*====DIALOGUE PARSING====*/

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
        RemoveButtons(); //Removes Any Buttons from Previous Choice
        if (story.canContinue)
        {
            // Set text to the next line of the story
            textObject.text = story.Continue();
            // This removes any white space from the text.
            textObject.text = textObject.text.Trim();
        }
        else if (!choicePoint)
        {
            // If the story cannot continue and there are no choices, end dialogue
            ToggleDialogueBox(false);
            playerMovement.SetCutscene(false);
        }

        // Check for Choices
        if (story.currentChoices.Count > 0)
        {
            // Set flag to indicate choice point
            choicePoint = true;
            for (int i = 0; i < story.currentChoices.Count; i++)
            {
                // Create a button for each choice
                Choice choice = story.currentChoices[i];
                Button button = CreateChoiceView(choice.text.Trim());

                // Set button to advance story
                button.onClick.AddListener(delegate {
                    OnClickChoiceButton(choice);
                });
            }
        }
    }

    void ToggleDialogueBox(Boolean active) {
        dialogueBox.SetActive(active); 
    }

    /*====BUTTON FUNCTIONS====*/
    void OnClickChoiceButton(Choice choice)
    {
        choicePoint = false; // Resets choice point flag
        story.ChooseChoiceIndex(choice.index);
        story.Continue(); //Skips displaying player's choice
        RefreshView();
    }

    Button CreateChoiceView(string text)
    {
        // Creates the button from a prefab
        Button choice = Instantiate(buttonPrefab) as Button;
        choice.transform.SetParent(choiceContainer.transform, false);

        // Gets the text from the button prefab
        Text choiceText = choice.GetComponentInChildren<Text>();
        choiceText.text = text;

        // Make the button expand to fit the text
        HorizontalLayoutGroup layoutGroup = choice.GetComponent<HorizontalLayoutGroup>();
        layoutGroup.childForceExpandHeight = false;

        return choice;
    }

    // Destroys all buttons from choice container
    void RemoveButtons()
    {
        int childCount = choiceContainer.transform.childCount;
        for (int i = childCount - 1; i >= 0; --i)
        {
            Destroy(choiceContainer.transform.GetChild(i).gameObject);
        }
    }

}
