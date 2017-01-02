using UnityEngine;
using System.Collections;

public class SnakeScript : MonoBehaviour
{
    int[,] grid = new int[10,10];

    int snakeScore = 3;
    int snakeX = 0;
    int snakeY = 4;

    Transform snakeTransform;
    float lastMove;
    float timeInBetweenMoves = 0.5f;
    Vector3 direction;

    bool hasLost;

    private void Start()
    {
        //   snakeTransform = GetComponent<Transform>();
        snakeTransform = transform;
        direction = Vector3.right;
        grid[snakeX, snakeY] = snakeScore;
    }

    private void Update()
    {
        if (hasLost)
        {
            return;
        }


        if(Time.time - lastMove > timeInBetweenMoves)
        {
            // Every move, itterate through out whole array 
            //and reduce every tile that isnt -1 or 0
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    //Debug.Log("X: " + i + " Y: " + j + "value: " + grid[i, j]);
                    if (grid[i,j] > 0)
                    {
                        grid[i, j]--;
                        if (grid[i,j] == 0)
                        {
                            // We have to destroy something
                            GameObject toDestroy = GameObject.Find(i.ToString() + j.ToString());
                            if (toDestroy != null)
                            {
                                Destroy(toDestroy);
                            }
                            
                        }
                    }
                }

            }

            lastMove = Time.time;

            GameObject obj = GameObject.CreatePrimitive(PrimitiveType.Cube);
            obj.transform.position = new Vector3(snakeX, snakeY, 0);
            obj.name = snakeX.ToString() + snakeY.ToString();

            // add up direction to our snakeX and snakeY
            if (direction.x == 1)
            {
                snakeX++;
            }

            if (direction.x == -1)
            {
                snakeX--;
            }

            if (direction.y == 1)
            {
                snakeY++;
            }

            if (direction.y == -1)
            {
                snakeY--;
            }

           

            // if it goes out of bounds
            if (snakeX > 9 || snakeX < 0 || snakeY < 0 || snakeY > 9)
            {
                hasLost = true;
                Debug.Log("out of bounds");
            }else
            {
                // We eat an apple
                if (grid[snakeX, snakeY] == -1)
                {
                    snakeScore++;
                    // Create new apple
                }
                else if (grid[snakeX, snakeY] != 0)
                {
                    hasLost = true;
                    Debug.Log("We lost");
                    return;
                }

                // move
                //  Debug.Log("move");
                snakeTransform.position += direction;
                grid[snakeX, snakeY] = snakeScore;            
            }
        }


        if (Input.GetKeyDown(KeyCode.W))
        {
            //Debug.Log("Move Up");
            direction = Vector3.up;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            direction = Vector3.left;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            direction = Vector3.down;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            direction = Vector3.right;
        }
    }
}
 