using UnityEngine;
using UnityEngine.SceneManagement;

public class CutsceneTrigger : MonoBehaviour
{
    //*====VARIABLES====*/
    

    // Ink Dialogue Story Files
    [SerializeField] private TextAsset inkJSONCutsceneDialogue = null;
    [SerializeField] private STORYPOINT[] StorypointsToActivateTrigger;

    // Related Game Objects
    private GameObject player;
    private PlayerMovementScript playerMovement;
    private DialogueManager dialogueManager;
    private GameManager gameManager;

    void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        player = GameObject.Find("Player");
        playerMovement = player.GetComponent<PlayerMovementScript>();
        dialogueManager = GameObject.Find("DialogueManager").GetComponent<DialogueManager>();

        SceneManager.sceneLoaded += OnSceneLoaded;
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

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        CheckCurrentStorypoint();
    }
    private void CheckCurrentStorypoint()
    {
        if (this == null)
        {
            return;
        }
        foreach (STORYPOINT storypoint in StorypointsToActivateTrigger)
        {
            if (storypoint.Equals(gameManager.GetStoryPoint()))
            {
                this.GetComponent<BoxCollider>().enabled = true;
                return;
            }
        }
        this.GetComponent<BoxCollider>().enabled = false;
    }

}
