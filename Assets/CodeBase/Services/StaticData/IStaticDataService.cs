namespace CodeBase.Services.StaticData
{
    public interface IStaticDataService
    {
        void LoadMonsters();
        MonsterStaticData ForMonster(MonsterTypeId monsterType);
    }
}