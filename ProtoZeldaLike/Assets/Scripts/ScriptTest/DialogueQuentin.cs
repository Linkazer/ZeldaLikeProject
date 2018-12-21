using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueQuentin : MonoBehaviour {

    public TextMesh textDisplay;
    public string[] sentences;
    private int index;
    public float typingSpeed;

    void Start()
    {
        StartCoroutine(Type());
    }

    private void Update()
    {
        if (Input.GetKeyDown("a"))
        {
            NextSentence();
        }
    }

    IEnumerator Type()
    {
        foreach (char letter in sentences[index].ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    public void NextSentence()
    {
        if (index < sentences.Length - 1)
        {
            index++;
            textDisplay.text = "";
            StartCoroutine(Type());
        }
        else
        {
            textDisplay.text = "";
        }
    }
}
