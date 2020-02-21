using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlobStateBlinking : BlobState
{
    private float elapsedTime;
    private float flashTimer;
    private float flashTime = 0.5f;
    private static float timeTillExit = 2f;

    private MeshRenderer renderer;

    public BlobStateBlinking(Blob theBlob):base(theBlob)
    {

    }

    public override void Enter()
    {
        base.Enter();
        elapsedTime = 0;
        flashTimer = 0;
        renderer = blob.GetComponent<MeshRenderer>();
    }

    public override void Run()
    {
        elapsedTime += Time.deltaTime;
        flashTimer += Time.deltaTime;

        if(flashTimer > flashTime)
        {
            flashTimer -= flashTime;
            renderer.enabled = !renderer.enabled;
        }

        if(elapsedTime >= timeTillExit)
        {
            blob.ChangeState(new BlobStateMoving(blob));
        }
    }
}
