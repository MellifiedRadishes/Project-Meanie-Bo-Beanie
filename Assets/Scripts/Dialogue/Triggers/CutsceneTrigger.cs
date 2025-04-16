using UnityEngine;

public class CutsceneTrigger : MonoBehaviour
{
    //*====VARIABLES====*/

    // Ink Dialogue Story Files
    [SerializeField] private TextAsset inkJSONCutsceneDialogue = null;

    // Related Game Objects
    private GameObject player;
    private PlayerMovementScript playerMovement;
    private DialogueManager dialogueManager;

    void Start()
    {
        player = GameObject.Find("Player");
        playerMovement = player.GetComponent<PlayerMovementScript>();
        dialogueManager = GameObject.Find("DialogueManager").GetComponent<DialogueManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            playerMovement.SetCutscene(true);
            dialogueManager.StartStory(inkJSONCutsceneDialogue);
            Destroy(this.gameObject);
        }
    }
}
