using System.IO;
using UnityEngine;

/// <summary>
/// CSVファイルへの書き込みを行うスクリプト.
/// </summary>
public class CSVWriter : MonoBehaviour
{
    public void Save(string data, string fileName)
    {
        StreamWriter streamWriter;
        FileInfo fileInfo;

        fileInfo = new FileInfo(Application.dataPath + "/Resources/" + fileName + ".csv");
        streamWriter = fileInfo.AppendText();
        streamWriter.WriteLine(data);

        streamWriter.Flush();
        streamWriter.Close();
    }
}
