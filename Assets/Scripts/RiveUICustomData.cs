using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rive;
using Rive.Components;

public class RiveUICustomData : MonoBehaviour
{

    // Start is called before the first frame update

    public List<string> weaponNames = new List<string>(); // List to hold the names
    public List<string> weaponDescriptions = new List<string>(); // List to hold the descriptions

    public Sprite[] weaponSprites; // Array to hold the weapon sprites

    public List<int> weaponPowers = new List<int>();
    public List<int> weaponFireRates = new List<int>();
    public int weaponPower = 50; // Variable to hold the weapon power
    public int weaponFireRate = 60; // Variable to hold the weapon range


    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
