using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using Cinemachine;
using System.ComponentModel.Design;

public class MapManager : MonoBehaviour{
    public Grid grid;
    public Tilemap groundMap;
    public Tilemap objectMap;
    public Tilemap seedMap;
    public List<TileData> cropDatas;
    public List<GroundData> groundDatas;
    public Sprite treeSprite;
    public Sprite boulderSprite;
    public Sprite houseSprite;
    public List<HarvestableObjects> harvestableObjects;
    public Tile untilledSoilTile;
    public Tile tilledSoilTile;
    public Tile wateredSoilTile;
    public GameObject redBorder;

    float SpawnId;
    PlayerInventory playerInventory;
    Item currentItem;
    Hotbar hotbar;
    Vector3Int position;
    GameObject rock1;
    Vector3 redBorderEnabledAt;
    bool redBorderEnabled;
    int currentDay;
    private Dictionary<Vector3Int, bool> plantedTiles;
    private List<Vector3Int> wateredTiles;
    int activeHotbarSlot;

    public int CurrentDay { get => currentDay; set => currentDay = value; }
    public List<Vector3Int> WateredTiles { get => wateredTiles; set => wateredTiles = value; }

    void OnEnable(){
        EventManager.StartListening("nextDay", OnSleep);
        EventManager.StartListening("hotbarSelect", OnPressNum);
    }

    void OnDisable(){
        EventManager.StopListening("nextDay", OnSleep);
    }
    private void OnPressNum(Dictionary<string, object> message)
    {
        //set previous hotbar slot color to normal
        Image image = hotbar.transform.GetChild(activeHotbarSlot).gameObject.GetComponent<Image>();
        image.color = new Color32(224, 165, 94, 255);

        //get new hotbar slot selection
        var number = (int)message["number"];
        if (playerInventory.PlayerInventoryList.Count > number){
            currentItem = playerInventory.PlayerInventoryList[number];
        }
        else{
            currentItem = null;
        }
        image = hotbar.transform.GetChild(number).gameObject.GetComponent<Image>();
        image.color = new Color32(188, 129, 58, 255);
        activeHotbarSlot = number;
        print("you have equipped " + currentItem);
    }

// When you click the bed, activates event
    private void OnSleep(Dictionary<string, object> message){
        var amount = (int)message["amount"];
        currentDay += amount;
        TurnWateredToTilled();
        Debug.Log("the day is" + currentDay);
    }

// Replace all the watered grid items to unwaters
    private void TurnWateredToTilled(){
        foreach (var Vector3Int in wateredTiles){
            groundMap.SetTile(Vector3Int, tilledSoilTile);
        }
    }

    private void Start(){
        redBorderEnabled = false;
        playerInventory = FindObjectOfType<PlayerInventory>();
        hotbar = FindObjectOfType<Hotbar>();
        plantedTiles = new Dictionary<Vector3Int, bool>();
        wateredTiles = new List<Vector3Int>();
        activeHotbarSlot = 0;
        SpawnObjects();
    }

