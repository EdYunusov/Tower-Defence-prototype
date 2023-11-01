using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpaseShooter;

public class MapCompletion : SingletonBase<MapCompletion>
{
    [Serializable]

    private class EpisodeScore
    {
        public Episode episode;
        public int score;
    }

    public static void SaveEpisodeResult(int levelScore)
    {
        Instance.SaveResult(LevelSequenceController.Instance.CurrentEpisode, levelScore);
    }


    [SerializeField] private EpisodeScore[] comletionData;
  
    public int TotalScore { private set; get; }

    private new void Awake()
    {
        base.Awake();
        Saver<EpisodeScore[]>.TryLoad(filename, ref comletionData);
        foreach (var episodeScore in comletionData)
        {
            TotalScore += episodeScore.score;
        }
    }

    public int GetEpisodeScore(Episode m_episode)
    {
        foreach(var data in comletionData)
        {
            if(data.episode == m_episode)
            {
                return data.score;
            }
        }
        return 0;
    }

    private void SaveResult(Episode currentEpisode, int levelScore)
    {
        foreach(var item in comletionData)
        {
            if(item.episode == currentEpisode)
            {
                if(levelScore > item.score)
                {
                    Instance.TotalScore += levelScore - item.score;
                    item.score = levelScore;

                    Saver<EpisodeScore[]>.Save(filename, comletionData);
                }
            }
        }
    }

    public const string filename = "completion.dat";

    public static void ResetSaveData()
    {
        FileHandler.Reset(filename);
    }
}
