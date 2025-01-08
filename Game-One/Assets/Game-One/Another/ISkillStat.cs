public interface ISkillStat
{
    string SkillName { get; set; }
    string SkillIcon { get; set; }
    float SkillRange { get; set; }
    float SkillCooldown { get; set; }
    float SkillDamage { get; set; }
    float SkillRange { get; set; }

    void TakeDamage(int amount);
}
