using System;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class DroppedItemPhysics : MonoBehaviour
{
    private float y;
    private float rotationSpeed;
    public Vector3 ShadowOffset;
    public Vector3 ShadowRotation;
    public Material ShadowMaterial;
    public float degreesPerSecond = 15.0f;
    public float amplitude = 0.4f;
    public float frequency = 0.4f;
    public float flyingSpeed;
    private Vector3 objectPos;
    SpriteRenderer spriteRenderer;
    Rigidbody rb;
    GameObject bottomShadow;
    public Sprite shadowSprite;
    public Item item;
    void Start()
    {
        y = 0.0f;
        rotationSpeed = 75.0f;
        //tempPos.z = 0.1f;
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody>();
        //create a new gameobject to be used as bottom shadow
        bottomShadow = new GameObject("Bottom Shadow");
        //create a new SpriteRenderer for Bottom Shadow gameobject
        SpriteRenderer bottomSpriteRenderer = bottomShadow.AddComponent<SpriteRenderer>();

        //set shadow position to 0 plus the offset for shadow
        bottomShadow.transform.localPosition = gameObject.transform.position;
        //set the shadow gameobject's sprite to the shadowsprite
        bottomSpriteRenderer.sprite = shadowSprite;
        //set sorting layer to objects
        bottomSpriteRenderer.sortingOrder = spriteRenderer.sortingOrder;

        var randomDirectionX = Random.Range(-1f, 1);
        var randomDirectionY = Random.Range(2f, 4f);
        var randomDirectionZ = Random.Range(-1f, 1);
        // var randomX = Random.ran
        // new Vector3 random 
        rb.AddForce(randomDirectionX, randomDirectionY, randomDirectionZ, ForceMode.Impulse);

    }

    void FixedUpdate()
    {
        Spin();

        //test transparancy for shadow
        //spriteRenderer.color = new Color(1f, 1f, 1f, 1f); is a normal sprite
        //spriteRenderer.color = new Color(1f, 1f, 1f, .5f); is about 50 % transparent
        //bottomShadow = new Color(1f, 1f, 0f, -transform.position.y); //is about 100 % transparent(Cant be seen at all, but still active)
        //Color color = bottomSpriteRenderer.color;
        //color.a = transform.position.y;
        //bottomSpriteRenderer.color = color;
    }
    private void OnDestroy()
    {
        Destroy(bottomShadow);
    }

    void Spin()
    {
        y += Time.deltaTime * rotationSpeed; //rotate y
        transform.rotation = Quaternion.Euler(0, y, 0); //rotate object with y
        bottomShadow.transform.rotation = Quaternion.Euler(0, y, 0); //rotate shadow with y
        objectPos.x = transform.position.x;
        objectPos.z = transform.position.z;
        bottomShadow.transform.position = new Vector3(objectPos.x, 0, objectPos.z); //move shadow with objects X and Z //make sure shadow y stays 0

        //if object hits floor, stop movement
        if (transform.position.y <= -.01f)
        {
            rb.useGravity = false;
            rb.velocity = Vector3.zero;

        }
    }

}