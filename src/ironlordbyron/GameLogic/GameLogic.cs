﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

public class GameLogic
{
    public GameLogic()
    {
    }

    public IEnumerable<AbstractCard> GetSelectableCardsFromScience()
    {
        return new List<AbstractCard>(); // TODO
    }
    // Returns the list of attackable regions, which should be all regions VISIBLE that are NOT in player territory.

}
