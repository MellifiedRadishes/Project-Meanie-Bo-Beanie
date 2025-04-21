using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    //*====VARIABLES====*/

    // Ink Dialogue Story Files
    [SerializeField] private TextAsset inkJSONDialogue = null;

    // Related Game Objects
    private GameObject player;
    private PlayerMovementScript playerMovement;
    private DialogueManager dialogueManager;
    private SpriteRenderer SR;

    void Start()
    {
        player = GameObject.Find("Player");
        playerMovement = player.GetComponent<PlayerMovementScript>();
        dialogueManager = GameObject.Find("DialogueManager").GetComponent<DialogueManager>();
        SR = gameObject.GetComponent<SpriteRenderer>();
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
        dialogueManager.StartStory(inkJSONDialogue);
    }

    public TextAsset GetDialogueFile() {
        return inkJSONDialogue;
    }
}
