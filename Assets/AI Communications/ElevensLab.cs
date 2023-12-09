using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;

[Serializable]
public class VoiceSettings1{
    public float stability;
    public float similarity_boost;
}
[Serializable]
public class TTSData1{
    public string text;
    public string model_id;
    public VoiceSettings1 voice_settings;
}
public class ElevensLab : MonoBehaviour
{
    public ElevenLabsConfig config;
    public AudioSource audioSource;
    public string text;
    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(GeneratorAndStreamAudio(text));
    }
    public void StartEvent(string text){
        Debug.Log(text);
        StartCoroutine(GeneratorAndStreamAudio(text));
    }

    // Update is called once per frame
    public IEnumerator GeneratorAndStreamAudio(string text)
    {
        string modelID = "eleven_multilingual_v2";
        string url = string.Format(config.ttsUrl, config.voiceID);

        TTSData1 ttsData = new TTSData1{
            text = text.Trim(),
            model_id = modelID,
            voice_settings = new VoiceSettings1{
                stability = 0.5f,
                similarity_boost = 0.8f
            }
        };

        string jsonData = JsonUtility.ToJson(ttsData);
        byte[]bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);

        using(UnityWebRequest request = new UnityWebRequest(url, UnityWebRequest.kHttpVerbPOST)){
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerAudioClip(new Uri(url), AudioType.MPEG);
            request.SetRequestHeader("Content-Type", "application/json");
            request.SetRequestHeader("xi-api-key", config.apiKey);

            yield return request.SendWebRequest();

            if(request.result != UnityWebRequest.Result.Success){
                Debug.LogError("Error: " + request.error);
                yield break;
            }
            AudioClip audioClip = DownloadHandlerAudioClip.GetContent(request);

            if(audioClip != null){
                audioSource.clip = audioClip;
                PlayAudio(audioClip);
                yield return new WaitForSeconds(audioClip.length*0.1f);
            }else{
                yield return StartCoroutine(GeneratorAndStreamAudio(text));
            }

            yield return new WaitForSeconds(audioClip.length);
        }
    }
    private void PlayAudio(AudioClip audioClip){
        audioSource.PlayOneShot(audioClip);
    }
}
