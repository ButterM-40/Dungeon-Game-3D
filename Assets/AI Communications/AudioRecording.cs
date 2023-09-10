using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Windows.Speech;
 
public class AudioRecording : MonoBehaviour {
 
    bool isRecording = false;
    private AudioSource audioSource;
    private string recognizedText = "";
 
    //temporary audio vector we write to every second while recording is enabled..
    List<float> tempRecording = new List<float>();
 
    //list of recorded clips...
    List<float[]> recordedClips = new List<float[]>();
 
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
 
    // void ResizeRecording()
    // {
    //     if (isRecording)
    //     {
    //         //add the next second of recorded audio to temp vector
    //         int length = 44100;
    //         float[] clipData = new float[length];
    //         audioSource.clip.GetData(clipData, 0);
    //         tempRecording.AddRange(clipData);
    //         Invoke("ResizeRecording", 1);
    //     }
    // }
 
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.X)){
            Debug.Log("Testing");
            isRecording = !isRecording;
            if(isRecording){
                audioSource.clip = Microphone.Start(null, false,100, 44100);
            }else{
                Microphone.End(null);
            }
        }
        if(Input.GetKeyDown(KeyCode.Z)){
            Invoke("AudioPlay", 1f);
        }
    }
 
    void AudioPlay(){
        audioSource.Play();
    }
    //chooose which clip to play based on number key..
}