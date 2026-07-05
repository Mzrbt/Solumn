using System;
using System.IO;

namespace Solumn.Managers
{
    public class ScoreManager
    {
        public int Score { get; private set; }

        public void SaveScore(int score)
        {
            if (score > Score)
            {
                Score = score;
                string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/Solumn/";
                Directory.CreateDirectory(path);
                StreamWriter sw = new StreamWriter(path + "score.txt");
                sw.WriteLine(score);
                sw.Close();
            }
        }

        public void LoadScore()
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/Solumn/score.txt";
            if (!File.Exists(path))
            {
                return;
            }
            StreamReader sr = new StreamReader(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/Solumn/score.txt");
            string scoreString = sr.ReadLine();
            if (int.TryParse(scoreString, out int loadedScore))
            {
                Score = loadedScore;
            }
            sr.Close();
        }
    }
}