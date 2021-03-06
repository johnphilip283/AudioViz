using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lineCreation : MonoBehaviour {
        
        private Color c1 = Color.blue;
        private Color c2 = Color.red;
        private LineRenderer lineRenderer;
        private float waveScale = 125;
        private int numCornerVertices = 500;
        private float centerScale = 50;
        private int peak = 256;

        void Start() {
                Debug.developerConsoleVisible = true;
                lineRenderer = gameObject.AddComponent<LineRenderer>();
                lineRenderer.material = new Material(Shader.Find("Particles/Additive"));
                lineRenderer.startColor = c1;
                lineRenderer.endColor = c2;
                lineRenderer.startWidth = 0.2F;
                lineRenderer.endWidth = 0.2F;
                ineRenderer.numCornerVertices = numCornerVertices;
                lineRenderer.positionCount = MicrophoneAudio.numSamples;
        }

        void Update() {
                
                Vector3[] positions = new Vector3[MicrophoneAudio.numSamples];
                
                for (int i = 0; i < MicrophoneAudio.numSamples; i++) {
                        float amp = MicrophoneAudio.samples[i];
                        
                        // Some cool ~Gaussian blurring~ on the data to eliminate most of the noise coming through the microphone.
                        positions[i] = new Vector3(i * 0.5F,
                                                   Mathf.Pow(Mathf.Exp(1), -0.5f * Mathf.Pow((i - peak) / centerScale, 2)) * waveScale * amp * Mathf.Sin(i),
                                                   0);
                }

                lineRenderer.SetPositions(positions);
        }
}
