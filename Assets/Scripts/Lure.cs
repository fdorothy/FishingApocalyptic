using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lure : MonoBehaviour
{
    public Bobber bobber;
    public bool bitten = false;
    public Fish fish;

    public void Bite(Fish fish)
    {
        bitten = true;
        this.fish = fish;
    }

    public void ReleaseFish()
    {
        bitten = false;
        if (fish)
            fish.ReleaseFish();
        fish = null;
    }
}
