using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SpaceShooter
{
    public class LevelSequenceController : SingletonBase<LevelSequenceController>
    {
        public static string MainMenuScene = "main_menu";

        public Episode CurrentEpisode {  get; private set; }
        public int CurrentLevel { get; private set; }
        public static SpaceShip PlayerShip { get; set; }
        public bool LastLevelResult { get; private set; }
        public PlayerStatistics LevelStatistics { get; private set; }
        #region API
        public void StartEpisode(Episode e)
        {
            CurrentEpisode = e;
            CurrentLevel = 0;

            LevelStatistics = gameObject.AddComponent<PlayerStatistics>();
            LevelStatistics.Reset();


            SceneManager.LoadScene(e.Levels[CurrentLevel]);
        }

        public void RestartLevel()
        {
            LevelStatistics.Reset();


            SceneManager.LoadScene(CurrentEpisode.Levels[CurrentLevel]);
        }

        public void FinishCurrentLevel(bool success)
        {
            LastLevelResult = success;
            CalculateLevelStatistic();

            ResultPanelController.Instance.ShowResults(LevelStatistics, success);

            GameStatistics.Instance.AddKills(LevelStatistics.NumKills);
            GameStatistics.Instance.AddScore(LevelStatistics.Score);
            GameStatistics.Instance.AddTime(LevelStatistics.Time);
            if(AstronautsCounter.Instance != null)
            {
                GameStatistics.Instance.AddAstronauts(AstronautsCounter.Instance.Saved);
            }
        }

        public void AdvanceLevel()
        {
            LevelStatistics.Reset();

            CurrentLevel++;

            if (CurrentEpisode.Levels.Length <= CurrentLevel)
            {
                SceneManager.LoadScene(MainMenuScene);
            }
            else
            {
                SceneManager.LoadScene(CurrentEpisode.Levels[CurrentLevel]);
            }
        }
        #endregion
        
        private void CalculateLevelStatistic()
        {
            LevelStatistics.Score = Player.Instance.Score;
            LevelStatistics.NumKills = Player.Instance.NumKills;
            LevelStatistics.Time = (int)LevelController.Instance.LevelTime;
        }
    }

}
