using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Blobs are objects whose behaviour is controlled by a state machine. They are destroyed when the player clicks on them.
 * This class manages the blob state machine and provides a link to the master game controller,
   to allow the blob to interact with the game world.*/
public class Blob : MonoBehaviour
{
    //Keeps track of how many times a blob can bounce off a wall before being destroyed
    public int SafeBouncesRemaining = 3;

    private BlobState currentState; // Current blob state (unique to each blob)
    public BlobStateMoving MovingState //Allows a certain value in BlobStateMoving to be altered externally
    {
        get
        {
            BlobStateMoving movingState = (BlobStateMoving)currentState;
            return movingState;
        }
    }
    private GameController controller;  // Cached connection to game controller component
    public GameController Controller
    {
        get
        {
            return controller;
        }
    }

    private bool shrinking = false;

    void Start()
    {
        ChangeState(new BlobStateMoving(this)); // Set initial state.
        controller = GetComponentInParent<GameController>();

    }

    void Update()
    {
        currentState.Run(); // Run state update.
    }

    // Change state.
    public void ChangeState(BlobState newState)
    {
        if (currentState != null) currentState.Leave();
        currentState = newState;
        currentState.Enter();
    }

    // Change blobs to shrinking state when clicked.
    void OnMouseDown()
    {
        if (!shrinking)
        {
            ChangeState(new BlobStateShrinking(this));
            shrinking = true;
        }
    }

    // Destroy blob gameObject and remove it from master blob list.
    public void Kill()
    {
        controller.RemoveFromList(this);
        Destroy(gameObject);
        controller.Score += 10;
    }
}

//"controller.Score = 10" makes it sound like 10 is being set as the score, not being added to it.
//Therefore, it has been changed to "controller.Score += 10", and the rest of the scorekeeping functionality has been appropriately changed to match it