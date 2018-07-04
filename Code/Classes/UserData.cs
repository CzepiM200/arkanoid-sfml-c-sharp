using System;
using System.IO;

namespace ArkanoidCS
{
    class UserData
    {
        #region Public Fields
        public int coins { get;  set; } = 2000;
        public int points { get;  set; } = 1000;
        public bool[] equipment = new bool[6];
        public int[] stats = new int[GameProperties.AmountOfStats];
        #endregion
        #region Construcor
        public UserData()
        {
            LoadFromData();
            LoadStatsFromData();
        }
        #endregion
        #region Public Methods
        public void LoadFromData()
        {
            try
            {
                FileStream fs = new FileStream("Save\\UserData.txt", FileMode.Open, FileAccess.Read);
                StreamReader sr = new StreamReader(fs);
                int.TryParse(sr.ReadLine(), out int temp);
                coins = temp;
                int.TryParse(sr.ReadLine(), out temp);
                points = temp;
                bool temp2;
                for (int i = 0; i < 6; i++)
                {
                    bool.TryParse(sr.ReadLine(), out temp2);
                    equipment[i] = temp2;
                }
                equipment[5] = true;
                fs.Close();
                sr.Close();
            }
            catch (Exception e)
            {
                coins = -1;
                points = -1;
                for (int i = 0; i < 6; i++) equipment[i] = true;
                Console.WriteLine(e);
            }
        }

        public void SaveDataToFile()
        {
            FileStream fs = new FileStream("Save\\UserData.txt", FileMode.Truncate, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs);
            sw.WriteLine(coins.ToString());
            sw.WriteLine(points.ToString());
            for (int i = 0; i < 6; i++)
            {
                if (equipment[i]) sw.WriteLine("true");
                else sw.WriteLine("false");
            }
            sw.WriteLineAsync("KONIEC PLIKU");
            sw.Close();
            fs.Close();
        }

        public void LoadStatsFromData()
        {
            try
            {
                FileStream fs = new FileStream("Save\\StatsData.txt", FileMode.Open, FileAccess.Read);
                StreamReader sr = new StreamReader(fs);
                for (int i = 0; i < GameProperties.AmountOfStats; i++)
                {
                    int.TryParse(sr.ReadLine(), out stats[i]);
                }

                fs.Close();
                sr.Close();
            }
            catch (Exception e)
            {
                for (int i = 0; i < GameProperties.AmountOfStats; i++) stats[i] = -1;
                Console.WriteLine(e);
            }
        }

        public void SaveStatsDataToFile()
        {
            FileStream fs = new FileStream("Save\\StatsData.txt", FileMode.Truncate, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs);
            for (int i = 0; i < GameProperties.AmountOfStats; i++)
            {
                sw.WriteLine(stats[i].ToString());
            }
            sw.WriteLineAsync("KONIEC PLIKU");
            sw.Close();
            fs.Close();
        }

        public void AddStat(int value)
        {
            for (int i = 0; i < GameProperties.AmountOfStats; i++)
            {
                if (stats[i] < value)
                {
                    for (int j = ((int)GameProperties.AmountOfStats - 1); j > i; j--)
                    {
                        stats[j] = stats[j-1];
                    }
                    stats[i] = value;
                    break;
                }
            }
        }

        public bool AddToEquiment(Rackets type)
        {
            switch (type)
            {
                case Rackets.FAST:
                    if (coins >= 100)
                    {
                        equipment[0] = true;
                        coins -= 100;
                        return true;
                    }
                    return false;
                case Rackets.LONG:
                    if (coins >= 100)
                    {
                        equipment[1] = true;
                        coins -= 100;
                        return true;
                    }
                    return false;
                case Rackets.STRONG:
                    if (coins >= 100)
                    {
                        equipment[2] = true;
                        coins -= 100;
                        return true;
                    }
                    return false;
                case Rackets.LUCKY:
                    if (coins >= 300)
                    {
                        equipment[3] = true;
                        coins -= 300;
                        return true;
                    }
                    return false;
                case Rackets.MASTER:
                    if (coins >= 1000)
                    {
                        equipment[4] = true;
                        coins -= 1000;
                        return true;
                    }
                    return false;
                default:
                    return false;
            }
        }

        public bool CheckEquipmentStatus(Rackets type)
        {
            switch (type)
            {
                case Rackets.BASIC:
                    return true;
                case Rackets.FAST:
                    return equipment[0];
                case Rackets.LONG:
                    return equipment[1];
                case Rackets.STRONG:
                    return equipment[2];
                case Rackets.LUCKY:
                    return equipment[3];
                case Rackets.MASTER:
                    return equipment[4];
                    default:
                        return false;
            }
        }

        public void SetToDefault()
        {
            coins = 0;
            points = 0;
            for (int i = 0; i < 6; i++) equipment[i] = false;
        }
#endregion
    }
}
