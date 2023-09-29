using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    public Collider2D collid;
    public bool sign, playerInRange;
    public Animator dialogueAnim;
    public Sprite icon;
    public GameObject visulCue;

    

    private void Start()
    {
        //dialogueAnim = 
    }

    private void Update()
    {


        if (playerInRange)
        {

            if (DialogueManager.instance.dialogueIsPlaying)
            {
                if (visulCue != null)
                    visulCue.SetActive(false);
            }
            else
            {
                if (visulCue != null)
                    visulCue.SetActive(true);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            playerInRange = true;
            PlayerController.instance.dig = this;

        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            playerInRange = false;

            if (visulCue != null)
                visulCue.SetActive(false);

            PlayerController.instance.dig = null;
        }
    }

   


    public void TriggerDialogue()
    {

        if (playerInRange)
        {


            if (dialogueAnim.GetBool("isOpen") == false)
            {

                DialogueManager.instance.StartDialogue(dialogue);


            }
            else
            {
                DialogueManager.instance.DisplayNextSentence();


            }






        }

    }
}
