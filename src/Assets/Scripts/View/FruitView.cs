using strange.extensions.mediation.impl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitView : ReuseableObject {

    public Sprite[] fruitSprites;
    public Sprite[] powerSprites;
    private SpriteRenderer[] spriteRenderers;
    public SpriteRenderer[] SpriteRenderers {
        get {
            if (spriteRenderers == null) {
                spriteRenderers = new SpriteRenderer[3];
                spriteRenderers[0] = GetComponent<SpriteRenderer>();
                spriteRenderers[1] = transform.GetChild(0).GetComponent<SpriteRenderer>();
                spriteRenderers[2] = transform.GetChild(1).GetComponent<SpriteRenderer>();
            }
            return spriteRenderers;
        }
    }

    public Vector2 MapPosition;

    public int fruitID = -1;

    private Transform selectedTransform;

    private int power = 0;
    public int Power {
        get { return power; }
        set {
            if (value == 0) {
                return;
            }

            spriteRenderers[2].gameObject.SetActive(value>=1 && value<=3);
            spriteRenderers[2].sprite = powerSprites[value-1];
            power = value;
        }
    }

    private bool selected = false;
    public bool Selected {
        get { return selected; }
        set {
            if (selectedTransform == null) {
                selectedTransform = transform.GetChild(0);
                
            }
            selected = value;
            selectedTransform.gameObject.SetActive(value);
        }
    }

    public void InitFruit(int fruitId, Vector2 pos) {
        this.MapPosition = pos;
        this.fruitID = fruitId;
        SpriteRenderers[0].sprite = fruitSprites[fruitId];
        SpriteRenderers[1].sprite = fruitSprites[fruitId];
        transform.name = "fruit_" + fruitId;
    }

    public override void BeforeGetObject()
    {
        Selected = false;
    }

    public override void BeforeHideObject()
    {
        power = 0;
        spriteRenderers[2].gameObject.SetActive(false);
    }
}
