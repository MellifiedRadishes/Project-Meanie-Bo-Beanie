using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSaver : MonoBehaviour
{
    public Dictionary<string, Vector3> objectList = new Dictionary<string, Vector3>();
    public List<GameObject> objectsInScene = new List<GameObject>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Object.DontDestroyOnLoad(this.gameObject);
        SceneManager.GetActiveScene().GetRootGameObjects(objectsInScene);
        
        foreach (GameObject o in objectsInScene) {
            if (LayerMask.LayerToName(o.layer) == "ObjectLayer")
            {
                Debug.Log(o.ToString());
                objectList.Add(o.ToString(), o.transform.position);
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        //if (scenemanager.getactivescene().isloaded)
        //{
        //    foreach (keyvaluepair<string, Vector3> entry in objectlist)
        //    {
        //        gameobject obj = gameobject.find(entry.key);
        //        if (obj != null)
        //        {
        //            obj.transform.position = entry.value;
        //        }
        //    }
        //}
    }

}
