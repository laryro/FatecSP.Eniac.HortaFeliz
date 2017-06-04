using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seed
{
    public enum Needs { NEED_WATER, NEED_SUN, NEED_CUT, NEED_NUTRIENTS };
    private List<Needs> needs;

    public Seed()
    {
        needs = new List<Needs>();

        this.AddNeed(Needs.NEED_SUN);
        this.AddNeed(Needs.NEED_SUN);
        this.AddNeed(Needs.NEED_CUT);
        this.AddNeed(Needs.NEED_WATER);
    }

    public void AddNeed(Needs newNeed)
    {
        needs.Add(newNeed);
    }

    public List<Needs> GetNeeds()
    {
        return needs;
    }
}