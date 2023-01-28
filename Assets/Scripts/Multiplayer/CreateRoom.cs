using System.Collections;
using System.Collections.Generic;
using Firebase.Database;
using Firebase.Extensions;
using Photon.Pun;
using UnityEngine;

public class CreateRoom : MonoBehaviourPunCallbacks
{
    public GameObject[] publicButtons;
    public GameObject[] typeButtons;

    Room room = new Room();

    // Start is called before the first frame update
    void Start()
    {
        Publicity(true);
        TypeRoom("Duos");
    }

    public void Publicity(bool publicity)
    {
        switch (publicity)
        {
            case true: publicButtons[0].SetActive(true); publicButtons[1].SetActive(false); room.publicRoom = true; break;
            case false: publicButtons[0].SetActive(false); publicButtons[1].SetActive(true); room.publicRoom = false; break;
        }

    }
    public void TypeRoom(string type)
    {
        switch (type)
        {
            case "Versus": typeButtons[0].SetActive(false); typeButtons[1].SetActive(true); room.type = "Versus"; break;
            case "Duos": typeButtons[0].SetActive(true); typeButtons[1].SetActive(false); room.type = "Duos"; break;
        }
    }

    public void CreateNewRoom()
    {
        /*/
        bool creating=true;

            room.GenerateCode(5);
            FirebaseDatabase.DefaultInstance.GetReference("Rooms").Child(room.code).GetValueAsync().ContinueWithOnMainThread(task =>
            {
                if (task.IsFaulted)
                {
                    Debug.Log("Not completed");
                    // errorText.text = "No internet connection";
                }
                else if (task.IsCompleted)
                {
                    Debug.Log("Completed");
                    DataSnapshot snapshot = task.Result;
                    if (string.IsNullOrEmpty(snapshot.ToString()))
                    {
                        Debug.Log("Not found");
                        creating = false;
                    }
                }
            });
    

        string roomstring = JsonUtility.ToJson(room);
        FirebaseDatabase.DefaultInstance.RootReference.Child("Rooms").Child(room.code).SetRawJsonValueAsync(roomstring).ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                Debug.Log("Created");
            }
        });
         /*/


        PhotonNetwork.CreateRoom("aa");
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("Online Test");
    }

}
