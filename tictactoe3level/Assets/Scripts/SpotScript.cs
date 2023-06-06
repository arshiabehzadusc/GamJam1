using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpotScript : MonoBehaviour
{
    private bool xTurn;
    private bool empty;
    public GameObject xMark;
    public GameObject oMark;

    public GameObject gameManager;
    private GameManager gameScript;
    // Start is called before the first frame update
    void Start()
    {
        gameScript = gameManager.GetComponent<GameManager>();
        empty = true;
    }


    //private GameObject currentTurnObject = xMark;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Left mouse button
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                // Mouse click detected on the colliders
                xTurn = gameScript.getTurn();
                if (empty)
                {
                    if (xTurn)
                    {
                        GameObject spawnedPrefab = Instantiate(xMark, transform.position, Quaternion.identity);
                        gameScript.toggleTurn();
                    }
                    else
                    {
                        GameObject spawnedPrefab = Instantiate(oMark, transform.position, Quaternion.identity);
                        gameScript.toggleTurn();
                    }
                    empty = false;
                }
            }
        }
    }
}
