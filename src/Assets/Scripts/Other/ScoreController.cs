using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 控制分数的显示
/// </summary>
public class ScoreController : ReuseableObject {

    public Sprite[] sprites;

    private SpriteRenderer spriteRenderer;

    public bool Special {
        set {
            if (value)
            {
                transform.localScale = new Vector3(2, 2);
            }
            else {
                transform.localScale = new Vector3(1, 1);
            }
        }
    }

    public void InitScore(int score) {
        if (score == 5) {
            spriteRenderer.sprite = sprites[0];
        }
        if (score == 10)
        {
            spriteRenderer.sprite = sprites[1];
        }
        if (score == 20)
        {
            spriteRenderer.sprite = sprites[2];
        }
        if (score == 100)
        {
            spriteRenderer.sprite = sprites[3];
        }
        if (score == 200)
        {
            spriteRenderer.sprite = sprites[4];
        }

    }

    public override void BeforeGetObject()
    {
        if (spriteRenderer == null) {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
        StartCoroutine(Hide());
    }

    public override void BeforeHideObject()
    {
        Special = false;
    }

    IEnumerator Hide() {
        float timer = 1f;
        while (timer >0) {
            timer -= Time.deltaTime;
            yield return 1f;
            transform.Translate(Vector2.up*Time.deltaTime);
        }
        PoolManager.Instance.HideObject(this.gameObject);
    }
}
