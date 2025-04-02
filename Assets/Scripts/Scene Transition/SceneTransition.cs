using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{

    /*====ANIMATION VARIABLES====*/
    SCENE oldScene;
    [SerializeField] Animator transition;
    public float transitionDuration = 1f;

    /*====SAVED GAME OBJECTS====*/
    private PersistentObject PersistentObjects;

    /*==Player Object/Scripts==*/
    public GameObject Player;
    public CharacterController PlayerCharacterController;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
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

    void TriggerCombat()
    {
        oldScene = (SCENE) SceneManager.GetActiveScene().buildIndex;
        StartCoroutine(LoadCombat());
    }
    void LeaveCombat()
    {
        StartCoroutine(ExitCombat(oldScene));
    }

    public void TriggerSceneChange(SCENE scene, Vector3 newPosition) {
        StartCoroutine(LoadMapScene(scene, newPosition));
    }

    IEnumerator LoadCombat() {
        transition.SetTrigger("START");
        
        yield return new WaitForSeconds(transitionDuration);

        SceneManager.LoadScene((int)SCENE.COMBAT);

        PersistentObjects.ToggleChildren(false);
        
    }

    IEnumerator ExitCombat(SCENE scene)
    {
        transition.SetTrigger("START");

        yield return new WaitForSeconds(transitionDuration);

        SceneManager.LoadScene((int)scene);

        PersistentObjects.ToggleChildren(true);

    }

    IEnumerator LoadMapScene(SCENE scene, Vector3 newPosition)
    {
        transition.SetTrigger("START");

        yield return new WaitForSeconds(transitionDuration);

        SceneManager.LoadScene((int)scene);

        PlayerCharacterController.enabled = false;
        Player.transform.position = newPosition;
        PlayerCharacterController.enabled = true;

        transition.SetTrigger("START");

    }


}
