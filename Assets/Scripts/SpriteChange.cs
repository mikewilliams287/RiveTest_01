using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SpriteChange : MonoBehaviour
{

    public Image gunImage; // Reference to the Image component
    public Sprite[] gunSprites; // Array to hold the gun sprites

    void Start()
    {
        GetComponent<Image>().sprite = gunSprites[0]; // Set the initial sprite to the first one in the array
    }

    void Update()
    {

    }

    public void ChangeSprite(int index)
    {
        if (index >= 0 && index < gunSprites.Length) // Check if the index is within bounds
        {
            gunImage.sprite = gunSprites[index]; // Change the sprite to the one at the specified index
        }
        else
        {
            Debug.LogError("Index out of bounds for gun sprites array."); // Log an error if the index is invalid
        }
    }
}
