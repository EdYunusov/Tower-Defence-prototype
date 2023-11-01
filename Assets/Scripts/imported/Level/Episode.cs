using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaseShooter
{
    [CreateAssetMenu]
    public class Episode : ScriptableObject
    {
        [SerializeField] private string m_EpisodeName;

        public string EpisodeName => m_EpisodeName;

        [SerializeField] private string[] m_Levels;

        public string[] Levels => m_Levels;

        [SerializeField] private Sprite m_PreviewLevel;

        public Sprite PreviewLevel => m_PreviewLevel;
    }
}

