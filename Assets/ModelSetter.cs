using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelSetter : MonoBehaviour
{
    private MeshFilter filter;
    public Mesh[] drops;
    // Start is called before the first frame update
    public void SetModel(int modelID)
    {
        //Debug.Log(modelID);
        filter.mesh = drops[modelID];
    }

    // Update is called once per frame
    void Start()
    {
        filter = this.gameObject.GetComponent<MeshFilter>();
    }
}
