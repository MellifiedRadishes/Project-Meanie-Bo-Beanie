using UnityEngine;

public class DetachScout : MonoBehaviour
{
    void Awake() { 
        GameObject Scout = GameObject.Find("PERSISTENTOBJECTS").transform.Find("Scout").gameObject;
        Destroy(Scout);
        Destroy(this);
    }
    
}
