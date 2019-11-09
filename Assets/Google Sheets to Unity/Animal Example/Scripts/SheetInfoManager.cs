using UnityEngine;
using GoogleSheetsToUnity;

/// <summary>
/// example script to show realtime updates of multiple items
/// </summary>
public class SheetInfoManager : MonoBehaviour
{
    public enum SheetStatus
    {
        PUBLIC,
        PRIVATE
    }
    public SheetStatus sheetStatus;

    public string associatedSheet = "195Si2_NVi9xt67DwPIXRzGe8Cfn5rOnJgrK9vRWWgFE";
    public string[] associatedWorksheets;// = "3 Drops";

    public DropsContainer container;
    public CSVReader reader;
    private int iter;

    // 起動時に一括でpullできないので，粒数を変更した時にそれぞれの粒数の情報をアップデートするようにする.
    void Awake()
    {
        //for (int i = 0; i < associatedWorksheets[i].Length; i++)
        //{
            SpreadsheetManager.Read(new GSTU_Search(associatedSheet, associatedWorksheets[7]), this.UpdateAllDrops);
            //iter = i;
        //}

        //reader.ManualStart();
    }

    public void UpdateStats()
    {
        if (sheetStatus == SheetStatus.PRIVATE)
        {

        }
    }

    void UpdateAllDrops(GstuSpreadSheet ss)
    {
        container.allNumDrops[7].UpdateStats(ss);
    }

}
