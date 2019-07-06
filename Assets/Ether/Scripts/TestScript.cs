using DG.Tweening;
using UniRx;
using UnityEngine;


[RequireComponent(typeof(SwipeGesture))]
public class TestScript : MonoBehaviour
{

    [SerializeField] private Transform cube;
    private SwipeGesture swipeGesture;
    private Tween moveAnimation;
    Quaternion quaternion;

    void Awake()
    {
        DOTween.Init();
        DOTween.defaultAutoPlay = AutoPlay.None; // Tween生成時に自動再生させない
    }

    void OnEnable()
    {
        quaternion = new Quaternion(0.0f, 0.0f, 90.0f, 1.0f);
        Vector3 vec = quaternion.eulerAngles;
        this.swipeGesture = this.GetComponent<SwipeGesture>();

        // next
        this.swipeGesture
            .OnSwipeLeft
            .Subscribe(_ =>
            {
                Debug.Log("right");
                this.moveAnimation = this.cube.DORotate
                (
                    Vector3.up * 90f, 1.0f
                )
                .Play();
            });

        this.swipeGesture
    .OnSwipeUp
    .Subscribe(_ =>
    {
        Debug.Log("up");
    this.moveAnimation = this.cube.DORotate
    (
        vec, 1.0f
        )
        .Play();
    });
    }
}