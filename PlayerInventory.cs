using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour{
    Item item;
    Dictionary<Item, int> playerInventory;
    private List<Item> playerInventoryList;
    Hotbar hotbar;

    public List<Item> PlayerInventoryList{
        get { return playerInventoryList; }
    }

    void Start(){
        hotbar = FindObjectOfType<Hotbar>();
        InitializeInventory();
    }
    void InitializeInventory(){
        playerInventory = new Dictionary<Item, int>();
        playerInventoryList = new List<Item>();
    }
    private void OnTriggerEnter(Collider other){
        //if collided object has dropped item physics
        if (other.gameObject.GetComponent<DroppedItemPhysics>() != null){
            PickupItem(other);
        }
        Destroy(other.gameObject);
    }
    void PickupItem(Collider other){
        //gets scriptable object
        item = other.gameObject.GetComponent<DroppedItemPhysics>().item;

        //if inventory does not have item
        if (!playerInventory.ContainsKey(item)){
            //add item to dictionary with 1 amount
            playerInventory.Add(item, 1);
            //add item to list of items owned
            playerInventoryList.Add(item);
            Debug.Log("Added " + playerInventoryList[playerInventoryList.Count - 1]);
            int index = playerInventoryList.IndexOf(item);
            if (index < 13){
                AddToHotbar(item, index);
            }
        }
        else{
            //if player has item, add 1 to amount
            playerInventory[item]++;
            UpdateHotbarAmount(item);
            Debug.Log("You have " + playerInventory[item] + " " + item);
        }
    }

    void AddToHotbar(Item item, int index){
        Image slotimage = hotbar.transform.GetChild(index).GetChild(1).gameObject.GetComponent<Image>();
        slotimage.sprite = item.sprite;
    }
    void UpdateHotbarAmount(Item item){
        int index = playerInventoryList.IndexOf(item);
        Text quantity = hotbar.transform.GetChild(index).GetChild(2).gameObject.GetComponent<Text>();
        quantity.text = playerInventory[item].ToString();
    }
}
