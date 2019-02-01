using DG.Tweening;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(SwipeGesture), typeof(HorizontalLayoutGroup))]
public class TurnPage : MonoBehaviour
{
    public float PageWidth = 1080;

    private RectTransform rectTransform;
    private SwipeGesture swipeGesture;
    private Tween moveAnimation;
    private int pageCount;
    private int currentPage = 1;

    void Awake()
    {
        DOTween.Init();
        DOTween.defaultAutoPlay = AutoPlay.None; // Tween生成時に自動再生させない
    }

    void OnEnable()
    {
        this.rectTransform = this.GetComponent<RectTransform>();
        this.swipeGesture = this.GetComponent<SwipeGesture>();
        this.pageCount = this.transform.childCount;

        // next
        this.swipeGesture
            .OnSwipeLeft
            .Where(_ => currentPage < pageCount) // 最大ページ以前である場合のみ進める
            .Where(_ => this.moveAnimation == null || !this.moveAnimation.IsPlaying()) // アニメーション実行中ではない
            .Subscribe(_ =>
            {
                this.currentPage++;
                this.moveAnimation = this.rectTransform
                    .DOAnchorPosX(rectTransform.anchoredPosition.x - this.PageWidth, 1.0f)
                    .SetEase(Ease.OutBounce)
                    .Play();
            });

        // back
        this.swipeGesture
            .OnSwipeRight
            .Where(_ => currentPage > 1) // 1ページ目以降である場合のみ戻れる
            .Where(_ => this.moveAnimation == null || !this.moveAnimation.IsPlaying()) // アニメーション実行中ではない
            .Subscribe(_ =>
            {
                this.currentPage--;
                this.moveAnimation = this.rectTransform
                    .DOAnchorPosX(rectTransform.anchoredPosition.x + this.PageWidth, 1.0f)
                    .SetEase(Ease.OutBounce)
                    .Play();
            });
    }
}