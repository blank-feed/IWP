using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkills : MonoBehaviour
{
    public static PlayerSkills instance;

    public enum allskills
    {
        //Paladin Skills
        Holy_Strike,
        Holy_Protection,
        Holy_Rage,

        //Rogue Skills
        Slip_Snip,
        Weakness_Policy,
        Agile_Snip,

        //Sorcerer Skills
        Arcane_Smash,
        Blood_Siphon,
        Recovery_Pool,

        //Ranger Skills
        Volley,
        High_Shot,
        Barrage,

        //Fighter
        Frenzy_Outrage,
        Critical_Opportunity,
        Frail_Crush,

        //Druid
        Dragonic_Beams,
        Winged_Buddy,
        Wrym_Wrath
    }

    public allskills skill1;
    public allskills skill2;
    public allskills skill3;

    int Skill1_Level = 1;
    int Skill2_Level = 1;
    int Skill3_Level = 1;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void PaladinSkillSet()
    {
        skill1 = allskills.Holy_Strike;
        skill2 = allskills.Holy_Protection;
        skill3 = allskills.Holy_Rage;
    }

    public void RogueSkillSet()
    {
        skill1 = allskills.Slip_Snip;
        skill2 = allskills.Weakness_Policy;
        skill3 = allskills.Agile_Snip;
    }

    public void SorcererSkillSet()
    {
        skill1 = allskills.Arcane_Smash;
        skill2 = allskills.Blood_Siphon;
        skill3 = allskills.Recovery_Pool;
    }

    public void RangerSkillSet()
    {
        skill1 = allskills.Volley;
        skill2 = allskills.High_Shot;
        skill3 = allskills.Barrage;
    }

    public void FighterSkillSet()
    {
        skill1 = allskills.Frenzy_Outrage;
        skill2 = allskills.Critical_Opportunity;
        skill3 = allskills.Frail_Crush;
    }

    public void DruidSkillSet()
    {
        skill1 = allskills.Dragonic_Beams;
        skill2 = allskills.Winged_Buddy;
        skill3 = allskills.Wrym_Wrath;
    }

    public void Use(allskills skillChosen)
    {
        switch (skillChosen)
        {
            case allskills.Holy_Strike:

                break;
            case allskills.Holy_Protection:

                break;
            case allskills.Holy_Rage:

                break;
            case allskills.Slip_Snip:

                break;
            case allskills.Weakness_Policy:

                break;
            case allskills.Agile_Snip:

                break;
            case allskills.Arcane_Smash:

                break;
            case allskills.Blood_Siphon:

                break;
            case allskills.Recovery_Pool:

                break;
            case allskills.Volley:

                break;
            case allskills.High_Shot:

                break;
            case allskills.Barrage:

                break;
            case allskills.Frenzy_Outrage:

                break;
            case allskills.Critical_Opportunity:

                break;
            case allskills.Frail_Crush:

                break;
            case allskills.Dragonic_Beams:

                break;
            case allskills.Winged_Buddy:

                break;
            case allskills.Wrym_Wrath:

                break;
            default:
                break;
        }
    }
}
