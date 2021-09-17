using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Rock1Script : MonoBehaviour{
	public int currentLevel;
	public int currentHealth;
	public int maxHealth;
	public HarvestableObjects harvestableObject;
	private SpriteRenderer objectSpriteRenderer;
	private MapManager mapManager;
	private DroppedItemPhysics droppedItemPhysics;

	void Awake(){
		mapManager = FindObjectOfType<MapManager>();
		maxHealth = 20;
		currentHealth = maxHealth;
		objectSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
		droppedItemPhysics = gameObject.GetComponent<DroppedItemPhysics>();
	}
	private void OnMouseDown(){
		TakeDamage(2);
		Debug.Log("Took 2 Damage");
		if (currentHealth > 10 && currentHealth <= 16){
			objectSpriteRenderer.sprite = mapManager.harvestableObjects[0].sprite2;
		}
		if (currentHealth > 6 && currentHealth <= 10){
			objectSpriteRenderer.sprite = mapManager.harvestableObjects[0].sprite3;
		}
		if (currentHealth > 0 && currentHealth <= 6){
			objectSpriteRenderer.sprite = mapManager.harvestableObjects[0].sprite4;
		}
		void TakeDamage(int damage){
			currentHealth -= damage;
			if (currentHealth <= 0){
				DropItem(); //100% chance to  drop
				var dropItemID = UnityEngine.Random.Range(0, 100);
				if (dropItemID >= 0 && dropItemID <= 50){ //50% chance to drop another item
					DropItem(); //second drop
					var thirdDropItemID = UnityEngine.Random.Range(0, 100); //
					if (thirdDropItemID >= 0 && dropItemID <= 30){ //30% chance to drop third item
						DropItem();
					}
				}
				Destroy(gameObject);
			}
		}
		void DropItem(){
			var pos = transform.position;
			pos.y = 0.1f;
			Instantiate(harvestableObject.droppableItems[0], pos, Quaternion.identity);
		}
    }
}
