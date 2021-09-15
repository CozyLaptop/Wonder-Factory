using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Rock1Script : MonoBehaviour
{
	public int currentLevel;
	public int currentHealth;
	public int maxHealth;
	public HarvestableObjects harvestableObject;
	private SpriteRenderer objectSpriteRenderer;
	private MapManager mapManager;
	private DroppedItemPhysics droppedItemPhysics;
	//public HealthBar healthBar;

	void Awake()
	{
		mapManager = FindObjectOfType<MapManager>();
		//itemDatabase = FindObjectOfType<ItemDatabase>();
		maxHealth = 20;
		currentHealth = maxHealth;
		objectSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
		droppedItemPhysics = gameObject.GetComponent<DroppedItemPhysics>();

	}
	private void OnMouseDown()
	{
		TakeDamage(2);
		Debug.Log("Took 2 Damage");
		if (currentHealth > 10 && currentHealth <= 16)
		{
			objectSpriteRenderer.sprite = mapManager.harvestableObjects[0].sprite2;
		}
		if (currentHealth > 6 && currentHealth <= 10)
		{
			objectSpriteRenderer.sprite = mapManager.harvestableObjects[0].sprite3;
		}
		if (currentHealth > 0 && currentHealth <= 6)
		{
			objectSpriteRenderer.sprite = mapManager.harvestableObjects[0].sprite4;
		}
		void TakeDamage(int damage)
		{
			currentHealth -= damage;
			if (currentHealth <= 0)
			{
				DropItem(); //100% chance to  drop
				var dropItemID = UnityEngine.Random.Range(0, 100); //second drop id
				if (dropItemID >= 0 && dropItemID <= 50) //check drop id
                {
					DropItem(); //second drop
					var thirdDropItemID = UnityEngine.Random.Range(0, 100); //create a third drop id
					if (thirdDropItemID >= 0 && dropItemID <= 30)
					{
						DropItem();
					}
				}

				Destroy(gameObject);
				
			}
			//healthBar.SetHealth(currentHealth);
		}
		void DropItem()
		{
			var pos = transform.position;
			pos.y = 0.1f;
			//pos.x = pos.x + 0.5f;
			//pos.z = pos.z + 0.5f;
			//Instantiate(harvestableObject.droppableItems[0], transform.position + new Vector3 (transform.position.x, transform.position.y , transform.position.z), Quaternion.identity)
			Instantiate(harvestableObject.droppableItems[0], pos, Quaternion.identity);
			//Instantiate(droppedItemPhysics.item.droppedItem, transform.position, Quaternion.identity);
		}
		
    }
}
