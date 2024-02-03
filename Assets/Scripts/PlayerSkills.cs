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
        Void_Jump,
        Crippling_Volley,
        High_Shot,
        Barrage_Strike,
        Frenzy_Impact,
        Critical_Opportunity,
        Frail_Crush,
        Dragon_Beam,
        Winged_Buddy,
        Dragon_Rush
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
            random = BattleManager.instance.RollDie(7);
            repeat = random;
        }
        else
        {
            random = repeat;
        }

        switch (skill_used)
        {
            case AllSkills.Holy_Strike:
                //if (PlayerManager.instance.mana >= 10)
                //{ 
                    range = Range.Melee;
                    playermovetile.instance.movespaces = 1;
                    BattleManager.instance.Damage = 100 + BattleManager.instance.deficiency; //30 + deficiency
                    BattleManager.instance.ManaToDeduct = 10;
                    BattleManager.instance.can_melee = true;
                //}
                //else
                //{
                //    BattleManager.instance.bs = BattlingState.playerturn;
                //}
                break;
            case AllSkills.Holy_Heal:
                range = Range.Self;
                if (PlayerManager.instance.maxHealth - PlayerManager.instance.health > 50)
                {
                    PlayerManager.instance.health += 20;
                }
                else
                {
                    PlayerManager.instance.health += 10;
                }
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
                    BattleManager.instance.Damage = 40 + BattleManager.instance.deficiency;
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
                BattleManager.instance.amplification++;
                BattleManager.instance.Damage = 25;
                if (BattleManager.instance.amplification == 4)
                {
                    BattleManager.instance.Damage *= 2;
                    BattleManager.instance.amplification = 0;
                }
                playermovetile.instance.movespaces = 5;
                BattleManager.instance.can_shoot = true;
                break;
            case AllSkills.Blood_Siphon:
                range = Range.Ranged;
                BattleManager.instance.amplification++;
                BattleManager.instance.Damage = 25;
                BattleManager.instance.LifeSteal = true;
                if (BattleManager.instance.amplification == 4)
                {
                    BattleManager.instance.Damage *= 2;
                    BattleManager.instance.amplification = 0;
                }
                playermovetile.instance.movespaces = 3;
                BattleManager.instance.can_shoot = true;
                break;
            case AllSkills.Void_Jump:
                if (!BattleManager.instance.VoidJumped)
                {
                    range = Range.Ranged;
                    BattleManager.instance.amplification++;
                    playermovetile.instance.movespaces = 3;
                    BattleManager.instance.Damage = 0;
                    if (BattleManager.instance.amplification == 4)
                    {
                        PlayerManager.instance.health += 25;
                        BattleManager.instance.amplification = 0;
                    }
                    if (PlayerManager.instance.health > PlayerManager.instance.maxHealth)
                    {
                        PlayerManager.instance.health = PlayerManager.instance.maxHealth;
                    }
                    BattleManager.instance.VoidJumped = true;
                    BattleManager.instance.can_dash = true;
                }
                else
                {
                    range = Range.Self;
                    BattleManager.instance.VoidJumped = false;
                    BattleManager.instance.bs = BattlingState.enemyturn;
                }
                break;
            case AllSkills.Crippling_Volley:
                range = Range.Ranged;
                BattleManager.instance.Damage = 0;
                playermovetile.instance.movespaces = 5;
                BattleManager.instance.CripplingAtk = true;
                BattleManager.instance.can_shoot = true;
                break;
            case AllSkills.High_Shot:
                BattleManager.instance.HighShotCount++;
                if (BattleManager.instance.HighShotCount >= 3)
                {
                    range = Range.Ranged;
                    playermovetile.instance.movespaces = 4;
                    BattleManager.instance.Damage = 90;
                    BattleManager.instance.can_shoot = true;
                }
                else
                {
                    range = Range.Self;
                    BattleManager.instance.bs = BattlingState.enemyturn;
                }
                break;
            case AllSkills.Barrage_Strike:
                range = Range.Ranged;
                BattleManager.instance.Damage = 24;
                playermovetile.instance.movespaces = 5;
                BattleManager.instance.can_shoot = true;
                break;
            case AllSkills.Frenzy_Impact:
                range = Range.Melee;
                playermovetile.instance.movespaces = 1;
                BattleManager.instance.Damage = 12 * random;
                BattleManager.instance.can_melee = true;
                break;
            case AllSkills.Critical_Opportunity:
                range = Range.Melee;
                playermovetile.instance.movespaces = 1;
                BattleManager.instance.Damage = 0;
                if (random > 3)
                {
                    BattleManager.instance.Damage = 70;
                }
                BattleManager.instance.can_melee = true;
                break;
            case AllSkills.Frail_Crush:
                range = Range.Melee;
                if (BattleManager.instance.bloodlust == 0)
                {
                    BattleManager.instance.Damage = 10;
                }
                else
                {
                    BattleManager.instance.Damage = 30 * BattleManager.instance.bloodlust;
                }
                BattleManager.instance.can_melee = true;
                break;
            case AllSkills.Dragon_Beam:
                range = Range.Ranged;
                BattleManager.instance.Damage = 10 * (BattleManager.instance.Dragon_Pals + 1);
                playermovetile.instance.movespaces = 4;
                BattleManager.instance.can_shoot = true;
                break;
            case AllSkills.Winged_Buddy:
                range = Range.Self;
                PlayerManager.instance.maxHealth += 5;
                PlayerManager.instance.health += 5;
                if (PlayerManager.instance.health > PlayerManager.instance.maxHealth)
                {
                    PlayerManager.instance.health = PlayerManager.instance.maxHealth;
                }
                BattleManager.instance.Dragon_Pals++;
                BattleManager.instance.bs = BattlingState.enemyturn;
                break;
            case AllSkills.Dragon_Rush:
                range = Range.Melee;
                if (BattleManager.instance.Dragon_Pals == 0)
                {
                    BattleManager.instance.Damage = 5;
                }
                else
                {
                    BattleManager.instance.Damage = 20 * BattleManager.instance.Dragon_Pals;
                }
                playermovetile.instance.movespaces = 1;
                BattleManager.instance.can_melee = true;
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
                str = "Heals youself based on missing health \nRange : Self";
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
                str = "Deals damage in a long line of direction \nRange : Distant";
                break;
            case AllSkills.Blood_Siphon:
                str = "Deals damage in a short line direction and heals \nRange : Distant";
                break;
            case AllSkills.Void_Jump:
                str = "Teleports away\nCannot use twice in a row\nRange : Distant";
                break;
            case AllSkills.Crippling_Volley:
                str = "Fires an arrow that cripples enemies \nRange : Distant";
                break;
            case AllSkills.High_Shot:
                str = "Use this skill 3 times to inflict massive damage \nRange : Distant";
                break;
            case AllSkills.Barrage_Strike:
                str = "Fires a barrage of arrows in a direction \nRange : Distant";
                break;
            case AllSkills.Frenzy_Impact:
                str = "Performs a random number of strikes based on the d6 roll \nRange : Melee";
                break;
            case AllSkills.Critical_Opportunity:
                str = "Inflict huge damage if you roll 4 or higher on the d6 roll \nRange : Melee";
                break;
            case AllSkills.Frail_Crush:
                str = "Spends all your momentum for one massive attack \nRange : Melee";
                break;
            case AllSkills.Dragon_Beam:
                str = "Commands your dragons to fire a dragon beam \nRange : Distant";
                break;
            case AllSkills.Winged_Buddy:
                str = "Summons a dragon to aid your fight\n\nadds 5 Max Health \nRange : Self";
                break;
            case AllSkills.Dragon_Rush:
                str = "Commands your dragons to charge at a nearby target \nRange : Melee";
                break;
            default:
                break;
        }

        return str;
    }
}