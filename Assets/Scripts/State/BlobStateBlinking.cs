using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlobStateBlinking : BlobState
{

    private IEnumerator coroutine;
    
    public BlobStateBlinking(Blob theBlob) : base(theBlob) // Derived class constructor calls base class constructor.
    {

    }

    public override void Run()
    {

        
    }

    public override void Enter()
    {
        blob.StartCoroutine(blob.blink(0.5f));
        blob.ChangeState(new BlobStateMoving(blob));
    }

    
    public override void Leave()
    {
        GameController.Scoring.AddScore(1);
    }
    
}
