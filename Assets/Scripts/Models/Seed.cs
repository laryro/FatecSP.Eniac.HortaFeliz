using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class Seed
{
    public enum Needs { NEED_WATER, NEED_SUN, NEED_CUT, NEED_NUTRIENTS };
    private List<Needs> needs;

    public Seed()
    {
        needs = new List<Needs>();
        for( int i = 0; i < 4; i++){
            AddNeed((Needs)Random.Range(0, 4));
        }
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