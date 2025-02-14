using UnityEngine;
using UnityEngine.SceneManagement;

public class CombatTransition : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TriggerEncounter(1);
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            LeaveEncounter();
        }
    }
    void TriggerEncounter(int i)
    {
        SceneManager.LoadScene(i);
    }
    void LeaveEncounter()
    {
        SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(0));

    }
}
