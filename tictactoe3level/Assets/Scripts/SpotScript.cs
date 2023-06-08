using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using TMPro;
using UnityEngine;

public class SpotScript : MonoBehaviour
{
    private bool xTurn;
    private bool empty;
    public GameObject xMark;
    public GameObject oMark;
    public string name;
    public GameObject gameManager;
    private GameManager gameScript;
    private bool canAdd;
    public TextMeshProUGUI notificationText;

    private string board;
    // Start is called before the first frame update
    void Start()
    {
        board = transform.root.gameObject.name;
        gameScript = gameManager.GetComponent<GameManager>();
        name = gameObject.name;
        empty = true;
        canAdd = false;
    }


    //private GameObject currentTurnObject = xMark;
    // Update is called once per frame
    public bool addToBoard(bool xTurn)
    {
        int row = (int)char.GetNumericValue(name[0]);
        int col = (int)char.GetNumericValue(name[1]);
        switch (board)
        {
            case "Board1":
                if (gameScript.g1.board[row, col] == 0)
                {
                    
                    gameScript.g1.fillSpot(xTurn, row, col);
                    //gameScript.g1.printBoard();
                    return true;
                }
                break;
            case "Board2":
                if (gameScript.g1.board[row, col] > 0 && gameScript.g2.board[row, col] == 0)
                {
                    gameScript.g2.fillSpot(xTurn, row,col);
                    //gameScript.g2.printBoard();
                    return true;
                }
                break;
            case "Board3":
                if (gameScript.g2.board[row, col] > 0 && gameScript.g3.board[row, col] == 0)
                {
                    gameScript.g3.fillSpot(xTurn, row, col);
                    //gameScript.g3.printBoard();
                    return true;
                }
                break;
        }

        return false;
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Left mouse button
        {
            canAdd = true;
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);
            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                // Mouse click detected on the colliders
                xTurn = gameScript.getTurn();
                if (xTurn)
                {
                        canAdd = addToBoard(gameScript.getTurn());
                        if (canAdd)
                        {
                            GameObject spawnedPrefab = Instantiate(xMark, transform.position, Quaternion.identity);
                            if (gameScript.determineIfWinner())
                            {
                                Debug.Log("X Wins!");
                                notificationText.text = "X Wins!";
                                //Application.Quit();
                            }
                            gameScript.toggleTurn();
                        }
                        else
                        {
                            Debug.Log("Cant add here");
                        }


                }
                else
                {
                    canAdd = addToBoard(gameScript.getTurn());
                    if (canAdd)
                    {
                        GameObject spawnedPrefab = Instantiate(oMark, transform.position, Quaternion.identity);
                        if (gameScript.determineIfWinner())
                        {
                            Debug.Log("O Wins!");
                            notificationText.text = "O Wins!";
                            //Application.Quit();
                        }
                        gameScript.toggleTurn();
                    }
                    else
                    {
                        Debug.Log("Cant add here");
                    }

                }

                empty = false;
            }
            
        }
    }
}
