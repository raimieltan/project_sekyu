using Photon.Pun;

public class LeaderboardData
{
    public float currentScore;
    public float killCount;
    public float deathCount;
    public float killStreak;
    public string playerName;

    public LeaderboardData()
    {
        currentScore = 0;
        killCount = 0;
        deathCount = 0;
        killStreak = 0;
    }
}

