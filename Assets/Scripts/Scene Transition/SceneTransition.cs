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
    

    void Awake()
    {
        PersistentObjects = GameObject.Find("PERSISTENTOBJECTS").GetComponent<PersistentObject>();
        Player = GameObject.Find("Player");
        PlayerCharacterController = GameObject.Find("Player").GetComponent<CharacterController>();
        

    }
    /*====FUNCTIONS TO TRIGGER SCENE TRANSITIONS====*/
    public void TriggerCombat()
    {
        oldScene = (SCENE) SceneManager.GetActiveScene().buildIndex;
        StartCoroutine(ToggleCombat(SCENE.COMBAT, false));
    }
    public void LeaveCombat()
    {
        StartCoroutine(ToggleCombat(oldScene, true));
    }
    public void TriggerSceneChange(SCENE scene, Vector3 newPosition) {
        StartCoroutine(LoadMapScene(scene, newPosition));
    }

    /*====COROUTINES FOR SCENE TRANSITIONS====*/
    IEnumerator ToggleCombat(SCENE scene, Boolean poEnable) {
       
        cutsceneManager.PlayCutscene("CENTRAL_SceneFadeTransition");

        yield return new WaitForSeconds(transitionDuration);

        SceneManager.LoadScene((int) scene);

        if (!poEnable) {
            cutsceneManager.PlayCutscene("DemoCombat");
        }
        

        // Temporarily Deactive
        PersistentObjects.ToggleChildren(poEnable);
    }

    IEnumerator LoadMapScene(SCENE scene, Vector3 newPosition)
    {
        cutsceneManager.PlayCutscene("CENTRAL_SceneFadeTransition");

        yield return new WaitForSeconds(transitionDuration);
        SceneManager.LoadScene((int)scene);

        gameManager.CheckCutsceneTriggers();

        // Force Change Player Position
        Player.transform.position = newPosition;
        Transform Scout = PersistentObjects.transform.Find("Scout");
        if (Scout != null)
        {
            Scout.GetComponent<ScoutMovement>().TeleportToPlayer();
        }
    }
}
