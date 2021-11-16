using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goals : MonoBehaviour
{
    public Transform[] children;
    private int index = 0;
    // Start is called before the first frame update
    void Start()
    {
       

    }

    public void resetIndex()
    {
        index = 0;
    }


    public Transform GetNextChildren()
    {
        Transform nc = null;
        if (index < children.Length)
        {
            nc = children[index];
            index++;
        }
        return nc;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
