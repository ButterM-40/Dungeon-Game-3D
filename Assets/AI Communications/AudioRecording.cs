using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Windows.Speech;

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
    }
    //chooose which clip to play based on number key..
}
}