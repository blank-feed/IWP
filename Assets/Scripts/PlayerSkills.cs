using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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

    public enum Range
    {
        Self,
        Melee,
        Ranged
    }

    public AllSkills S1;
    public AllSkills S2;
    public AllSkills S3;

    public Sprite S1_Img;
    public Sprite S2_Img;
    public Sprite S3_Img;

    public Sprite[] Boon_Img;

    public Array skillValues;

    //public bool self = false;
    public Range range;

    public int repeat = -1;

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
        return string.Join(" ", parts);
    }

    public void UseSkill(AllSkills skill_used)
    {
        if (repeat == -1)
        {
            random = UnityEngine.Random.Range(1, 7);
            Debug.Log("Dice Rolled : " + random);
            repeat = random;
        }
        else
        {
            random = repeat;
        }
        
        switch (skill_used)
        {
            case AllSkills.Holy_Strike:
                range = Range.Melee;
                playermovetile.instance.movespaces = 1;
                BattleManager.instance.Damage = 30 + BattleManager.instance.deficiency;
                BattleManager.instance.can_melee = true;
                break;
            case AllSkills.Holy_Heal:
                range = Range.Self;
                PlayerManager.instance.health += 10;
                if (PlayerManager.instance.health > PlayerManager.instance.maxHealth)
                {
                    PlayerManager.instance.health = PlayerManager.instance.maxHealth;
                }
                BattleManager.instance.bs = BattlingState.enemyturn;
                break;
            case AllSkills.Holy_Rage:
                range = Range.Melee;
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
                range = Range.Melee;
                BattleManager.instance.Damage = 20 + (BattleManager.instance.confiscation * random);
                playermovetile.instance.movespaces = 3;
                BattleManager.instance.can_dash = true;
                break;
            case AllSkills.Weakness_Policy:
                range = Range.Melee;
                playermovetile.instance.movespaces = 1;
                BattleManager.instance.can_melee = true;
                break;
            case AllSkills.Agile_Snip:
                range = Range.Melee;
                BattleManager.instance.Damage = 40 + (BattleManager.instance.confiscation * random);
                playermovetile.instance.movespaces = 1;
                BattleManager.instance.can_dash = true;
                break;
            case AllSkills.Arcane_Smash:
                range = Range.Ranged;
                break;
            case AllSkills.Blood_Siphon:
                range = Range.Ranged;
                break;
            case AllSkills.Recovery_Pool:
                range = Range.Melee;
                break;
            case AllSkills.Crippling_Volley:
                range = Range.Ranged;
                break;
            case AllSkills.High_Shot:
                range = Range.Ranged;
                break;
            case AllSkills.Barrage_Strike:
                range = Range.Ranged;
                break;
            case AllSkills.Frenzy_Impact:
                range = Range.Melee;
                BattleManager.instance.Damage = 8 * random;
                BattleManager.instance.can_melee = true;
                break;
            case AllSkills.Critical_Opportunity:
                range = Range.Melee;
                playermovetile.instance.movespaces = 1;
                BattleManager.instance.Damage = 0;
                if (random > 4)
                {
                    BattleManager.instance.Damage = 70;
                    BattleManager.instance.momentum++;
                }
                BattleManager.instance.can_melee = true;
                break;
            case AllSkills.Frail_Crush:
                range = Range.Melee;
                BattleManager.instance.Damage = 10 * BattleManager.instance.momentum;
                BattleManager.instance.can_melee = true;
                break;
            case AllSkills.Dragon_Beam:
                range = Range.Ranged;
                break;
            case AllSkills.Winged_Buddy:
                range = Range.Ranged;
                break;
            case AllSkills.Wyrm_Summon:
                range = Range.Ranged;
                playermovetile.instance.movespaces = 3;
                break;
            default:
                break;
        }
    }

    public string GetSkillDescription(AllSkills skill)
    {
        string str = "";
        switch (skill)
        {
            case AllSkills.Holy_Strike:
                str = "Deals damage in a single direction \nRange : Melee";
                break;
            case AllSkills.Holy_Heal:
                str = "Heals yourself \nRange : Self";
                break;
            case AllSkills.Holy_Rage:
                str = "Deals more damage the lesser your health in a single direction \nRange : Melee";
                break;
            case AllSkills.Slip_Snip:
                str = "Dashes through while dealing damage \nRange : Dash";
                break;
            case AllSkills.Weakness_Policy:
                str = "Steals attack from target \nRange : Melee";
                break;
            case AllSkills.Agile_Snip:
                str = "High damage short dash \nRange : Dash";
                break;
            case AllSkills.Arcane_Smash:
                str = "Deals damage in a single direction \nRange : Melee";
                break;
            case AllSkills.Blood_Siphon:
                str = "Deals damage in a single direction \nRange : Melee";
                break;
            case AllSkills.Recovery_Pool:
                str = "Deals damage in a single direction \nRange : Melee";
                break;
            case AllSkills.Crippling_Volley:
                str = "Deals damage in a single direction \nRange : Melee";
                break;
            case AllSkills.High_Shot:
                str = "Deals damage in a single direction \nRange : Melee";
                break;
            case AllSkills.Barrage_Strike:
                str = "Deals damage in a single direction \nRange : Melee";
                break;
            case AllSkills.Frenzy_Impact:
                str = "Strikes a random amount of times (Momentum increases) \nRange : Melee";
                break;
            case AllSkills.Critical_Opportunity:
                str = "Have a chance to deal huge amount of damage (Momentum increases) \nRange : Melee";
                break;
            case AllSkills.Frail_Crush:
                str = "The higher your Momentum, the higher the damage \nRange : Melee";
                break;
            case AllSkills.Dragon_Beam:
                str = "Deals damage in a single direction \nRange : Melee";
                break;
            case AllSkills.Winged_Buddy:
                str = "Deals damage in a single direction \nRange : Melee";
                break;
            case AllSkills.Wyrm_Summon:
                str = "Deals damage in a single direction \nRange : Melee";
                break;
            default:
                break;
        }

        return str;
    }
}