using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SpaseShooter
{
    public class LevelSequenceController : SingletonBase<LevelSequenceController>
    {
        //time segment
        [SerializeField] private int m_GoldTime;
        [SerializeField] private int m_SilverTime;
        [SerializeField] private int m_BronzeTime;

        public static string MainMenuSceneNickname = "MapLevel";
        public Episode CurrentEpisode { get; private set; }

        public int CurrentLevel { get; private set; }

        public bool LastLevelResult { get; private set; }

        //public PlayerStatistics LevelStatisticts { get; private set; }
        public static SpaceShip PlayerShip { get; set; }

        public void StartEpisode(Episode episode)
        {
            CurrentEpisode = episode;
            CurrentLevel = 0;

            ////LevelStatisticts = new PlayerStatistics();
            //LevelStatisticts.Reset();

            SceneManager.LoadScene(episode.Levels[CurrentLevel]);
        }

        public void RestartLevel()
        {
            //SceneManager.LoadScene(CurrentEpisode.Levels[CurrentLevel]);
            
            if(TDPlayer.Instance.HitPoints <= 0)
            {
                print("You lose!");
                SceneManager.LoadScene(0);
            }
        }

        public void FinishCurrentLevel(bool success)
        {
            //CalculateResult();
            //GetScoreMultiplier(LevelStatisticts);

            ResultPanelController.Instance.Show(success);
        }

        public void AdvanceLevel()
        {
            CurrentLevel++;

            if (CurrentEpisode.Levels.Length <= CurrentLevel)
            {
                SceneManager.LoadScene(MainMenuSceneNickname);
            }
            else
            {
                SceneManager.LoadScene(CurrentEpisode.Levels[CurrentLevel]);
            }
        }

        //private void CalculateResult()
        //{
        //    LevelStatisticts.score = Player.Instance.Score;
        //    LevelStatisticts.kills = Player.Instance.NumKill;
        //    LevelStatisticts.time = (int)LevelController.Instance.LevelTime;
        //}

        //public void GetScoreMultiplier(PlayerStatistics levelResults)
        //{
        //    if (LevelController.Instance.LevelTime <= m_GoldTime)
        //    {
        //        levelResults.score = levelResults.score * 2;
        //    }

        //    if (LevelController.Instance.LevelTime <= m_GoldTime && LevelController.Instance.LevelTime >= m_SilverTime)
        //    {
        //        levelResults.score = (int)(levelResults.score * 1.5f);
        //    }

        //    if (LevelController.Instance.LevelTime <= m_SilverTime && LevelController.Instance.LevelTime >= m_BronzeTime)
        //    {
        //        levelResults.score = (int)(levelResults.score * 0.5f);
        //    }
        //}
    }
}

