using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    STORYPOINT currentStorypoint = STORYPOINT.WakeUp;
    Dictionary<string, bool> CutsceneTriggersVisited = new Dictionary<string, bool>();
    Dictionary<string, int> DialogueVisited = new Dictionary<string, int>();

    private string CakeFlavor = "Chocolate";
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        CheckCutsceneTriggers();
        CheckDialogueStates();
    }

    /*====CUTSCENE TRIGGER ACTIVATION====*/
    public void CheckCutsceneTriggers() {
        GameObject TriggersParentObject = GameObject.Find("CUTSCENETRIGGERS");
        if (TriggersParentObject != null) {
            foreach (Transform child in TriggersParentObject.transform) {
                CutsceneTriggersVisited.TryGetValue(child.name, out bool TriggerVisited);
                if (CutsceneTriggersVisited.ContainsKey(child.name))
                {
                    if (TriggerVisited) {
                        Destroy(child.gameObject);
                    }
                    
                }
                else {
                    CutsceneTriggersVisited.Add(child.name, false);
                }
            }
        }
    }

    public void DisableTrigger(string triggerName)
    {
        if (!CutsceneTriggersVisited.ContainsKey(triggerName)) {
            Debug.Log("ERROR: CUTSCENE TRIGGER " + triggerName + " DOES NOT EXIST");
        }
        CutsceneTriggersVisited[triggerName] = false;
    }

    /*====DIALOGUE STATE STORAGE====*/
    public void CheckDialogueStates()
    {
        
        GameObject CharactersParentObject = GameObject.Find("CHARACTERS");
        if (CharactersParentObject != null)
        {
            foreach (Transform child in CharactersParentObject.transform)
            {
                string dialogueName = child.gameObject.GetComponent<DialogueTrigger>().GetDialogueFile().name;
                if (!DialogueVisited.ContainsKey(dialogueName))
                {
                    DialogueVisited.Add(dialogueName, 0);
                }
            }
        }
    }

    /*====DIALOGUE STATE SETTERS/GETTERS====*/

    public void SetDialogueState(string dialogueName, int stateIndex)
    {
        if (!DialogueVisited.ContainsKey(dialogueName))
        {
            Debug.Log("ERROR: DIALOGUE " + dialogueName + " DOES NOT EXIST");
        }
        DialogueVisited[dialogueName] = stateIndex;
    }

    public int GetDialogueState(string dialogue)
    {
        if (DialogueVisited.ContainsKey(dialogue))
        {
            return DialogueVisited[dialogue];
        }
        return 0;
    }


    /*====STORYPOINT SETTERS/GETTERS====*/
    public void SetStoryPoint(STORYPOINT point) {
 
        currentStorypoint = point;
    }

    public STORYPOINT GetStoryPoint()
    {
        return currentStorypoint;
    }

    /*====CAKEFLAVOR SETTERS/GETTERS====*/
    public void SetCakeFlavor(string flavor)
    {

        CakeFlavor = flavor;
    }

    public string GetCakeFlavor()
    {
        return CakeFlavor;
    }

}
