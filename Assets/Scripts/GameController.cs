﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/*
 * Main game controller class  - controls game logic.
 */
public class GameController : MonoBehaviour
{
    // These links must be set in the Inspector.
    public Blob blobPrefab; 
    public Text scoreText; // Link to UI element to display score.

    // Control where the blobs spawn.
    public float spawnInterval = 1.0f;
    public float spawnDistanceMax = 10.0f;
    public float blobStartY = 1.0f;

    // How often do blobs spawn?
    //private float spawnTimer;

    private int _score; //backing variable for public score

    public int Score //Score is added on destroying blobs
    {
        get
        {
            return _score;
        }

        set
        {
           _score += value;
            scoreText.text = _score.ToString();
        }

    }

    // List of all the blobs in the game.
    private List<Blob> blobList = new List<Blob>();

    void Start()
    {
        //spawn blobs using a coroutine. i thought it was hard to find something to improve, but i like to use a coroutine for timed things so i implemented one here!
        //it frees up space in update which would be more useful in a longer script
        StartCoroutine(SpawnTimer());
    }

    
    void Update()
    {
        // On pressing space bar, remove the the half of the list that is highest up in the y-axis.
        if (Input.GetKeyDown("space"))
        {
            RemoveHighestBlobs();
        }
    }


    // Remove blob from blob list.
    public void RemoveFromList(Blob blob)
    {
        Debug.Log(blobList.Remove(blob) ? "Blob removed from list" : "Blob error: not removed from list");
    }

    // Remove the blobs with the highest y values. 
    public void RemoveHighestBlobs()
    {
        // Selection sort the list of blobs by y
        for (int i = 0; i < blobList.Count; i++)
        {
            int lowest = i;

            for(int j = i+1; j < blobList.Count; j++)
            {
                float currentY = blobList[j].transform.localPosition.y;
                float lowestY = blobList[lowest].transform.localPosition.y;
                if (currentY < lowestY)
                {
                    lowest = j;
                }
            }

            // Swap
            Blob temp = blobList[i];
            blobList[i] = blobList[lowest];
            blobList[lowest] = temp;
        }

        // Remove the 50% of the list with the highest y value.
        int toKill = blobList.Count / 2;

        // Iterate backwards through the list to avoid invalidating index after removing blob.
        for (int i = blobList.Count - 1; i >= toKill; i--) 
        {
            blobList[i].Kill();
        }
        
    }

    IEnumerator SpawnTimer()
    {
        //spawn blob - taken from update
        Vector3 startPosOffset = new Vector3(Random.Range(-spawnDistanceMax, spawnDistanceMax),
                                                blobStartY,
                                                Random.Range(-spawnDistanceMax, spawnDistanceMax));
        Blob newBlob = Instantiate<Blob>(blobPrefab, transform.position + startPosOffset, Quaternion.identity);
        newBlob.transform.parent = transform; // Set parent to be this gameObject so that the blobs can find the game controller.
        blobList.Add(newBlob);

        yield return new WaitForSeconds(spawnInterval);
        StartCoroutine(SpawnTimer());
    }


}
