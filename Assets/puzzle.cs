using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class puzzle : MonoBehaviour
{
    [SerializeField] int puzzlePieces = 0;
    [SerializeField] GameObject cameraObj;
    [SerializeField] Collider interactCollider;

    int nextNmbr = 1;
    Animator animator;
    GameObject playerObj;

    // Start is called before the first frame update
    void Start()
    {
        TryGetComponent<Animator>(out animator);
    }

    void Update()
    {
        if(Input.GetButton("Holster"))
        {
            if (playerObj.activeSelf == false)
            {
                JumpOutOfPuzzle();
            }
        }
    }

    //should trigger when the puzzlepieces get into the trigger area and the right piece is in place
    void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PuzzlePiece piece))
            if (piece.nmbr == nextNmbr)
            {
                piece.FitInPlace();
                ++nextNmbr;
                if (nextNmbr > puzzlePieces)
                    SolvePuzzle();
            }
    }

    public void StartPuzzle(GameObject playerObject)
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
        interactCollider.enabled = false;
        cameraObj.SetActive(true);
        playerObj = playerObject;
        playerObj.SetActive(false);
    }

    public void JumpOutOfPuzzle()
    {
        interactCollider.enabled = true;
        playerObj.SetActive(true);
        cameraObj.SetActive(false);
    }

    void SolvePuzzle()
    {
        JumpOutOfPuzzle();
        //set effects here
        //Do something here when the puzzle is solved , dont know yet
    }


}
