﻿using UnityEngine;
using System.Collections;

public class Rogue : AttackUnits
{

    // Use this for initialization
    public override void Start()
    {
        mana = 10;
        //Dir = inDir;
        HP = 100.0f;
        Def = 2.0f;
        Speed = 1.5f;
    }

    // Update is called once per frame
    public override void Update()
    {
        //Move
    }
}
