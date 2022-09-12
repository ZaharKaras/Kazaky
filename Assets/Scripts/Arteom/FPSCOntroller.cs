
using UnityEngine;
using TMPro;


public class FPSCOntroller : MonoBehaviour
{
    public TextMeshProUGUI fpsText;

    private float pollingTime = 1f;
    private float time;
    private float frameCount;

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        frameCount++;

        if(time >= pollingTime)
        {
            int frameRate = Mathf.RoundToInt(frameCount/time);

            fpsText.text = frameRate.ToString() + "FPS";

            time -= pollingTime;
            frameCount = 0;
        }
    }
}
