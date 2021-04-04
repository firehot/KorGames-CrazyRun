using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Space(20)]    
    [SerializeField] private TextMeshProUGUI collectedDiamondCountText;
    [SerializeField] private TextMeshProUGUI inGameDiamondCount;

    [Space(20)]    
    [SerializeField ]private GameObject diamondIcon;
    private int endGameCollectedDiamondCount = 0;//level based amount
    private int diamondCount = 0;

    [HideInInspector]
    public bool isGameStarted = false;

    public PlayerData userData;

    public static GameManager instance;

    private void Awake()
    {
        Application.targetFrameRate = 60;
        if (Application.isMobilePlatform)
            QualitySettings.vSyncCount = 0;
    }
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            GameObject.Destroy(gameObject);
        }

        DOTween.Init();

        userData = saveLoad.LoadPlayer();
        diamondCount = userData.diamondCount;
        inGameDiamondCount.text = userData.diamondCount.ToString();


    }

    public void CollectDiamond()
    {
        //Debug.Log("Added +1 diamond");
        diamondCount++;
        inGameDiamondCount.text = diamondCount.ToString();
        diamondIcon.transform.DOPunchScale(new Vector3(0.1f, 0.1f, 0.1f), 0.2f, 0, 0).SetEase(Ease.Linear).SetAutoKill(true);

        endGameCollectedDiamondCount++;

        userData.diamondCount = diamondCount;
        saveLoad.SavePlayer(userData);
    }

    public void StartGame()
    {
        UIManager.instance.DisableMainMenu();
        GameManager.instance.isGameStarted = true;
        PlayerController.instance.StartRunning();
    }

    public void EndGame()
    {
        collectedDiamondCountText.text ="+"+ endGameCollectedDiamondCount.ToString();
        userData.diamondCount = diamondCount;
        inGameDiamondCount.text = userData.diamondCount.ToString();
        saveLoad.SavePlayer(userData);

        UIManager.instance.EnableEndGameMenu();
    }

    public void ResetParameters()
    {
        endGameCollectedDiamondCount = 0;
        UIManager.instance.DisableFailMenu();
        UIManager.instance.DisableEndGameMenu();
        UIManager.instance.EnableMainMenu();
        GameManager.instance.isGameStarted = false;
    }

    public void EnableFailedCanvas()
    {
        UIManager.instance.EnableFailMenu();
    }
}
