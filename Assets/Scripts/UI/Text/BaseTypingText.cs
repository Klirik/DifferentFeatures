using System.Collections;
using TMPro;
using UnityEngine;

namespace Template.UI.Text
{
    public abstract class BaseTypingText : MonoBehaviour
    {
        [SerializeField] protected TMP_Text text;
        [SerializeField] protected float typingDelay = 0.05f;

        
        protected void OnValidate()
        {
            if (!text)
                text = GetComponent<TMP_Text>();
            if (!text) Debug.LogError("Cant find TMP_Text", this);
        }
        
        public virtual void Init(string str)
        {
            Reset();
            text.text                 = str;
            text.maxVisibleCharacters = 0;
        }

        public abstract void Launch();

        public virtual void Break() {
            text.maxVisibleCharacters = int.MaxValue;         
        }

        protected virtual void Reset()
        {
            text.text                 = string.Empty;
            text.maxVisibleCharacters = int.MaxValue;
        }
        protected IEnumerator StartTypingText()
        {
            while (text.maxVisibleCharacters != text.text.Length) {
                text.maxVisibleCharacters++;
                yield return new WaitForSeconds(typingDelay);
            }
            Break();
        }

    }
}