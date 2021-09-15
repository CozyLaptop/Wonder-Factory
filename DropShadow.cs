using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class DropShadow : MonoBehaviour
{
    public Vector3 ShadowOffset;
    public Vector3 ShadowRotation;
    public Material ShadowMaterial;
    SpriteRenderer spriteRenderer;
    GameObject shadowGameobject;


    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        //create a new gameobject to be used as drop shadow
        shadowGameobject = new GameObject("Shadow");
        shadowGameobject.transform.parent = gameObject.transform;
        //create a new SpriteRenderer for Shadow gameobject
        SpriteRenderer shadowSpriteRenderer = shadowGameobject.AddComponent<SpriteRenderer>();

        //set the shadow gameobject's sprite to the original sprite
        shadowSpriteRenderer.sprite = spriteRenderer.sprite;
        //set the shadow gameobject's material to the shadow material we created
        shadowSpriteRenderer.material = ShadowMaterial;

        //update the sorting layer of the shadow to always lie behind the sprite
        //shadowSpriteRenderer.sortingLayerName = spriteRenderer.sortingLayerName;
        shadowSpriteRenderer.sortingOrder = spriteRenderer.sortingOrder;
        ShadowOffset = new Vector3(0 , 0.1f, 0);
    }

    void LateUpdate()
    {
        //update the position and rotation of the sprite's shadow with moving sprite
        //shadowGameobject.transform.localPosition = transform.localPosition + (Vector3)ShadowOffset;
        //shadowGameobject.transform.localRotation = transform.rotation;
        //shadowGameobject.transform.localPosition = transform.position + (Vector3)ShadowOffset;
        //shadowGameobject.transform.Rotate(0, .5f, 0);
    }
}