using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlobStateBlinking : BlobStatePulsing
{
    private bool isBlinking;
    private MeshRenderer blobMR;

    private float elapsedTime;
    private float endTime;

    private float blink;
    private float maxBlink;



    public BlobStateBlinking(Blob theBlob) : base(theBlob)
    {
        
    }

    public override void Enter()
    {
        base.Enter();
     
        blobMR = blob.GetComponent<MeshRenderer>();
        endTime = 10f;
        blink = 0f;
        maxBlink = .5f;

    }

    public override void Run()
    {
        elapsedTime += Time.deltaTime;

        if (elapsedTime > endTime)
        {
            blob.ChangeState(new BlobStateMoving(blob));
        }

        blink += Time.deltaTime;
        
        if(blink>maxBlink)
        {
            blink = 0f;
            Blink();


        }
        


    }

    public void Blink()
    {
        
        
            if (isBlinking) blobMR.enabled = false;
            else
            {
                
                blobMR.enabled = true;
            }

            isBlinking = !isBlinking;

    }
    
    
    public override void Leave()
    {
        base.Leave();
        GameController controller = blob.GetComponentInParent<GameController>();
        controller.Score += 1;
    }
}

