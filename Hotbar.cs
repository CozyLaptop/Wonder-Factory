using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hotbar : MonoBehaviour
{
    //PlayerInventory playerInventory;
    Item currentItem;
    Hotbar hotbar;
    Sprite newImage;
    int children;

    void Start()
    {
        //playerInventory = FindObjectOfType<PlayerInventory>();
        children = transform.childCount;
        setupInventory();

    }

    void setupInventory()
    {
        //for (int i = 0; i < 13; ++i)
        //{
            //GameObject slot = gameObject.transform.GetChild(i).gameObject;
    //        GameObject slotbarItem1 = slot.transform.GetChild(1).gameObject;
    //        GameObject quantity = child.transform.GetChild(2).gameObject;
            //Image childImgComponent = slotbarItem1.gameObject.GetComponentInChildren<Image>();
    //        Text textComponent = quantity.gameObject.GetComponentInChildren<Text>();
    //        // currentItem = playerController.playerInventory[i];
    //        newImage = currentItem.sprite;
    //        childImgComponent.sprite = newImage;

    //        //  int myint = playerController.playerInventoryAmounts[currentItem];

    //        bool isTool = currentItem.isTool;

    //        if (!isTool == true)
    //        {
    //            // textComponent.text = ("" + myint);
    //        }
        }
}
