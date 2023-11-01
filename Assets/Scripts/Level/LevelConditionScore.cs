using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaseShooter
{
    public class LevelConditionScore : MonoBehaviour, ILevelCondition
    {
        [SerializeField] private int killCount;

        private bool m_Reached;

        bool ILevelCondition.IsComplited
        {
            get
            {
                if(Player.Instance != null && Player.Instance.ActiveShip != null)
                {
                    if(Player.Instance.killCount >= killCount)
                    {
                        m_Reached = true;
                        Debug.Log("Reached!");
                        LevelSequenceController.Instance.FinishCurrentLevel(m_Reached);
                    }
                } 
                return m_Reached;
            }
        }
    }
}

