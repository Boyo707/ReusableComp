using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIScore : MonoBehaviour
{
    [SerializeField]private int _score;
    private TMP_Text _text;
    private string _numberTemp;
    int startingnum;
    //10 spots
    void Start()
    {
        _text = GetComponent<TMP_Text>();
        //startingnum = (int)Mathf.Floor(Mathf.Log10(_score) + 1);
    }

    // Update is called once per frame
    void Update()
    {
        thingy(_score);
        int firstNumber = (_score / 1000000000) % 10;

        if (firstNumber != 0)
        {
            

            //Debug.Log(firstNumber+ "first" + secondNumber + "second" + thirdNumber + "thirt");
            
            //_text.text = $"<sprite={firstNumber}> <sprite={secondNumber}>";
            _text.text = thingy(_score);
        }
    }

    private string thingy(int score)
    {
        //Als ik de starting number aanpas dan gaat het de eerste number weg halen. Maar als ik de score in de inspector aanpas-
        //dan laat het 0 zien. moet met debugs kijken waar het fout gaat.
        long[] stuffy = new long[(long)Mathf.Floor(Mathf.Log10(_score) + 1) + 1];
        long startingNumber = 1000000000;
        Debug.Log("mathf thing  " + Mathf.Floor(Mathf.Log10(_score) + 1));
        for (int i = 0; i < stuffy.Length; i++)
        {
            stuffy[i] = startingNumber;
            for (int t = 0; t < i; t++)
            {
                stuffy[i] /= 10;
                
            }
            if (stuffy[i] == 1)
                stuffy[i] = 0;
            Debug.Log("10 things " + stuffy[i]);
        }
        Debug.Log("stuffy " + stuffy.Length);
        long[] painfull = new long[stuffy.Length];
        for (int i = 0; i < painfull.Length; i++)
        {
            if (stuffy[i] == 0)
                painfull[i] = score % 10;
            else
                painfull[i] = (score / stuffy[i]) % 10;

            Debug.Log("the number " + painfull[i]);
        }
        Debug.Log("pain " + painfull.Length);

        string[] textNumbers = new string[painfull.Length];
        for (int i = 0; i < textNumbers.Length; i++)
        {
            
            textNumbers[i] = $"<sprite={painfull[i]}> ";
        }

        string fullString = "";

        for (int i = 0; i < textNumbers.Length; i++)
        {
            fullString += textNumbers[i];
        }

        return fullString;
    }

    public void AddPoints(int points)
    {
        _score += points;
    }
}
