using UnityEngine;
using UnityEngine.SceneManagement;

public class GameBarrier : MonoBehaviour
{
    [SerializeField] private TextAsset inkJSONDialogue = null;
    private DialogueManager dialogueManager;
    private PlayerMovementScript playerMovement;
    [SerializeField] private STORYPOINT[] StorypointsToActivateTrigger;
    private GameManager gameManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovementScript>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        dialogueManager = GameObject.Find("DialogueManager").GetComponent<DialogueManager>();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        CheckCurrentStorypoint();
    }

    private void OnTriggerStay(Collider other)
    {
        if (playerMovement.GetCutscene())
        {
            return;
        }
        if (other.name == "Player")
        {
            playerMovement.SetCutscene(true);
            dialogueManager.StartStory(inkJSONDialogue);
        }
    }

    private void CheckCurrentStorypoint() {
        if (this == null) {
            return;
        }
        foreach (STORYPOINT storypoint in StorypointsToActivateTrigger) {
            if (storypoint.Equals(gameManager.GetStoryPoint())) {
                this.gameObject.GetComponent<SceneTransitionFlag>().enabled = false;
                return;
            }
        }
        this.enabled = false;
    }
}
