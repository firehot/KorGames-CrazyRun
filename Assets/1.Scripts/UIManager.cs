using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Space(20)]
    [SerializeField ]private GameObject mainMenuCanvas;
    [SerializeField] private GameObject endGameCanvas;
    [SerializeField] private GameObject failCanvas;
    [SerializeField] private GameObject debugCanvas;


    [Space(20)]
    [SerializeField] private Transform ICO_Hand;

    public static UIManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            GameObject.Destroy(this);
        }
    }

    private void Start()
    {
        ICO_Hand.DOScale(1.05f, 0.4f).SetEase(Ease.OutQuad).SetLoops(-1, LoopType.Yoyo);
    }

    public void DisableMainMenu()
    {
        mainMenuCanvas.SetActive(false);
    }

    public void EnableMainMenu()
    {
        mainMenuCanvas.SetActive(true);
    }

    public void EnableEndGameMenu()
    {
        endGameCanvas.SetActive(true);
    }

    public void DisableEndGameMenu()
    {
        endGameCanvas.SetActive(false);
    }

    public void EnableFailMenu()
    {
        failCanvas.SetActive(true);
    }

    public void DisableFailMenu()
    {
        failCanvas.SetActive(false);
    }

    public void EnableDisableDebugCanvas()
    {
        debugCanvas.SetActive(!debugCanvas.activeSelf);
        mainMenuCanvas.SetActive(!debugCanvas.activeSelf);
    }

}
