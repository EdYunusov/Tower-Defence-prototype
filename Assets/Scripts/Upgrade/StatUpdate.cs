using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatUpdate : MonoBehaviour
{
    public enum UpdateSource { Gold, Life, Mana }
    public UpdateSource source;

    private Text m_text;

    void Start()
    {
        m_text = GetComponent<Text>();
        
        switch (source)
        {
            case UpdateSource.Gold:
                TDPlayer.Instance.GoldUpdateSub(UITextUpdate);
                break;
            case UpdateSource.Life:
                TDPlayer.Instance.LifeUpdateSub(UITextUpdate);
                break;
            case UpdateSource.Mana:
                TDPlayer.Instance.ManaUpdateSub(UITextUpdate);
                break;
        }
    }

    private void UITextUpdate(int money)
    {
        m_text.text = money.ToString();
    }
}
