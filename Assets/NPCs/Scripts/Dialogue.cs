using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class Dialogue
{
    private string path;
    public string name;
    public string[] sentences;

    public Dialogue(string name, string path_sufix){
        this.path = Application.dataPath + path_sufix;
        this.name = name;
        sentences = File.ReadAllLines(this.path);
    }
}
