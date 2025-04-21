using System;
using Ink.Runtime;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class DialogueManager : MonoBehaviour {

    [SerializeField] private TextAsset globalsJSON;
    public Story story;
    public static event Action<Story> OnCreateStory;

    // Dialogue Objects
    [SerializeField] private GameObject DialogueBox;
    
    [SerializeField] private GameObject ChoiceContainer;
    [SerializeField] private TextMeshProUGUI SpeakerText;
    [SerializeField] private TextMeshProUGUI TextObject;
    [SerializeField] private CutsceneManager cutsceneManager;
    [SerializeField] private SceneTransition sceneManager;
    [SerializeField] private GameManager gameManager;

    // Functions
    private InkDialogueFunctions ExternalFunctions;
    private DialogueVariables dialogueVariables;
    // Button Prefab
    [SerializeField] private Button buttonPrefab = null;

    // Player Game Object
    private GameObject player;
    private PlayerMovementScript playerMovement;

    // Dialogue State Variables
    private bool PauseDialogue;

    private void Awake()
    {
        ExternalFunctions = new InkDialogueFunctions();
        dialogueVariables = new DialogueVariables(globalsJSON);
        player = GameObject.Find("Player");
        playerMovement = player.GetComponent<PlayerMovementScript>();
    }

    private void Update()
    {
        if (TextObject.text == "")
        {
            ToggleDialogueBox(false);
            playerMovement.SetCutscene(false);
            return;
        }
        if (story != null)
        { // Refreshes text on input
            if (Input.GetMouseButtonDown(0) && !PauseDialogue)
            {
                RefreshView();
            }
        }
        
    }
    /*====DIALOGUE START/STOP====*/

    // Creates a new Story object and starts
    public void StartStory (TextAsset dialogue) {
        
		story = new Story (dialogue.text);
        if (OnCreateStory != null) OnCreateStory(story);

        story.variablesState["current_story_point"] = (int) gameManager.GetStoryPoint();
        story.variablesState["dialogue_state"] = gameManager.GetDialogueState(dialogue.name);
        story.variablesState["cake_flavor"] = gameManager.GetCakeFlavor();

        ToggleDialogueBox(true);
        playerMovement.SetCutscene(true);

        dialogueVariables.StartListening(story);
        ExternalFunctions.Bind(story, dialogue, SpeakerText, this, cutsceneManager, gameManager, sceneManager);

        RefreshView();
    }

    public void EndStory()
    {
        // If the story cannot continue and there are no choices, end dialogue
        ToggleDialogueBox(false);
        playerMovement.SetCutscene(false);

        dialogueVariables.StopListening(story);
        ExternalFunctions.Unbind(story);

        story = null;
    }

    /*====DIALOGUE PARSING====*/
    // Updates Text Object using Ink Story
    void RefreshView () {
        RemoveButtons(); //Removes Any Buttons from Previous Choice
        if (story.canContinue)
        {
            // Set text to the next line of the story
            TextObject.text = story.Continue();
            // This removes any white space from the text.
            TextObject.text = TextObject.text.Trim();

        }
        else
        {
            EndStory();
            return;
        }

        // Check for Choices
        if (story.currentChoices.Count > 0)
        {
            // Set flag to indicate choice point
            PauseDialogue = true;
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

    /*====BUTTON FUNCTIONS====*/
    void OnClickChoiceButton(Choice choice)
    {
        PauseDialogue = false; // Resets choice point flag
        story.ChooseChoiceIndex(choice.index);
        story.Continue(); //Skips displaying player's choice
        RefreshView();
    }

    Button CreateChoiceView(string text)
    {
        // Creates the button from a prefab
        Button choice = Instantiate(buttonPrefab) as Button;
        choice.transform.SetParent(ChoiceContainer.transform, false);

        // Gets the text from the button prefab
        Text choiceText = choice.GetComponentInChildren<Text>();
        choiceText.text = text;

        return choice;
    }

    // Destroys all buttons from choice container
    void RemoveButtons()
    {
        int childCount = ChoiceContainer.transform.childCount;
        for (int i = childCount - 1; i >= 0; --i)
        {
            Destroy(ChoiceContainer.transform.GetChild(i).gameObject);
        }
    }

    /*====DIALOGUE PAUSING====*/
    public void SetPausedDialogue(bool boolean) { 
        PauseDialogue = boolean;
    }

    void ToggleDialogueBox(Boolean active)
    {
        DialogueBox.SetActive(active);
    }


    /*====GETTER FUNCTIONS====*/

    public Ink.Runtime.Object GetVariableState(string variableName)
    {
        Ink.Runtime.Object variableValue = null;
        dialogueVariables.variables.TryGetValue(variableName, out variableValue);
        return variableValue;
       
    }

    public bool CanStoryContinue() { 
        return story.canContinue;
    }

}
