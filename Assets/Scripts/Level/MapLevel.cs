using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SpaseShooter;
using System;

public class MapLevel : MonoBehaviour
{

    [SerializeField] private Text text;
    [SerializeField] private RectTransform resultPanel;
    [SerializeField] private Episode m_episode;

    public bool IsComplited { get { return gameObject.activeSelf && resultPanel.gameObject.activeSelf; } }

    public void LoadLevel()
    {
        LevelSequenceController.Instance.StartEpisode(m_episode);
    }

    public void Initialize()
    {
        var score = MapCompletion.Instance.GetEpisodeScore(m_episode);
        resultPanel.gameObject.SetActive(score > 0);
        text.text = $"{score}/3";


        //for (int i = 0; i < score; i++)
        //{
            
        //}
    }
}
