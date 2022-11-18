using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.XR;
using UnityEngine.Events;

public enum FROLFscores
{

}

public class UiManager : Singleton<UiManager>
{
    public UnityEvent m_GameEnd;
    public UnityEvent m_OutOfBounds;

    [SerializeField]
    private TextMeshProUGUI ThrowCountText, CageDistanceText, GameOverText;
    [SerializeField]
    private Scrollbar Power;
    [SerializeField]
    private int ThrowCount;
    [SerializeField]
    private RectTransform EndCard;
    public int CageDistance;

    public bool gameEnded { get; private set;}

    private string[] scoreValues = new string[]
    {
    "Albatross",
    "Eagle",
    "Birdie",
    "Par",
    "Bogey",
    "Double Bogey",
    "TripleBogey"
    };

    private void Awake()
    {
        if (m_GameEnd == null)
            m_GameEnd = new UnityEvent();
        if (m_OutOfBounds == null)
            m_OutOfBounds = new UnityEvent();
    }

    private void Update()
    {
        ThrowCountText.text = ThrowCount.ToString();
        CageDistanceText.text = CageDistance.ToString();
    }
    public float powerValue()
    {
        return Power.value;
    }
    public void increaseThrowCount()
    {
        ThrowCount++;
        if(ThrowCount>7)
        {
            ThrowCount = 7;
            EndGame();
        }

    }
    public void EndGame()
    {
        m_GameEnd.Invoke();
        gameEnded = true;
        GameOverText.text = scoreValues[ThrowCount - 1];
        EndCard.gameObject.SetActive(true);
    }
}
