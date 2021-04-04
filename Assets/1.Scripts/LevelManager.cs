using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public List<Levels> levels;

    [Space(20)]
    [SerializeField]private List<GameObject> platforms;
    [SerializeField] private GameObject finalPlatform;
    private Diamond[] diamonds;

    private GameObject player;

    //Platform Parameters
    private List<GameObject> spawnedPlatforms =  new List<GameObject>();
    private float platformStartOffset = -20f;//first platform position
    private float platformLength = 20f; //length of each platform
    private float platformDisableDistance = 40;//last platform will be disabled
    private float lastPlatformPositionZ=0;
    private float levelLength = 200;
    private int maxActivePlatformCount = 6;

    private GameObject tempPlatform;

    private int currentLevelIndex = 0;
    private bool isLevelCompleted = false; //Bonus Stage'e gelince true oluyor
    

    [Space(20)]
    public GameObject confetti;

    public static LevelManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        EnableLevel();
        SetPlatforms();
    }

    private void SetPlatforms()
    {
        lastPlatformPositionZ = 0;
        platformDisableDistance = 40;
        lastPlatformPositionZ += platformStartOffset;
        spawnedPlatforms.Clear();
        finalPlatform.SetActive(false);

        for (int i = 0; i < platforms.Count; i++)
        {
            platforms[i].SetActive(false);
        }

        for (int i = 0; i < maxActivePlatformCount; i++)
        {
            tempPlatform = platforms.Find(x => !x.activeSelf);
            tempPlatform.transform.position = new Vector3(0, -1.65f, lastPlatformPositionZ);
            lastPlatformPositionZ += platformLength;
            tempPlatform.SetActive(true);
            spawnedPlatforms.Add(tempPlatform);
        }
    }

    private void EnableLevel()
    {
        currentLevelIndex = GameManager.instance.userData.currentLevel;

        for (int i = 0; i < levels.Count; i++)
        {
            levels[i].level.SetActive(false);
        }

        if (currentLevelIndex>=levels.Count)
        {
            currentLevelIndex = 0;
        }

        levels[currentLevelIndex].level.SetActive(true);
    }

    private void Update()
    {
        if (lastPlatformPositionZ < levelLength && player.transform.position.z >= platformDisableDistance) //Player transform
        {
            spawnedPlatforms[0].SetActive(false);
            spawnedPlatforms.RemoveAt(0);

            tempPlatform = platforms.Find(x => !x.activeSelf);
            tempPlatform.transform.position = new Vector3(0, -1.65f, lastPlatformPositionZ);
            spawnedPlatforms.Add(tempPlatform);
            tempPlatform.SetActive(true);

            lastPlatformPositionZ += platformLength;
            platformDisableDistance += platformLength;

        }

        if (lastPlatformPositionZ >= levelLength && !finalPlatform.activeSelf)
        {
            finalPlatform.transform.position = new Vector3(0, -1.65f, lastPlatformPositionZ);
            finalPlatform.SetActive(true);
        }
        
    }

    public void EnableEndGameDecorations()
    {
        currentLevelIndex++;
        if (currentLevelIndex>=levels.Count)
        {
            currentLevelIndex = 0;
        }

        GameManager.instance.userData.currentLevel = currentLevelIndex;
        saveLoad.SavePlayer(GameManager.instance.userData);
        UIManager.instance.EnableEndGameMenu();

        //Enable Confettis
        confetti.SetActive(true);

        GameManager.instance.isGameStarted = false;
    }

    public void NextLevel()
    {
        GameManager.instance.ResetParameters();

        SetPlatforms();
        EnableDiamonds();
        
        confetti.SetActive(false);
        PlayerController.instance.Reset();

        if (currentLevelIndex == 0)
        {
            levels[levels.Count - 1].level.SetActive(false);
        }
        else
        {
            levels[currentLevelIndex - 1].level.SetActive(false);
        }
        
        levels[currentLevelIndex].level.SetActive(true);
        
    }

    private void EnableDiamonds()
    {
        if (diamonds!=null)
        {
            Array.Clear(diamonds, 0, diamonds.Length);
        }
        
        diamonds = levels[currentLevelIndex].level.transform.GetComponentsInChildren<Diamond>();
        

        if (diamonds.Length == 0)
            return;

        for (int i = 0; i < diamonds.Length; i++)
        {
            diamonds[i].EnableDiamond();
        }
    }

    public float GetLevelLength()
    {
        return levelLength;
    }

    public float GetFinalPlatformMiddlePosition()
    {
        return finalPlatform.transform.position.z + 12;
    }

}

[System.Serializable]
public class Levels
{
    public string levelName;
    public GameObject level;
}
