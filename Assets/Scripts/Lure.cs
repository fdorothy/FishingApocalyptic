using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lure : MonoBehaviour
{
    public Bobber bobber;
    public bool bitten = false;

    public void Bite(Fish fish)
    {
        bobber.Bite(fish);
        bitten = true;
    }
}
