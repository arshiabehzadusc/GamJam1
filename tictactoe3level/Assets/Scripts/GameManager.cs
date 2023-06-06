using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public bool xTurn;
    void Start()
    {
        xTurn = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool getTurn()
    {
        return xTurn;
    }
    public void toggleTurn()
    {
        if (xTurn)
        {
            xTurn = false;
        }
        else
        {
            xTurn = true;
        }
    }
}
