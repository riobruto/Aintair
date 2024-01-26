using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UI_Fade : MonoBehaviour
    {
        internal int _targetValue;
        private Image fade;
        private Text _score;

        private void Start()
        {
            fade = GetComponent<Image>();
        }

        private void Update()
        {
            fade.color = new Color(0, 0, 0, Mathf.Lerp(fade.color.a, _targetValue, Time.deltaTime*.5f));
        }

        private IEnumerator ShowScore()
        {
            _score.text = GameSystem.JetsSytem.Instance.JetCount.ToString();
            yield return null;
        }
    }
}