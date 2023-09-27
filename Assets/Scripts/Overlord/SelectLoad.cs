using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class SelectLoad : MonoBehaviour
{
    public string levelName;
    public string zoneName;
    [SerializeField] private TMP_Text selectedText;

    [SerializeField] private GameObject levels;
    [SerializeField] private GameObject map;
    private void Update()
    {
        selectedText.text = $"Selected: {LevelData.levelToLoad} | {LevelData.zoneToLoad}";  
    }
    public void SelectLevel()
    {
            LevelData.levelToLoad = levelName;
    }
    public void SelectZone()
    {
            LevelData.zoneToLoad = zoneName;
    }
    public void LoadZone()
    {
        if (LevelData.zoneToLoad != "")
        {
            SceneManager.LoadScene(LevelData.zoneToLoad);
        }
    }
    public void SwapMenu()
    {
        if (levels.activeSelf)
        {
            levels.SetActive(false);
            map.SetActive(true);
        }
        else
        {
            levels.SetActive(true);
            map.SetActive(false);
        }
    }
}
