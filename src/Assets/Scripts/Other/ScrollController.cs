using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;

public class ScrollController : MonoBehaviour,IEndDragHandler {

    public ScrollRect scrollRect;
    public Button btn_next;
    public Button btn_review;

    private float target = 0f;

    private bool start = false;


    private int page = 1;
    public int Page {
        get { return page; }
        set {
            page = Mathf.Clamp(value,1,3);
            target = (page - 1) * 0.5f;
            start = true;
        }
    }

    private void Start()
    {
        btn_next.onClick.AddListener(OnNextPage);
        btn_review.onClick.AddListener(OnReviewPage);
    }

    private void Update()
    {
        //滑动的效果
        if (start) {
            if (Mathf.Abs(scrollRect.horizontalNormalizedPosition - target) > 0.001f)
            {
                scrollRect.horizontalNormalizedPosition = Mathf.Lerp(scrollRect.horizontalNormalizedPosition, target, Time.deltaTime * 5f);
            }
            else {
                start = false;
            }
        }
    }

    private void OnDestroy()
    {
        btn_next.onClick.RemoveListener(OnNextPage);
        btn_review.onClick.RemoveListener(OnReviewPage);
    }

    public void OnNextPage() {
        Page += 1;
    }

    public void OnReviewPage()
    {
        Page -= 1;
    }

    //拖拽的效果
    public void OnEndDrag(PointerEventData eventData)
    {
        float pos = scrollRect.horizontalNormalizedPosition;
        if (pos <= 0.25f) {
            Page = 1;
        } else if (pos > 0.25f && pos < 0.75f) {
            Page = 2;
        } else if (pos > 0.75f) {
            Page = 3;
        }
    }
}
