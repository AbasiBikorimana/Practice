using UnityEngine;
using TMPro;

public class FrameRateCounter : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI display;

    public enum DisplayMode { FPS, MS }

    [SerializeField]
    DisplayMode displayMode = DisplayMode.FPS;

    [SerializeField, Range(0.1f, 2f)]
    float sampleDuration = 1f;

    int frames;
    float duration, BestDuration = float.MaxValue, WorstDuration;

    void Update()
    {
        float FrameDuration = Time.unscaledDeltaTime;
        frames += 1;
        duration += FrameDuration;

        if (FrameDuration < BestDuration)
        {
            BestDuration = FrameDuration;
        }
        
        if (FrameDuration > BestDuration)
        {
            WorstDuration = FrameDuration;
        }

        if (duration >= sampleDuration)
        {
            if (displayMode == DisplayMode.FPS)
            {
                display.SetText
                (
                    "FPS\n{0:0}\n{1:0}\n{2:0}", 
                    1f / BestDuration,
                    frames / duration,
                    1f / WorstDuration
                );
            }
            else
            {
            display.SetText
                (
                    "MS\n{0:1}\n{1:1}\n{2:1}", 
                    1000f * BestDuration,
                    1000f * duration / frames,
                    1000f * WorstDuration
                );
            }

            frames = 0;
            duration = 0f;
            BestDuration = float.MaxValue;
            WorstDuration = 0f;
        }

    }
}
