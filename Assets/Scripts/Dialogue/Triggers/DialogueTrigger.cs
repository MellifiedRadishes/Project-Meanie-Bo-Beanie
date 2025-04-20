using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    //*====VARIABLES====*/

    // Ink Dialogue Story Files
    [SerializeField] private TextAsset inkJSONMainDialogue = null;
    [SerializeField] private TextAsset inkJSONSecondDialogue = null;

    // Related Game Objects
    private GameObject player;
    private PlayerMovementScript playerMovement;
    private DialogueManager dialogueManager;
    private SpriteRenderer SR;

    // Interaction Variables
    private bool interacted;

    void Start()
    {
        player = GameObject.Find("Player");
        playerMovement = player.GetComponent<PlayerMovementScript>();
        dialogueManager = GameObject.Find("DialogueManager").GetComponent<DialogueManager>();
        SR = gameObject.GetComponent<SpriteRenderer>();
        interacted = false;
    }

    void Update()
    {
        if (Vector3.Distance(player.transform.position, this.transform.position) < 1){
            if (playerMovement.GetCutscene()) {
                return;
            }
            if (Input.GetKeyDown(KeyCode.F)) {
                StartDialogue();
            }
        }

    }
    void FlipCharacter() {
        if (player.transform.position.x < transform.position.x)
        {
            SR.flipX = false;
        }
        else if (player.transform.position.x > transform.position.x)
        {
            SR.flipX = true;
        }
    }

    void StartDialogue() {
        FlipCharacter();
        playerMovement.SetCutscene(true);
        if (inkJSONSecondDialogue != null && interacted) // Plays Second Dialogue if Interacted With
        {
            dialogueManager.StartStory(inkJSONSecondDialogue);
        }
        else // Plays Primary Dialogue if First Time
        {
            dialogueManager.StartStory(inkJSONMainDialogue);
            interacted = true;
        }
    }
}