    private void FixedUpdate(){
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 worldPoint = ray.GetPoint(-ray.origin.y / ray.direction.y);
        position = grid.WorldToCell(worldPoint);

        if (position != redBorderEnabledAt){//if current mouse position is not where red border is at
            if (!redBorderEnabled){
                redBorder = Instantiate(redBorder, grid.CellToWorld(position), Quaternion.Euler(90, 0, 0));
                redBorderEnabledAt = grid.WorldToCell(worldPoint);
                redBorderEnabled = true;
            }
            if (redBorderEnabled){
                redBorder.transform.position = grid.CellToWorld(position);
                redBorderEnabledAt = grid.WorldToCell(worldPoint);
            }
        }
    }
    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Alpha1)){
            EventManager.TriggerEvent("hotbarSelect", new Dictionary<string, object> { { "number", 0 } });
        }
        if (Input.GetKeyDown(KeyCode.Alpha2)){
            EventManager.TriggerEvent("hotbarSelect", new Dictionary<string, object> { { "number", 1 } });
        }
        if (Input.GetKeyDown(KeyCode.Alpha3)){
            EventManager.TriggerEvent("hotbarSelect", new Dictionary<string, object> { { "number", 2 } });
        }
        if (Input.GetKeyDown(KeyCode.Alpha4)){
            EventManager.TriggerEvent("hotbarSelect", new Dictionary<string, object> { { "number", 3 } });
        }
        if (Input.GetKeyDown(KeyCode.Alpha5)){
            EventManager.TriggerEvent("hotbarSelect", new Dictionary<string, object> { { "number", 4 } });
        }
        if (Input.GetKeyDown(KeyCode.Alpha6)){
            EventManager.TriggerEvent("hotbarSelect", new Dictionary<string, object> { { "number", 5 } });
        }
        if (Input.GetKeyDown(KeyCode.Alpha7)){
            EventManager.TriggerEvent("hotbarSelect", new Dictionary<string, object> { { "number", 6 } });
        }
        if (Input.GetKeyDown(KeyCode.Alpha8)){
            EventManager.TriggerEvent("hotbarSelect", new Dictionary<string, object> { { "number", 7 } });
        }
        if (Input.GetKeyDown(KeyCode.Alpha9)){
            EventManager.TriggerEvent("hotbarSelect", new Dictionary<string, object> { { "number", 8 } });
        }
        if (Input.GetKeyDown(KeyCode.Alpha0)){
            EventManager.TriggerEvent("hotbarSelect", new Dictionary<string, object> { { "number", 9 } });
        }

        if (Input.GetMouseButtonDown(0)){
            TileBase clickedTile = groundMap.GetTile(position);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Vector3 worldPoint = ray.GetPoint(-ray.origin.y / ray.direction.y);

            if (currentItem != null){ //if item equipped
                if (currentItem.isHoe){ //check if currentItem is a hoe
                    //if clicked tile is untilled soil
                    if (clickedTile == untilledSoilTile){
                        //set tile to tilled soil
                        groundMap.SetTile(grid.WorldToCell(worldPoint), tilledSoilTile);
                    }
                }
                //if you have a watering can, water the soil
                if (currentItem.isWateringCan){
                    if (clickedTile == tilledSoilTile){
                        groundMap.SetTile(grid.WorldToCell(worldPoint), wateredSoilTile);
                        wateredTiles.Add(grid.WorldToCell(worldPoint));
                    }
                }
                if (currentItem.isSeed){ //if seed is equipped
                    if (clickedTile == tilledSoilTile || clickedTile == wateredSoilTile){ //if tile is tilled
                        //get crop object from current item and instantiate
                        if (!plantedTiles.ContainsKey(grid.WorldToCell(worldPoint))){
                            plantedTiles.Add(grid.WorldToCell(worldPoint), true);
                            Instantiate(currentItem.cropData.cropObject, grid.GetCellCenterWorld(position), Quaternion.Euler(90, 0, 0));
                            Debug.Log(grid.GetCellCenterWorld(position));
                        }
                    }
                }
            }
        }
    }
    // Iterated through the entire grid. Assigns values to each square. 
    // If value matches certain range, spawn object
    void SpawnObjects(){
        foreach (Vector3Int pos in groundMap.cellBounds.allPositionsWithin){
                Vector3Int localPlace = new Vector3Int(pos.x, pos.y, pos.z);

                if (groundMap.HasTile(localPlace)){
                SpawnId = UnityEngine.Random.Range(0, 10000);
                //150 rock chance each
                if (SpawnId >= 0 && SpawnId < 150){
                rock1 = harvestableObjects[0].spawnedObject;
                Instantiate(rock1, grid.GetCellCenterWorld(pos), Quaternion.Euler(45, 0, 0));
                }
                if (SpawnId >= 150 && SpawnId < 300){
                    //spawn rock2
                    Instantiate(harvestableObjects[1].spawnedObject, grid.GetCellCenterWorld(pos), Quaternion.Euler(45, 0, 0));
                }
                //150 chance for tree
                if (SpawnId >= 300 && SpawnId < 450){ //spawn tree
                    Instantiate(harvestableObjects[2].spawnedObject, grid.GetCellCenterWorld(pos), Quaternion.Euler(45, 0, 0));
                }
                //75 for boulder
                if (SpawnId >= 450 && SpawnId < 525) {   //spawn boulder
                    Instantiate(harvestableObjects[3].spawnedObject, grid.GetCellCenterWorld(pos), Quaternion.Euler(45, 0, 0));
                }
                // Manual spawning of a house. Without Unity's built in helper functions

                //if (SpawnId >= 425 && SpawnId < 425)
                //{   //spawn house
                //    create a new gameobject
                //    GameObject house = new GameObject("House");
                //    create a new SpriteRenderer
                //    SpriteRenderer houseSpriteRenderer = house.AddComponent<SpriteRenderer>();
                //    houseSpriteRenderer.sprite = houseSprite;
                //    houseSpriteRenderer.spriteSortPoint = SpriteSortPoint.Pivot;
                //    set position to 0 plus the offset for shadow
                //    rock.transform.localPosition
                //    house.transform.localPosition = place - (new Vector3(0.5f, 0, 0));
                //    house.transform.localRotation = Quaternion.Euler(45, 0, 0);
                //    set sorting layer
                //    houseSpriteRenderer.sortingOrder = 1;
                //    BoxCollider houseBoxCollider = house.AddComponent<BoxCollider>();
                //    houseBoxCollider.size = new Vector3(0.8f, 0.7f, 0.2f);
                //    houseBoxCollider.center = new Vector3(0.0f, 0.4f, 0.1f);

                //}
                }
            }
        }
    }