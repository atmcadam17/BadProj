using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Blobs are objects whose behaviour is controlled by a state machine. They are destroyed when the player clicks on them.
 * This class manages the blob state machine and provides a link to the master game controller,
   to allow the blob to interact with the game world.*/
public class Blob : MonoBehaviour
{
    private BlobState currentState; // Current blob state (unique to each blob)
    private GameController controller;  // Cached connection to game controller component

    // Store whether blob has been clicked or not in private backing variable.
    private bool _clicked;

    public bool Clicked
    {
        get // Only a getter is provided, so that only this class can set that the blob was clicked.
        {
            return _clicked;
        }
    }

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

    // When a blob is clicked, the state machine needs to know.
    void OnMouseDown()
    {
        _clicked = true;
    }

    // Destroy blob gameObject and remove it from master blob list.
    public void Kill()
    {
        controller.RemoveFromList(this);
        Destroy(gameObject);
        controller.Score += 10;
    }
}
