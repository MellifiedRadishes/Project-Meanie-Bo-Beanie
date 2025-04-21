using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    //*====VARIABLES====*/

    // Ink Dialogue Story Files
    [SerializeField] private TextAsset inkJSONDialogue = null;

    // Related Game Objects
    private GameObject Player;
    private PlayerMovementScript playerMovement;
    private DialogueManager dialogueManager;
    [SerializeField] private GameObject model;

    void Start()
    {
        Player = GameObject.Find("Player");
        playerMovement = Player.GetComponent<PlayerMovementScript>();
        dialogueManager = GameObject.Find("DialogueManager").GetComponent<DialogueManager>();
    }

    void Update()
    {
        if (Vector3.Distance(Player.transform.position, this.transform.position) < 1){
            if (playerMovement.GetCutscene()) {
                return;
            }
            if (Input.GetKeyDown(KeyCode.F)) {
                StartDialogue();
            }
        }

    }
    void FlipCharacter() {
        if (model == null) { return;  }
        if (Player.transform.position.x < transform.position.x)
        {
            model.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (Player.transform.position.x > transform.position.x)
        {
            model.transform.rotation = Quaternion.Euler(0, -180, 0);
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
