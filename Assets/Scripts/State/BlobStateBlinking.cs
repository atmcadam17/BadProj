using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlobStateBlinking : BlobState
{

    private GameController controller;
    private float elapsedTime;
    private float endTime;
    private Renderer renderer;
    private Color initialColor;
    private Color white = new Color(1,1,1);
    private float blinktime;
    private float blinkEndTime = 0.5f;
    public BlobStateBlinking(Blob theBlob) : base(theBlob) // Derived class constructor calls base class constructor.
    {

    }

    public override void Run()
    {


        elapsedTime += Time.deltaTime;
        blinktime += Time.deltaTime;
        
        if (elapsedTime > endTime)
        {
            blob.ChangeState(new BlobStateMoving(blob));
        }
        else if (blinktime > blinkEndTime)
        {
            blinktime = blinkEndTime;
            if(renderer.material.color == white)
            {
                renderer.material.color = initialColor;
            }
            else
            {
                renderer.material.color = white;
            }
                
        }
    }


    public override void Enter() 
    {
       
        base.Enter();
        endTime = 2.0f;
        elapsedTime = 0.0f;
        controller = GameObject.Find("Ground").GetComponent<GameController>();
        renderer = blob.GetComponent<Renderer>(); // Cache the renderer.
        initialColor = renderer.material.GetColor("_Color");

    }
    public override void Leave()
    {
        base.Leave();
        controller.score += 1;
        renderer.material.color = white;
    }
}
