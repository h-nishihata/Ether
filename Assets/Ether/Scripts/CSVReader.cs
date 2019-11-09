using UnityEngine;

/// <summary>
/// Canvasに付いている，データ読込〜粒の設定までを行うスクリプト.
/// </summary>
public class CSVReader : MonoBehaviour
{
    private RectTransform list; // 全ページを含む，Canvasの子オブジェクト.
    private PageSwitcher pageSwitcher;
    private int numMaxPages = 200;
    public GameObject pageTemplate;
    private GameObject[] pages;

    private DropModelSetter[] modelSetters; //各ページの子オブジェクトについている，モデル設定のためのスクリプト.
    private int lastHandlePos;


    void Start()
    {
        list = transform.GetComponent<RectTransform>();
        pageSwitcher = transform.GetComponent<PageSwitcher>();

        // 上限までページを生成し，Hierarchy内のオブジェクトに名前をつける.
        pages = new GameObject[numMaxPages];
        for (int i = 0; i < numMaxPages; i++)
        {
            pages[i] = Instantiate(pageTemplate);
            var p = i < 10 ? "0" + i.ToString() : i.ToString();
            pages[i].name = "page" + p;
            pages[i].transform.SetParent(this.transform);
        }

        // 粒のモデルを準備.
        modelSetters = new DropModelSetter[pages.Length];
        for (int i = 0; i < pages.Length; i++)
        {
            modelSetters[i] = pages[i].GetComponentInChildren<DropModelSetter>();
            modelSetters[i].ManualStart();
        }

        SetPages(3, 3);
    }

    /// <summary>
    /// スライダーから粒の数を変更する.
    /// </summary>
    public void OnValueChanged(int handlePos)
    {
        if ((!MatTexSetter.genConfirmed) && (handlePos == lastHandlePos))
            return;

        lastHandlePos = handlePos;

        list.anchoredPosition = Vector3.zero; // 1ページ目に戻す.
        pageSwitcher.currentPage = 1;
        SetPages(handlePos, 3);
    }


    void SetPages(int numDrops, int activePages)
    {
        pageSwitcher.pageCount = activePages; // ページの端の位置を伝える.

        for (int i = 0; i < activePages; i++)
        {
            modelSetters[i].pageID = i;
            modelSetters[i].SetDrops(numDrops);
        }
    }
}
