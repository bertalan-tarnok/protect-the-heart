using System.Collections;
using UnityEngine;
using TMPro;

public class Waves : MonoBehaviour
{
    [SerializeField] private Spawner left;
    [SerializeField] private Spawner right;

    [SerializeField] private TMP_Text waveText;
    [SerializeField] private TMP_Text waveTextLeft;
    [SerializeField] private TMP_Text waveTextRight;

    private Wave[] waves = {
        new Wave(1, 1, 0.5f, 0.5f),
        new Wave(0, 6),
        new Wave(7, 1),
        new Wave(4, 4),
        new Wave(7, 5, 3, 1),
        new Wave(4, 4, 4.5f, 4.5f),
        new Wave(0, 10, 1, 6),
        new Wave(10, 0, 6, 1),
        new Wave(10, 10, 5, 5),
        new Wave(20, 20, 7, 7),
    };

    public IEnumerator Play()
    {
        yield return new WaitForSeconds(1f);

        left.Open();
        right.Open();

        waveTextLeft.text = waves[0].leftCount.ToString();
        waveTextRight.text = waves[0].rightCount.ToString();

        yield return new WaitForSeconds(1f);

        for (int i = 0; i < waves.Length; i++)
        {
            waveText.text = (i + 1).ToString();

            left.Wave(waves[i].leftCount, waves[i].leftSpeed);
            right.Wave(waves[i].rightCount, waves[i].rightSpeed);

            yield return new WaitForSeconds(0.2f);

            while (!left.ready || !right.ready)
            {
                yield return new WaitForEndOfFrame();
            }

            if (i < waves.Length - 1)
            {
                waveTextLeft.text = waves[i + 1].leftCount.ToString();
                waveTextRight.text = waves[i + 1].rightCount.ToString();
            }

            yield return new WaitForSeconds(5f);
        }

        GameManager.EndGame();
    }

    public class Wave
    {
        public int leftCount;
        public int rightCount;

        public float leftSpeed;
        public float rightSpeed;

        public Wave(int leftCount, int rightCount, float leftSpeed = 1f, float rightSpeed = 1f)
        {
            this.leftCount = leftCount;
            this.rightCount = rightCount;

            this.leftSpeed = leftSpeed;
            this.rightSpeed = rightSpeed;
        }
    }
}
