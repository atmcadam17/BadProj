using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Blobs are objects whose behaviour is controlled by a state machine. They are destroyed when the player clicks on them.
 * This class manages the blob state machine and provides a link to the master game controller,
   to allow the blob to interact with the game world.*/
public class Blob : MonoBehaviour
{
    private BlobState currentState; // Current blob state (unique to each blob)
    public GameController controller;  // Cached connection to game controller component
    public bool isShrinking = false;
    public Renderer rend;
    void Start()
    {
        ChangeState(new BlobStateMoving(this)); // Set initial state.
        controller = GetComponentInParent<GameController>();
        rend = GetComponent<Renderer>();

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
        if (!isShrinking)
        {
            ChangeState(new BlobStateShrinking(this));
        }
    }

    // Destroy blob gameObject and remove it from master blob list.
    public void Kill()
    {
        controller.RemoveFromList(this);
        Destroy(gameObject);
        controller.AddScore(10);
    }
    
    public IEnumerator blink(float blinkTime)
    {
        rend.enabled = false;
        yield return new WaitForSeconds(blinkTime);
        rend.enabled = true;
        yield return new WaitForSeconds(blinkTime);

    }
}
