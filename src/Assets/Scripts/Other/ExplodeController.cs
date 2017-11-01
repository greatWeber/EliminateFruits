using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeController : ReuseableObject
{
    public override void BeforeGetObject()
    {
        StartCoroutine(Hide());
    }

    IEnumerator Hide() {
        yield return new WaitForSeconds(0.5f);
        PoolManager.Instance.HideObject(this.gameObject);
    }

    public override void BeforeHideObject()
    {
        
    }
}
