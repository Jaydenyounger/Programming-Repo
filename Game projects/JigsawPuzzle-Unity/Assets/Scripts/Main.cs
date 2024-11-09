/*******************************************************************
* COPYRIGHT       : 2024
* PROJECT         : GDC: Jigsaw puzzle
* FILE NAME       : Main.cs
* DESCRIPTION     : Conntrols our gameplay
*                    
* REVISION HISTORY:
* Date 			Author    		        Comments
* ---------------------------------------------------------------------------
* 2024/10/28	Jayden Younger   		Added and setup the script
* 2024/10/31  Jayden Younger      Finnished the timer UI
* 2024/10/31  Jayden Younger      Finnish the Player Moves UI
* 2024/11/03  Jayden Younger      Spawn Game Pieces
* 2024/11/03  Jayden Younger      User interactions
* 2024/11/03  Jayden Younger      add custom board size and gaps   
/******************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Main : MonoBehaviour
{
    public Text UI_Timer;
    public Text UI_PlayerMoves;
    public Text ErrorMessage;
    public GameObject EndScreen;

    public int Minutes = 0;
    public int Seconds = 0;
    public bool ShouldTime = true;
    public int PlayerMoves = 0;
    public int size = 4;
    public float gapThickness = 0.01f;

    [Header("Game Reset Settings")]
    public float resetDelayTime = 2f;

    [SerializeField] private Transform gameTransform;
    [SerializeField] private Transform piecePrefab;

    private List<Transform> pieces;
    private int emptyLocation;
    private bool shuffling = false;
    private Coroutine timerCoroutine;

    private const int SecondsInAMinute = 60;
    private int completionCount;
    private bool isPaused = false;

    void Start()
    {
        InitializeGame();
    }

    private void InitializeGame()
    {
        EndScreen.SetActive(false);
        PlayerMoves = 0;
        Minutes = 0;
        Seconds = 0;
        UpdateUI();

        if (ShouldTime)
        {
            RestartTimer();
        }

        pieces = new List<Transform>(size * size);
        CreateGamePieces();
    }

    private void CreateGamePieces()
    {
        float width = 1f / size;

        for (int row = 0; row < size; row++)
        {
            for (int col = 0; col < size; col++)
            {
                Transform piece = Instantiate(piecePrefab, gameTransform);
                pieces.Add(piece);

                piece.localPosition = new Vector3(-1 + (2 * width * col) + width,
                                                   +1 - (2 * width * row) - width,
                                                   0);
                piece.localScale = ((2 * width) - gapThickness) * Vector3.one;
                piece.name = $"{(row * size) + col}";

                if (row == size - 1 && col == size - 1)
                {
                    emptyLocation = (size * size) - 1;
                    piece.gameObject.SetActive(false);
                }
                else
                {
                    float gap = gapThickness / 2;
                    Mesh mesh = piece.GetComponent<MeshFilter>().mesh;
                    Vector2[] uv = new Vector2[4];

                    uv[0] = new Vector2((width * col) + gap, 1 - ((width * (row + 1)) - gap));
                    uv[1] = new Vector2((width * (col + 1)) - gap, 1 - ((width * (row + 1)) - gap));
                    uv[2] = new Vector2((width * col) + gap, 1 - ((width * row) + gap));
                    uv[3] = new Vector2((width * (col + 1)) - gap, 1 - ((width * row) + gap));

                    mesh.uv = uv;
                }
            }
        }
    }

    private IEnumerator Timer()
    {
        while (ShouldTime && !isPaused)
        {
            yield return new WaitForSeconds(1f);
            Seconds++;
            if (Seconds >= SecondsInAMinute)
            {
                Minutes++;
                Seconds = 0;
            }
            UpdateTimerUI();
        }
    }

    private void UpdateTimerUI()
    {
        UI_Timer.text = $"{Minutes:D2} : {Seconds:D2}";
    }

    private void UpdateMovesUI()
    {
        UI_PlayerMoves.text = $"Moves: {PlayerMoves}";
    }

    private void UpdateUI()
    {
        UpdateTimerUI();
        UpdateMovesUI();
    }

    void Update()
    {

        if (!shuffling && CheckCompletion())
        { 
            shuffling = true;
            StartCoroutine(ResetGameAfterDelay(resetDelayTime));
        }

        if (!isPaused && Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit)
            {
                for (int i = 0; i < pieces.Count; i++)
                {
                    if (pieces[i] == hit.transform)
                    {
                        if (SwapIfValid(i, -size, size)) { break; }
                        if (SwapIfValid(i, +size, size)) { break; }
                        if (SwapIfValid(i, -1, 0)) { break; }
                        if (SwapIfValid(i, +1, size - 1)) { break; }
                    }
                }
                PlayerMoves++;
                UpdateMovesUI();
            }
        }
    }

    private bool SwapIfValid(int i, int offset, int colCheck)
    {
        if (((i % size) != colCheck) && ((i + offset) == emptyLocation))
        {
            (pieces[i], pieces[i + offset]) = (pieces[i + offset], pieces[i]);
            (pieces[i].localPosition, pieces[i + offset].localPosition) = (pieces[i + offset].localPosition, pieces[i].localPosition);
            emptyLocation = i;
            return true;
        }
        return false;
    }

    private bool CheckCompletion()
    {
        for (int i = 0; i < pieces.Count; i++)
        {
            if (pieces[i].name != $"{i}")
            {
                return false;
            }
        }
        return true;
    }

    private IEnumerator ResetGameAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (completionCount == 1)
        {
            EndScreen.SetActive(true);
            ShouldTime = false;
        }
        else
        {
            ResetGameState();
            completionCount++;
        }
    }

    private void ResetGameState()
    {
        Minutes = 0;
        Seconds = 0;
        PlayerMoves = 0;
        UpdateUI();
        RestartTimer();
        StartCoroutine(WaitShuffle(0.1f));
    }

    private void RestartTimer()
    {
        if (timerCoroutine != null) StopCoroutine(timerCoroutine);
        timerCoroutine = StartCoroutine(Timer());
    }

    private IEnumerator WaitShuffle(float duration)
    {
        yield return new WaitForSeconds(duration);
        Shuffle();
        shuffling = false;
    }

    private void Shuffle()
    {
        int count = 0;
        int last = 0;
        while (count < (size * size * size))
        {
            int rnd = Random.Range(0, size * size);
            if (rnd == last) { continue; }
            last = rnd;

            if (SwapIfValid(rnd, -size, size)) { count++; }
            else if (SwapIfValid(rnd, +size, size)) { count++; }
            else if (SwapIfValid(rnd, -1, 0)) { count++; }
            else if (SwapIfValid(rnd, +1, size - 1)) { count++; }
        }
    }

    public void SetCustomGapSize(string inputGapSize)
    {
        if (float.TryParse(inputGapSize, out float newGapSize) && newGapSize >= 0)
        {
            gapThickness = newGapSize;
            RestartGame();
            ErrorMessage.text = "";
        }
        else
        {
            ErrorMessage.text = "Invalid gap size. Please enter a positive number.";
        }
    }

    private void RestartGame()
    {
        if (timerCoroutine != null) StopCoroutine(timerCoroutine);
        foreach (var piece in pieces)
        {
            Destroy(piece.gameObject);
        }
        pieces.Clear();
        InitializeGame();
    }

    public void ResetGame()
    {
        RestartGame();
    }
}


