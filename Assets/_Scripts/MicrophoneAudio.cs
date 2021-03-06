using UnityEngine;
[RequireComponent(typeof(AudioSource))]

public class MicrophoneAudio : MonoBehaviour {

  public static int numSamples = 512;
  AudioSource audioSource;
  
  public static float[] samples = new float[numSamples];
  
  // Use this for initialization
  void Start() {
  
    audioSource = GetComponent<AudioSource>();
    
    if (Microphone.devices.Length > 0) {
      string micro = Microphone.devices[0];
      audioSource.clip = Microphone.Start(micro, true, 1, AudioSettings.outputSampleRate);
      audioSource.loop = true;
      
      if (Microphone.IsRecording(micro)) { 
        // check that the mic is recording, otherwise you'll get stuck in an infinite loop waiting for it to start
        
        while (!(Microphone.GetPosition(micro) > 0)) { } // Wait until the recording has started.
        // Start playing the audio source
        
        audioSource.Play();
      }
    }
 }


  // Update is called once per frame
  void Update() {
    GetComponent<AudioSource>().clip.GetData(samples, audioSource.timeSamples);
  }
  
}
