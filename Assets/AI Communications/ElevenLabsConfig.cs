using UnityEngine;

[CreateAssetMenu(fileName = "ElevenLabsConfig", menuName = "ElvensLab/ElvenLabs Configuration")]

public class ElevenLabsConfig : ScriptableObject{
    public string apiKey = "";
    public string voiceID = "";
    public string ttsUrl = "https://api.elevenlabs.io/v1/text-to-speech/{0}/stream";
}
