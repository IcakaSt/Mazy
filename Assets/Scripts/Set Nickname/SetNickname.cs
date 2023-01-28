using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using Firebase.Database;
using UnityEngine.SceneManagement;
using System.IO;
using System.Text.RegularExpressions;
using UnityEngine;

public class SetNickname : MonoBehaviour
{
    DatabaseReference reference;
    public InputField inputNick;
    public Text errorText;
    private TouchScreenKeyboard keyboard;

    bool none = true;
    bool ready = false;

    Highscore high = new Highscore();

    public TextAsset bannedTextFile;
    string[] allBannedWords;

    private void Start()
    {
        errorText.text = "";
        reference = FirebaseDatabase.DefaultInstance.RootReference;
        PlayerPrefs.SetInt("HomeScreenTutorial", 0);

        allBannedWords = bannedTextFile.text.Split(new string[] { ",", "\n" }, System.StringSplitOptions.RemoveEmptyEntries);
    }

    private void Update()
    {
        if (!none)
        { errorText.text = "This username already exists"; }
        else { high.arenaScore = 0; high.runnerScore = 0; high.total = 0; }

        if (ready)
        { PlayerPrefs.SetString("Username", inputNick.text); SceneManager.LoadScene("Homescreen"); }
    }

    public void SendNickname()
    {
        if (CheckNick())
        {
            FirebaseDatabase.DefaultInstance.GetReference("Highscore").Child(inputNick.text).GetValueAsync().ContinueWith(task =>
            {
                if (task.IsFaulted)
                {
                    errorText.text = "No internet connection";
                }
                else if (task.IsCompleted)
                { 
                    DataSnapshot snapshot = task.Result;
                    if (!snapshot.Exists)
                    {
                        string json = JsonUtility.ToJson(high);
                        Debug.Log(json);
                        FirebaseDatabase.DefaultInstance.RootReference.Child("Highscore").Child(inputNick.text).SetRawJsonValueAsync(json).ContinueWith(task =>
                        {
                            if (task.IsCompleted)
                            {
                                Debug.Log("Completed");
                                ready = true;
                            }
                        });
                    }
                    else { none = false; }
                }
            });
        }
    }

    bool CheckNick()
    {
        if (string.IsNullOrEmpty(inputNick.text))
        { errorText.text = "Write down an username"; return false; }
        else if (inputNick.text.Length < 5)
        { errorText.text = "The username has to be with 5 characters or more"; return false; }
        else if (inputNick.text.Contains("/") || inputNick.text.Contains("$") || inputNick.text.Contains(".") || inputNick.text.Contains("[") || inputNick.text.Contains("]") || inputNick.text.Contains("#"))
        { errorText.text = "The username cannot contains the following characters: / $ . [ ] #"; return false; }
        else if (BadLanguage())
        { errorText.text = "The username cannot contains bad language!"; return false; }
        else { return true; } 
    }


    bool BadLanguage()
    {
        bool lang = false;
        foreach (string s in allBannedWords)
        {
            string profanity = Regex.Replace(s, @"\s+", "");
            if (inputNick.text.Contains(profanity))
            { lang = true; Debug.Log("Found bad word!"); break; }
            else { lang = false; }
        }
        return lang;
    }
}
