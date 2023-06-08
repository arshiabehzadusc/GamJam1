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

    
    /*
     * Checks if it is possible to add piece to the board based on 2 factors:
     * 1) There is an empty spot (no piece has been played there)
     * 2) The previous board has a piece already played there (simulates stacking)
     * Note: Board 1 does not have the restriction of 2) because it is the bottom layer
     * 
     *
     * If it is possible to add to the board it adds to the board, it adds it the 2d matrix
     * representing the correct board
     *
     * @returns: true if can add to board, false if cannot
     */
    public bool addToBoard(bool xTurn)
    {
        //uses name of object to know what column and row the spot is in
        //uses the board variable to know what board the spot is in
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
        if (Input.GetMouseButtonDown(0)) // Checks for mouse click
        {
            canAdd = true;
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);
            //checks if spot was clicked
            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                xTurn = gameScript.getTurn();
                //handeling for X's turn
                if (xTurn)
                {
                        //adds X to board if possible
                        canAdd = addToBoard(gameScript.getTurn());
                        if (canAdd)
                        {
                            //instantiates the X prefab to spawn on spot
                            GameObject spawnedPrefab = Instantiate(xMark, transform.position, Quaternion.identity);
                            //checks if winner
                            if (gameScript.determineIfWinner())
                            {
                                Debug.Log("X Wins!");
                                notificationText.text = "X Wins!";
                                Application.Quit();
                            }
                            //changes turn to O
                            gameScript.toggleTurn();
                        }
                        else
                        {
                            //Debug.Log("Cant add here");
                        }

                }
                //handeling for Os turn
                else
                {
                    //adds O to board if possible
                    canAdd = addToBoard(gameScript.getTurn());
                    if (canAdd)
                    {
                        //instantiates the O prefab to spawn on spot
                        GameObject spawnedPrefab = Instantiate(oMark, transform.position, Quaternion.identity);
                        //checks if winner
                        if (gameScript.determineIfWinner())
                        {
                            Debug.Log("O Wins!");
                            notificationText.text = "O Wins!";
                            Application.Quit();
                        }
                        //changes turn to X 
                        gameScript.toggleTurn();
                    }
                    else
                    {
                        //Debug.Log("Cant add here");
                    }

                }
            }
            
        }
    }
}
