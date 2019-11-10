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
    //public string associatedWorksheets = "Ether";

    public DropsContainer container;


    void Awake()
    {
        this.UpdateStats();
    }

    public void UpdateStats()
    {
        if (sheetStatus == SheetStatus.PRIVATE)
        {
            SpreadsheetManager.Read(new GSTU_Search(associatedSheet, "Ether"), this.UpdateAllDrops);
        }
    }

    void UpdateAllDrops(GstuSpreadSheet ss)
    {
        foreach (Drops drops in container.allNumDrops)
        {
            drops.UpdateStats(ss);
        }
    }

}
