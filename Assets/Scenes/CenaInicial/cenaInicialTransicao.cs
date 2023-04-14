using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Yarn.Unity;

public class cenaInicialTransicao : MonoBehaviour
{
    private DialogueRunner dialogueRunner;
    // Start is called before the first frame update
    void Start()
    {
        dialogueRunner = FindObjectOfType<Yarn.Unity.DialogueRunner>();
    }

    // Update is called once per frame
    void Update()
    {
      

            if (!dialogueRunner.IsDialogueRunning)
               {
            SceneManager.LoadScene(2);
        }

        
    }
}
