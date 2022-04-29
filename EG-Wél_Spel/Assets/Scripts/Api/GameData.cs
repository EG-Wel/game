[System.Serializable]
public class GameData
{
    public int GameDataId { get; set; }
    public int UserDataId { get; set; }
    public int HighScore { get; set; }
    public int LevelsCompleted { get; set; }
    public double TimePlayed { get; set; }
}
