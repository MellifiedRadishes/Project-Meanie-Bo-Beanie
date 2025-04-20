using UnityEngine;

public class SceneTransitionFlag : MonoBehaviour
{
    SceneTransition SceneLoader;
    [SerializeField] private SCENE scene;
    [SerializeField] private Vector3 newPosition;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SceneLoader = GameObject.Find("SceneLoader").GetComponent<SceneTransition>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (this.enabled && other.name == "Player") {
             SceneLoader.TriggerSceneChange(scene, newPosition);
        }
    }
}


