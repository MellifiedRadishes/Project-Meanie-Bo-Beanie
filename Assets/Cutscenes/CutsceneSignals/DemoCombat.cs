using UnityEngine;
using UnityEngine.SceneManagement;

public class DemoCombat : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private CutsceneManager cutsceneManager;
    [SerializeField] private SceneTransition sceneManager;
    private void Awake()
    {
        
    }

    public void LeaveCombat() {
        sceneManager.LeaveCombat();
    }

}
