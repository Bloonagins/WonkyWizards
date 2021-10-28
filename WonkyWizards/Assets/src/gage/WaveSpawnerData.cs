public class WaveSpawnerData
{
    private string waveName;
    private float spawnInterval;
    private int total_GoblinGrunt;
    private int total_GoblinBeserker;
    private int total_GoblinArcher;
    private int total_GoblinAssassin;
    private int total_GoblinGiant;

    //private int total_bosses;
    //private string[] bossType;

    private int total_Enemies;

    public WaveSpawnerData(string waveName, float spawnInterval, int total_GoblinGrunt, int total_GoblinBeserker, int total_GoblinArcher, int total_GoblinAssassin, int total_GoblinGiant)
    {
        this.waveName = waveName;
        this.spawnInterval = spawnInterval;
        this.total_GoblinGrunt = total_GoblinGrunt;
        this.total_GoblinBeserker = total_GoblinBeserker;
        this.total_GoblinArcher = total_GoblinArcher;
        this.total_GoblinAssassin = total_GoblinAssassin;
        this.total_GoblinGiant = total_GoblinGiant;
        this.total_Enemies = total_GoblinGrunt + total_GoblinBeserker + total_GoblinArcher + total_GoblinAssassin + total_GoblinGiant;
    }

    /*public void SetTotal_Bosses(int num)
    {
        this.total_bosses = num;
    }
    public void SetBossType(string name)
    {
        this.bossType = num;
    }
    */

}
