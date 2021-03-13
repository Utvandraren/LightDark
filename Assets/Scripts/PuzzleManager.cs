using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class PuzzleManager : MonoBehaviour
{
    [SerializeField] GameObject cameraObj;
    [SerializeField] Collider interactCollider;
    [SerializeField] UnityEvent onPuzzleSolve;

    int nextNmbr = 1;
    int puzzlePieces = 0;
    Animator animator;
    GameObject playerObj;
    bool isSolved = false;

    // Start is called before the first frame update
    void Start()
    {
        TryGetComponent<Animator>(out animator);
        puzzlePieces = GetComponentsInChildren<PuzzlePiece>().Length;
        playerObj = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (Input.GetButton("Holster"))
        {
            if (playerObj.activeSelf == false)
            {
                ExitPuzzle();
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

    public void EnterPuzzle()
    {
        if (isSolved)
            return;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
        interactCollider.enabled = false;
        cameraObj.SetActive(true);
        playerObj.SetActive(false);
    }

    public void ExitPuzzle()
    {
        interactCollider.enabled = true;
        playerObj.SetActive(true);
        cameraObj.SetActive(false);
    }

    void SolvePuzzle()
    {
        isSolved = true;
        onPuzzleSolve.Invoke();
        ExitPuzzle();
        //set effects here
        //Do something here when the puzzle is solved , dont know yet
    }


}