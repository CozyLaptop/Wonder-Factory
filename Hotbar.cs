using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hotbar : MonoBehaviour{
    //PlayerInventory playerInventory;
    Item currentItem;
    Hotbar hotbar;
    Sprite newImage;
    int children;

// Attack this script to hotbar UI to grab all children.
    void Start(){
        children = transform.childCount;
    }
}
