using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName="message Object")]
public class messageObjects : ScriptableObject
{
    public string Sender;//The name of the sender
    public string Message;//The actual message
    public bool custom = false;//Used to class the object as custom or normal
    public string customType;// Used to set the type if custom
    public string customButtonTitle;// Used to change the text of the button if custom

    public bool notRead = true;//Makes the message as unread by default
}
