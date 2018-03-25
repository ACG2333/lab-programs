using UnityEngine;

public class BoomEffectLight : MonoBehaviour {

    public float lightIntensity = 3;

    private bool havePlay = false;
    private bool haveTop = false;

    void Update()
    {
        /*控制光*/
        if(!haveTop)
        {
            GetComponent<Light>().range += 38* Time.deltaTime;
            GetComponent<Light>().intensity += 15 * Time.deltaTime;
            if (GetComponent<Light>().intensity > lightIntensity)
                haveTop = true;
        }
        else
        {
            GetComponent<Light>().range -= 38 * Time.deltaTime;
            GetComponent<Light>().intensity = 18f * Time.deltaTime;
        }

        /*播放特效*/
        if (!havePlay)
        {
            GetComponent<ParticleSystem>().Play();
            havePlay = true;
        }

        /*播放完之后删除*/
        if (havePlay && !GetComponent<ParticleSystem>().isPlaying)
        {
            Destroy(gameObject);
        }
    }
}
