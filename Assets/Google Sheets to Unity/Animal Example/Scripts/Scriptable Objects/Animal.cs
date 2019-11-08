using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using GoogleSheetsToUnity;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class Animal : ScriptableObject
{
    public string associatedSheet = "195Si2_NVi9xt67DwPIXRzGe8Cfn5rOnJgrK9vRWWgFE";
    public string associatedWorksheet = "3 Drops";

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
        for (int i = 0; i < pattern.Length; i++)
        {
            pattern[i] = ss[i.ToString(), name].value;
        }
        items.Add(ss["Items", name].value);
    }
}


//Custom editior to provide additional features
#if UNITY_EDITOR
[CustomEditor(typeof(Animal))]
public class AnimalEditor : Editor
{
    Animal animal;

    void OnEnable()
    {
        animal = (Animal)target;
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
            UpdateAnimalInformationOnSheet();

        GUILayout.Label("Add New Data");
        if (GUILayout.Button("Add Data to Archive"))
            WriteToSheet();
    }

    public void UpdateStats(UnityAction<GstuSpreadSheet> callback, bool mergedCells = false)
    {
        SpreadsheetManager.Read(new GSTU_Search(animal.associatedSheet, animal.associatedWorksheet), callback, mergedCells);
    }

    public void UpdateMethodOne(GstuSpreadSheet ss)
    {
        animal.UpdateStats(ss.rows[animal.name]);

        EditorUtility.SetDirty(target);
    }

    void UpdateMethodTwo(GstuSpreadSheet ss)
    {
        animal.UpdateStats(ss);

        EditorUtility.SetDirty(target);
    }

    /// <summary>
    /// Adds the new animal to the spreadsheet online at the location defined as start cell, if no start cell defined will write from A1
    /// </summary>
    public void WriteToSheet()
    {
        List<string> list = new List<string>();

        list.Add(animal.name);
        list.Add(animal.health.ToString());
        list.Add(animal.attack.ToString());
        list.Add(animal.defence.ToString());

        SpreadsheetManager.Write(new GSTU_Search(animal.associatedSheet, "archive", "G10"), new ValueRange(list), null);
    }

    /// <summary>
    /// Finds and updates the rows data based on an entry row data, in this example i am using the name as the unique id to find the starting cell for the row
    /// If the spreadsheet is cashed then no need to do the read and can just pass into the update
    /// </summary>
    void UpdateAnimalInformationOnSheet()
    {
        SpreadsheetManager.Read(new GSTU_Search(animal.associatedSheet, animal.associatedWorksheet), UpdateAnimalInformation);
    }
    private void UpdateAnimalInformation(GstuSpreadSheet ss)
    {
        BatchRequestBody updateRequest = new BatchRequestBody();
        updateRequest.Add(ss["0", animal.name].AddCellToBatchUpdate(animal.associatedSheet, animal.associatedWorksheet, animal.pattern[0]));
        updateRequest.Add(ss["1", animal.name].AddCellToBatchUpdate(animal.associatedSheet, animal.associatedWorksheet, animal.pattern[1]));
        updateRequest.Add(ss["2", animal.name].AddCellToBatchUpdate(animal.associatedSheet, animal.associatedWorksheet, animal.pattern[2]));
        updateRequest.Send(animal.associatedSheet, animal.associatedWorksheet, null);
    }
}
#endif
