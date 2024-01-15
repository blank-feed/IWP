using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerSkills : MonoBehaviour
{
    public static PlayerSkills instance;

    int random;

    public enum AllSkills
    {
        Holy_Strike,
        Holy_Heal,
        Holy_Rage,
        Slip_Snip,
        Weakness_Policy,
        Agile_Snip,
        Arcane_Smash,
        Blood_Siphon,
        Recovery_Pool,
        Crippling_Volley,
        High_Shot,
        Barrage_Strike,
        Frenzy_Impact,
        Critical_Opportunity,
        Frail_Crush,
        Dragon_Beam,
        Winged_Buddy,
        Wyrm_Summon
    }

    public AllSkills S1;
    public AllSkills S2;
    public AllSkills S3;
    public Array skillValues;

    public bool self = false;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        skillValues = Enum.GetValues(typeof(AllSkills));
    }

    public string ProcessSkillName(AllSkills skill)
    {
        string originalName = skill.ToString();
        string[] parts = originalName.Split('_');
        return string.Join("\n", parts);
    }

    public void UseSkill(AllSkills skill_used)
    {
        random = UnityEngine.Random.Range(1, 7);
        Debug.Log("Dice Rolled : " + random);
        switch (skill_used)
        {
            case AllSkills.Holy_Strike:
                self = false;
                playermovetile.instance.movespaces = 1;
                BattleManager.instance.Damage = 30;
                BattleManager.instance.can_melee = true;
                break;
            case AllSkills.Holy_Heal:
                self = true;
                PlayerManager.instance.health += 10;
                if (PlayerManager.instance.health > PlayerManager.instance.maxHealth)
                {
                    PlayerManager.instance.health = PlayerManager.instance.maxHealth;
                }
                BattleManager.instance.bs = BattlingState.enemyturn;
                break;
            case AllSkills.Holy_Rage:
                self = false;
                playermovetile.instance.movespaces = 1;
                if (PlayerManager.instance.health < 50)
                {
                    BattleManager.instance.Damage = 40;
                }
                else
                {
                    BattleManager.instance.Damage = 20;
                }
                BattleManager.instance.can_melee = true;
                break;
            case AllSkills.Slip_Snip:
                self = false;
                playermovetile.instance.movespaces = 3;
                BattleManager.instance.can_melee = true;
                break;
            case AllSkills.Weakness_Policy:
                self = false;
                playermovetile.instance.movespaces = 1;
                BattleManager.instance.can_melee = true;
                break;
            case AllSkills.Agile_Snip:
                self = false;
                playermovetile.instance.movespaces = 1;
                BattleManager.instance.can_melee = true;
                break;
            case AllSkills.Arcane_Smash:
                self = false;
                break;
            case AllSkills.Blood_Siphon:
                self = false;
                break;
            case AllSkills.Recovery_Pool:
                self = false;
                break;
            case AllSkills.Crippling_Volley:
                self = false;
                break;
            case AllSkills.High_Shot:
                self = false;
                break;
            case AllSkills.Barrage_Strike:
                self = false;
                break;
            case AllSkills.Frenzy_Impact:
                self = false;
                BattleManager.instance.Damage = 8 * random;
                BattleManager.instance.can_melee = true;
                break;
            case AllSkills.Critical_Opportunity:
                self = false;
                if (random > 4)
                {
                    BattleManager.instance.Damage = 70;
                    BattleManager.instance.can_melee = true;
                }
                else
                {
                    BattleManager.instance.momentum++;
                    BattleManager.instance.bs = BattlingState.enemyturn;
                }
                break;
            case AllSkills.Frail_Crush:
                self = false;
                BattleManager.instance.Damage = 10 * BattleManager.instance.momentum;
                BattleManager.instance.can_melee = true;
                break;
            case AllSkills.Dragon_Beam:
                self = false;
                break;
            case AllSkills.Winged_Buddy:
                self = false;
                break;
            case AllSkills.Wyrm_Summon:
                self = false;
                playermovetile.instance.movespaces = 3;
                break;
            default:
                break;
        }
    }
}