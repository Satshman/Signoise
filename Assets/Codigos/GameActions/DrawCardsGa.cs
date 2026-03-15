using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawCardsGa : GameAction
{
    public int Amount {  get; set; }
    public DrawCardsGa(int amount)
    {
        Amount = amount;
    }
}
