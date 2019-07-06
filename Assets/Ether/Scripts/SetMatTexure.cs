//Attach this script to your GameObject (make sure it has a Renderer component)
//Click on the GameObject. Attach your own Textures in the GameObject’s Inspector.

//This script takes your GameObject’s material and changes its Normal Map, Albedo, and Metallic properties to the Textures you attach in the GameObject’s Inspector. This happens when you enter Play Mode

using UnityEngine;

public class SetMatTexure : MonoBehaviour {

    //Set these Textures in the Inspector
    public Texture m_MainTexture, m_Normal, m_Metal;
    Renderer m_Renderer;

    // Use this for initialization
    void Start () {
        //Fetch the Renderer from the GameObject
        m_Renderer = GetComponent<Renderer> ();
        m_Renderer.material.SetTexture("_MainTex", m_MainTexture);
    }

    public void SetDefaultTexture()
    {
        m_Renderer.material.SetTexture("_MainTex", m_MainTexture);
    }

    public void SetTextureA()
    {
        m_Renderer.material.SetTexture("_MainTex", m_Normal);
    }

    public void SetTextureB()
    {
        m_Renderer.material.SetTexture("_MainTex", m_Metal);
    }
}