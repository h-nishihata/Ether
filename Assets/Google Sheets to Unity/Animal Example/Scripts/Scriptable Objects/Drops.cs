using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using GoogleSheetsToUnity;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class Drops : ScriptableObject
{
    public string associatedSheet = "195Si2_NVi9xt67DwPIXRzGe8Cfn5rOnJgrK9vRWWgFE";
    public string associatedWorksheet;

    public string[] pattern;
    public int health;
    public int attack;
    public int defence;
    public List<string> items = new List<string>();

    internal void UpdateStats(List<GSTU_Cell> list)
    {
        items.Clear();

        for (int i = 0; i < list.Count; i++)
        {
            pattern[i] = list[i].value;
            //if(list[i].columnId == "Items")
            //    items.Add(list[i].value);
        }
    }

    internal void UpdateStats(GstuSpreadSheet ss)
    {
        items.Clear();
        for (int i = 0; i < 3; i++)
        {
            pattern[i] = ss[i.ToString(), name].value;
        }
        items.Add(ss["Items", name].value);
    }
}


//Custom editior to provide additional features
#if UNITY_EDITOR
[CustomEditor(typeof(Drops))]
public class AnimalEditor : Editor
{
    Drops drops;

    void OnEnable()
    {
        drops = (Drops)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GUILayout.Label("Read Data");

        if (GUILayout.Button("Pull Data Method One"))
            UpdateStats(UpdateMethodOne);

        if (GUILayout.Button("Pull Data Method Two"))
            UpdateStats(UpdateMethodTwo);


        GUILayout.Label("Write Data");
        GUILayout.Label("Update the existing data");
        if (GUILayout.Button("Update sheet information"))
            UpdateInformationOnSheet();

        GUILayout.Label("Add New Data");
        if (GUILayout.Button("Add Data to Archive"))
            WriteToSheet();
    }

    public void UpdateStats(UnityAction<GstuSpreadSheet> callback, bool mergedCells = false)
    {
        SpreadsheetManager.Read(new GSTU_Search(drops.associatedSheet, drops.associatedWorksheet), callback, mergedCells);
    }

    public void UpdateMethodOne(GstuSpreadSheet ss)
    {
        drops.UpdateStats(ss.rows[drops.name]);

        EditorUtility.SetDirty(target);
    }

    void UpdateMethodTwo(GstuSpreadSheet ss)
    {
        drops.UpdateStats(ss);

        EditorUtility.SetDirty(target);
    }

    /// <summary>
    /// Adds the new animal to the spreadsheet online at the location defined as start cell, if no start cell defined will write from A1
    /// </summary>
    public void WriteToSheet()
    {
        List<string> list = new List<string>();

        list.Add(drops.name);
        list.Add(drops.health.ToString());
        list.Add(drops.attack.ToString());
        list.Add(drops.defence.ToString());

        SpreadsheetManager.Write(new GSTU_Search(drops.associatedSheet, "archive", "G10"), new ValueRange(list), null);
    }

    /// <summary>
    /// Finds and updates the rows data based on an entry row data, in this example i am using the name as the unique id to find the starting cell for the row
    /// If the spreadsheet is cashed then no need to do the read and can just pass into the update
    /// </summary>
    void UpdateInformationOnSheet()
    {
        SpreadsheetManager.Read(new GSTU_Search(drops.associatedSheet, drops.associatedWorksheet), this.UpdateInformation);
    }
    private void UpdateInformation(GstuSpreadSheet ss)
    {
        BatchRequestBody updateRequest = new BatchRequestBody();
        updateRequest.Add(ss["0", drops.name].AddCellToBatchUpdate(drops.associatedSheet, drops.associatedWorksheet, drops.pattern[0]));
        updateRequest.Add(ss["1", drops.name].AddCellToBatchUpdate(drops.associatedSheet, drops.associatedWorksheet, drops.pattern[1]));
        updateRequest.Add(ss["2", drops.name].AddCellToBatchUpdate(drops.associatedSheet, drops.associatedWorksheet, drops.pattern[2]));
        updateRequest.Send(drops.associatedSheet, drops.associatedWorksheet, null);
    }
}
#endif
