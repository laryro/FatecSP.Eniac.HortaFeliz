using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PtTwoController : MonoBehaviour {


    [SerializeField]
    private Sprite bgImage;

    public Sprite[] puzzles;

    public List<Sprite> gamePuzzeles = new List<Sprite>();

    public List<Button> btns = new List<Button>();

    private bool firstGuess = false;

    private bool SecondGuess = false;

    private int countGuessPuzzel;
    private int countCorrectGuesses;
    private int gameGuesses;

    private string firstGuessesPuzzle, secondGuessPuzzle;

    private int firstGuessesIndex, secondGuessIndex;

    private void Start()
    {
        GetButtons();
        AddListeners();
        AddGamePuzzele();
        Shuffle(gamePuzzeles);
        gameGuesses = gamePuzzeles.Count / 2;

    }


    public void Awake()
    {
        puzzles = Resources.LoadAll<Sprite>("");
    }

    void GetButtons() {

        GameObject[] objects = GameObject.FindGameObjectsWithTag("PuzzleButton");

        for (int i = 0; i < objects.Length; i++)
        {
          btns.Add(objects[i].GetComponent<Button>());
            btns[i].image.sprite = bgImage;
        }


    }

    public void AddGamePuzzele() {
        int looper = btns.Count;

        int index = 0;

        for (int i = 0; i < looper; i++)
        {

            if (index == looper / 2) {
                index = 0;

            }

            gamePuzzeles.Add(puzzles[index]);

            index++;
        }


    }


    void AddListeners() {

        foreach (var item in btns)
        {
            item.onClick.AddListener(() => PickPuzzle());
        }

    }



    public void PickPuzzle() {

        Debug.Log(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);

        if (!firstGuess)
        {
            firstGuess = true;

            firstGuessesIndex = int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);

            firstGuessesPuzzle = gamePuzzeles[firstGuessesIndex].name;

            btns[firstGuessesIndex].image.sprite = gamePuzzeles[firstGuessesIndex];
        }
        else if (!SecondGuess) {

            SecondGuess = true;

            secondGuessIndex = int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);

            secondGuessPuzzle = gamePuzzeles[secondGuessIndex].name;

            btns[secondGuessIndex].image.sprite = gamePuzzeles[secondGuessIndex];


            gameGuesses++;

            StartCoroutine(CheckIfThePuzzlesMatch());

        }
    }



    IEnumerator CheckIfThePuzzlesMatch() {

        yield return new WaitForSeconds(1f);

        if (firstGuessesPuzzle == secondGuessPuzzle)
        {

            yield return new WaitForSeconds(.5f);


            btns[firstGuessesIndex].interactable = false;
            btns[secondGuessIndex].interactable = false;

            btns[firstGuessesIndex].image.color = new Color(0, 0, 0, 0);
            btns[secondGuessIndex].image.color = new Color(0, 0, 0, 0);


            CheckIfTheGameFinished();

        }
        else {

            yield return new WaitForSeconds(.5f);

            btns[firstGuessesIndex].image.sprite = bgImage;

            btns[secondGuessIndex].image.sprite = bgImage;

        }

        yield return new WaitForSeconds(.5f);

        firstGuess = SecondGuess = false;

    }

    void CheckIfTheGameFinished() {

        countCorrectGuesses++;

        if (countCorrectGuesses == gameGuesses) {
            Debug.Log("Game Fim");

            Debug.Log(gameGuesses);


        }


    }


    void Shuffle(List<Sprite> list ) {

        for (int i = 0; i < list.Count; i++)
        {
            Sprite temp = list[i];
            int randomIndex = Random.Range(i, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;

        }
    }




}
