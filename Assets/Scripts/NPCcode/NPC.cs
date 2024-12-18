using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NPC : MonoBehaviour
{
    public GameObject NPCPanel;
    public TMP_Text NPCText;
    public string[] dialogue;
    private int index;
    //public GameObject OnActiveInven;
    //public GameObject OnActiveInven1;
    //public GameObject OnActiveInven2;
    //public GameObject OnActiveInven3;

    


    public GameObject contButton;
    public float wordSpeed;
    public bool playerIsClose;
    public GameObject npc;

     void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && playerIsClose)
        {
            if(NPCPanel.activeInHierarchy)
            {
                zeroText();
                Destroy(npc);
                npc = null;

            }
            else
            {
                NPCPanel.SetActive(true);
                //OnActiveInven.SetActive(true);
                //StopAllCoroutines();
                StartCoroutine(Typing());
            }
        }
        if(NPCText.text == dialogue[index])
        {
            contButton.SetActive(true);
        }
    }


    public void zeroText()
    {
        NPCText.text = "";
        index = 0;
        NPCPanel.SetActive(false);
    }

    IEnumerator Typing()
    {
        foreach(char letter in dialogue[index].ToCharArray())
        {
            NPCText.text += letter;
            yield return new WaitForSeconds(wordSpeed);
        }
    }

    public void NextLine()
    {
        contButton.SetActive(false);

        if(index < dialogue.Length - 1)
        {
            index++;
            NPCText.text = "";
            StartCoroutine(Typing());
        }
        else
        {
            zeroText();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsClose = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsClose = false;
        }
    }
}
