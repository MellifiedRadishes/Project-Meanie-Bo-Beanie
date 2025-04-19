using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    [SerializeField] private CutsceneManager cutsceneManager;
    [SerializeField] private GameManager gameManager;
    public static SceneTransition instance;

    /*====ANIMATION VARIABLES====*/
    SCENE oldScene;
    public float transitionDuration = 1f;

    /*====SAVED GAME OBJECTS====*/
    private PersistentObject PersistentObjects;

    /*==Player Object/Scripts==*/
    private GameObject Player;
    private CharacterController PlayerCharacterController;
    

    void Start()
    {
        PersistentObjects = GameObject.Find("PERSISTENTOBJECTS").GetComponent<PersistentObject>();
        Player = GameObject.Find("Player");
        PlayerCharacterController = GameObject.Find("Player").GetComponent<CharacterController>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TriggerCombat();

        }
        if (Input.GetKeyDown(KeyCode.K)) {
            LeaveCombat();
        }
    }
    /*====FUNCTIONS TO TRIGGER SCENE TRANSITIONS====*/
    void TriggerCombat()
    {
        oldScene = (SCENE) SceneManager.GetActiveScene().buildIndex;
        StartCoroutine(ToggleCombat(SCENE.COMBAT, false));
    }
    void LeaveCombat()
    {
        StartCoroutine(ToggleCombat(oldScene, true));
    }
    public void TriggerSceneChange(SCENE scene, Vector3 newPosition) {
        StartCoroutine(LoadMapScene(scene, newPosition));
    }

    /*====COROUTINES FOR SCENE TRANSITIONS====*/
    IEnumerator ToggleCombat(SCENE scene, Boolean poEnable) {
       
        cutsceneManager.PlayCutscene("SceneFadeTransition");

        yield return new WaitForSeconds(transitionDuration);

        SceneManager.LoadScene((int) scene);

        // Temporarily Deactive
        gameManager.CheckCutsceneTriggers();
        PersistentObjects.ToggleChildren(poEnable);
    }

    IEnumerator LoadMapScene(SCENE scene, Vector3 newPosition)
    {
        cutsceneManager.PlayCutscene("SceneFadeTransition");

        yield return new WaitForSeconds(transitionDuration);

        SceneManager.LoadScene((int)scene);

        gameManager.CheckCutsceneTriggers();

        // Force Change Player Position
        PlayerCharacterController.enabled = false;
        Player.transform.position = newPosition;
        PlayerCharacterController.enabled = true;
    }
}
