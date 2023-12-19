using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

//ChatGPT helped, but I know the gist of how saving works.
[System.Serializable]
public class ObjectState
{
    public string sceneName; // Add scene name information
    public string objectName;
    public Vector3 position;
    public Quaternion rotation;
}
public class SceneStateManager : MonoBehaviour
{
    // Adjust the file name format to include the scene name
    private const string saveFileNameFormat = "saved_{0}.json";
    private void Start()
    {
        LoadSceneState(SceneManager.GetActiveScene().name);
    }
    public void SaveSceneState(string sceneName)
    {
        Debug.Log("SaveScene ran");

        ObjectState[] objectStates = CollectObjectStates(sceneName);

        // Check if objectStates is not null and not empty
        if (objectStates != null && objectStates.Length > 0)
        {
            Debug.Log("SaveScene was true, file was written");

        string json = JsonUtility.ToJson(objectStates);

        // Use the formatted file name with the scene name
        string saveFileName = string.Format(saveFileNameFormat, sceneName);

        File.WriteAllText(Path.Combine(Application.persistentDataPath, saveFileName), json);
        }
        else
        {
            Debug.LogWarning("No object states to save.");
        }
    }

    public void LoadSceneState(string sceneName)
    {
        // Use the formatted file name with the scene name
        string saveFileName = string.Format(saveFileNameFormat, sceneName);
        string filePath = Path.Combine(Application.persistentDataPath, saveFileName);

        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);

            // Deserialize and apply object states
            ObjectState[] objectStates = JsonUtility.FromJson<ObjectState[]>(json);
            ApplyObjectStates(objectStates);
        }
    }

    private ObjectState[] CollectObjectStates(string sceneName)
    {
        Debug.Log("CollectObj ran");
        List<GameObject> objectsInScene = new List<GameObject>();

        objectsInScene.AddRange(GameObject.FindGameObjectsWithTag("Player"));
        objectsInScene.AddRange(GameObject.FindGameObjectsWithTag("greenCreature"));
        objectsInScene.AddRange(GameObject.FindGameObjectsWithTag("yellowCreature"));
        objectsInScene.AddRange(GameObject.FindGameObjectsWithTag("redCreature"));

        ObjectState[] objectStates = new ObjectState[objectsInScene.Count];

        for (int i = 0; i < objectsInScene.Count; i++)
        {
            objectStates[i] = new ObjectState
            {
                sceneName = sceneName, // Set the scene name
                objectName = objectsInScene[i].name,
                position = objectsInScene[i].transform.position,
                rotation = objectsInScene[i].transform.rotation
            };
            Debug.Log("for loop in CollectObj ran");
        }

        return objectStates;
    }

    private void ApplyObjectStates(ObjectState[] objectStates)
    {
        foreach (var objectState in objectStates)
        {
            // Check if the object belongs to the current scene
            if (objectState.sceneName == SceneManager.GetActiveScene().name)
            {
                GameObject obj = GameObject.Find(objectState.objectName);
                if (obj != null)
                {
                    obj.transform.position = objectState.position;
                    obj.transform.rotation = objectState.rotation;
                }
            }
        }
    }
}

