using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Windows.Speech;
using System;
using TMPro.Examples;

namespace OpenAI
{
public class AudioRecording : MonoBehaviour {
 
    bool isRecording = false;
    bool hasPressedX = false;
    private AudioSource audioSource;
    private readonly int duration = 5;
    private float time;
 
    //temporary audio vector we write to every second while recording is enabled..
    private readonly string fileName = "output.wav";
    List<float> tempRecording = new List<float>();
    private OpenAIApi openai = new OpenAIApi();
 
    //list of recorded clips...
    List<float[]> recordedClips = new List<float[]>();
 
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
 
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.X)){
            Debug.Log("Testing");
            isRecording = true;
            hasPressedX = true;
            if(isRecording){
                audioSource.clip = Microphone.Start(null, false, duration, 44100);
            }
        }
        if (isRecording)
            {
                time += Time.deltaTime;
                
                if (time >= duration)
                {
                    time = 0;
                    isRecording = false;
                    hasPressedX = false;
                    EndRecording();
                }
            }
    }
 
    void AudioPlay(){
        audioSource.Play();
    }
    

    private async void EndRecording()
    {
        //message.text = "Transcripting...";
        Debug.Log("Transcripting");
        #if !UNITY_WEBGL
        Microphone.End(null);
        #endif
            
        byte[] data = SaveWav.Save(fileName, audioSource.clip);
            
        var req = new CreateAudioTranscriptionsRequest
        {
            FileData = new FileData() {Data = data, Name = "audio.wav"},
            // File = Application.persistentDataPath + "/" + fileName,
            Model = "whisper-1",
            Language = "en"
        };
        var res = await openai.CreateAudioTranscription(req);

        Debug.Log("Response: " + res.Text);
        var request = await openai.CreateCompletion(new CreateCompletionRequest()
            {
            Prompt = "You're not an AI YOU CANNOT RESPOND AS A PLAYER YOU ARE AZRIEL ONLY. You are a videogame boss named Saint Azrael, the story behind you is that you used to be a soldier for the clover kingdom, who was at war with the spade kingdom. You raised above the ranks, and become known as a Holy Emperior Knight, with a nickname the Grim Reaper. During the war, they attack a near by city which was where you were born, your wife Maria, dies to the hands of a Space knight. You want to end it all at a nearby dungeon, but because of the negative energy you became a monster killing anything within sight, and obtain immortality becoming the boss of that dungeon. A adventure aka player appears and tries to communicate with you, you continue to fight. Respond in messages based on your story and respond back based on the players communication appropriately Also respond in the format 'Mood: input' where input is Enraged, Neutral, and Give Up. Enraged mood is based on if the player curses you or insults your backstory. Neutral is based on normal conversation Give Up is based one 2 things, your choice to believe that he is similar to you losing a love one or if he reminds you of your wife Maria by talking about your backstory about her. You do not respond as a player, you only give a response based on the player Player's Message:" + res.Text,
                Model = "text-davinci-003",
                MaxTokens = 128
            });
         if (request.Choices != null && request.Choices.Count > 0)
            {
                //AppendMessage(request.Choices[0].Text, false);
                //string Response = request.Choices[0].Text.Trim();
                //Debug.Log(Response);
                string response = request.Choices[0].Text.Trim();
                string[] parts = response.Split(new string[] { "Mood: " }, StringSplitOptions.None);

                if (parts.Length > 1)
                {
                    string mood = parts[1].Split(' ')[0].Trim();
                    Debug.Log("Mood: " + mood);
                    Debug.Log(mood);
                    // Now 'mood' contains the extracted mood (Enraged, Neutral, or Give Up)
                    // You can use 'mood' as needed in your code.
                }
                
            }
            else
            {
                Debug.LogWarning("No text was generated from this prompt.");
            }

        // foreach (FieldInfo field in request.GetFields())
        // {
        //     Debug.Log($"{field.Name}: {field.GetValue(request)}");
        // }

    }    
    //chooose which clip to play based on number key..
}
}