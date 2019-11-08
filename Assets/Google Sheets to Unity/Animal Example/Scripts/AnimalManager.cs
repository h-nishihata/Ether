using UnityEngine;
using System.Collections;
using GoogleSheetsToUnity;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections.Generic;

/// <summary>
/// example script to show realtime updates of multiple items
/// </summary>
public class AnimalManager : MonoBehaviour
{
    public enum SheetStatus
    {
        PUBLIC,
        PRIVATE
    }
    public SheetStatus sheetStatus;


    public string associatedSheet = "195Si2_NVi9xt67DwPIXRzGe8Cfn5rOnJgrK9vRWWgFE";
    public string associatedWorksheet = "3 Drops";

    public AnimalContainer container;
    

    public bool updateOnPlay;

    void Awake()
    {
        if(updateOnPlay)
        {
           UpdateStats();
        }
    }

    public void UpdateStats()
    {
        if (sheetStatus == SheetStatus.PRIVATE)
        {
            SpreadsheetManager.Read(new GSTU_Search(associatedSheet, associatedWorksheet), UpdateAllAnimals);
        }
        else if(sheetStatus == SheetStatus.PUBLIC)
        {
            SpreadsheetManager.ReadPublicSpreadsheet(new GSTU_Search(associatedSheet, associatedWorksheet), UpdateAllAnimals);
        }
    }

    void UpdateAllAnimals(GstuSpreadSheet ss)
    {
        foreach (Animal animal in container.allAnimals)
        {
            animal.UpdateStats(ss);
        }
    }

}
