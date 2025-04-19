using UnityEngine;

public class CutsceneTrigger : MonoBehaviour
{
    //*====VARIABLES====*/
    private GameManager gameManager;

    // Ink Dialogue Story Files
    [SerializeField] private TextAsset inkJSONCutsceneDialogue = null;

    // Related Game Objects
    private GameObject player;
    private PlayerMovementScript playerMovement;
    private DialogueManager dialogueManager;

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        player = GameObject.Find("Player");
        playerMovement = player.GetComponent<PlayerMovementScript>();
        dialogueManager = GameObject.Find("DialogueManager").GetComponent<DialogueManager>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (playerMovement.GetCutscene()) {
            return;
        }
        if (other.name == "Player")
        {
            gameManager.DisableTrigger(this.transform.name);
            playerMovement.SetCutscene(true);
            dialogueManager.StartStory(inkJSONCutsceneDialogue);
            Destroy(this.gameObject);
        }
    }
}
