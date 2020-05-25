using System.Collections;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public void ManipulateTime(float newTime, float duration)
    {
        // If the time is set to zero, we will need to increase 
        // the time by just a little bit to let some of our loops 
        // or other objects that are executing in the game run correctly.   
        if (Time.timeScale == 0)
            Time.timeScale = 0.1f;

        StartCoroutine(FadeTo(newTime, duration));
    }

    IEnumerator FadeTo(float value, float time)
    {
        // t -> represent base time
        // Time.deltaTime / time ->  This way, we increment this based on the difference of time 
        // from one frame to another instead of arbitrarily running this entire loop 
        // and just running it so fast that it executes and we don't see the transition.
        for (float t = 0f; t < 1; t += Time.deltaTime / time)
        {
            // Lerp allows us to alter a value from a start position 
            // to an end position over a certain period of time.

            // Update ilk kez çağrıldığında a’nın değeri 0 ile 100’ün 
            // ortasındaki sayı, yani 50 olacak. Update ikince kez 
            // çağrıldığında a’nın değeri 50 ile 100’ün arasındaki sayı, 
            // yani 75 olacak. Sonraki frame’de 87.5 diyerekten böyle böyle
            // a sayısı b sayısına yaklaşacak. Fonksiyondaki 0.5’i artırırsak
            // a daha hızlı yaklaşır, azaltırsak daha yavaş yaklaşır.
            Time.timeScale = Mathf.Lerp(Time.timeScale, value, t);

            // Next, we're gonna want to make sure that we don't have a scenario 
            // where time is so close to being near zero that we don't actually reach it 
            // or that it takes a very long time to scale down. So in this case, 
            // we're going to see if time is close enough to zero, we'll just set it to be zero.
            if (Mathf.Abs(value - Time.timeScale) < .01f)
            {
                Time.timeScale = value;
                // At this point, we can also exit out of the loop, 
                // so we'll just return false.

                break;
            }

            // And finally, while this is executing, in order to get this 
            // to execute over multiple frames, 
            // we're gonna have to return null through each iteration. 
            yield return null;
        }
    }
}
