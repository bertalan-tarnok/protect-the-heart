using System.Collections;
using UnityEngine;
using TMPro;

public class Waves : MonoBehaviour
{
    [SerializeField] private Spawner left;
    [SerializeField] private Spawner right;

    [SerializeField] private TMP_Text waveText;

    private Wave[] waves = {
        new Wave(1, 1),
        new Wave(2, 4),
    };

    public IEnumerator Play()
    {
        yield return new WaitForSeconds(1f);

        left.Open();
        right.Open();

        yield return new WaitForSeconds(1f);

        for (int i = 0; i < waves.Length; i++)
        {
            waveText.text = i.ToString();

            left.Wave(waves[i].leftCount, waves[i].leftSpeed);
            right.Wave(waves[i].rightCount, waves[i].rightSpeed);

            yield return new WaitForSeconds(0.2f);

            while (!left.ready || !right.ready)
            {
                yield return new WaitForEndOfFrame();
            }

            yield return new WaitForSeconds(6f);
        }
    }

    public class Wave
    {
        public int leftCount;
        public int rightCount;

        public float leftSpeed;
        public float rightSpeed;

        public Wave(int leftCount, int rightCount, float rightSpeed = 1f, float leftSpeed = 1f)
        {
            this.leftCount = leftCount;
            this.rightCount = rightCount;

            this.leftSpeed = leftSpeed;
            this.rightSpeed = rightSpeed;
        }
    }
}
