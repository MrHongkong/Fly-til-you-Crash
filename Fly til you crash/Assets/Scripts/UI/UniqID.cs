using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UniqID : MonoBehaviour
{
    public static string GetUniqueID(String username)
    {
        string[] split = System.Guid.NewGuid().ToString().Split(new Char[] { ':', '.' });
        string id = "";
        for (int i = 0; i < split.Length; i++)
        {
            id = username + " " + split[i];
        }
        return id;
    }
}
