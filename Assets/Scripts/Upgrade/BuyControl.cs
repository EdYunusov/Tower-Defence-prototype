using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpaseShooter;

public class BuyControl : MonoBehaviour
{
    [SerializeField] private TowerBuyController m_TowerBuyPrefab;
    //[SerializeField] private TowerAsset[] m_TowerAssets;
    //[SerializeField] private UpgradeAsset m_UpgradeAsset;

    private List<TowerBuyController> m_ActiveController;
    private RectTransform m_RectTransform;

    private void Awake()
    {
        m_RectTransform = GetComponent<RectTransform>();
        TowerBuildSite.OnClickEvent += MoveToBuildSite;
        gameObject.SetActive(false);
    }

    private void MoveToBuildSite(TowerBuildSite buildSite)
    {
        if(buildSite)
        {
            var position = Camera.main.WorldToScreenPoint(buildSite.transform.root.position);
            print(position);
            m_RectTransform.anchoredPosition = position;
            m_ActiveController = new List<TowerBuyController>();
            foreach (var asset in buildSite.buildableTowers)
            {
                if((asset.IsAvaible()))
                {
                    var newController = Instantiate(m_TowerBuyPrefab, transform);
                    m_ActiveController.Add(newController);
                    newController.SetTowerAsset(asset);
                }
            }
            if (m_ActiveController.Count > 0)
            {
                var angle = 360 / m_ActiveController.Count;
                gameObject.SetActive(true);

                for (int i = 0; i < m_ActiveController.Count; i++)
                {
                    var offset = Quaternion.AngleAxis(angle * i, Vector3.forward) * (Vector3.left * 100);
                    m_ActiveController[i].transform.position += offset;
                }

                foreach (var tbc in GetComponentsInChildren<TowerBuyController>())
                {
                    tbc.SetBuildSite(buildSite.transform.root);
                }
            }
        }
        else
        {
            if(m_ActiveController != null)
            {
                foreach (var control in m_ActiveController) Destroy(control.gameObject);
                m_ActiveController.Clear();
            }
           
            gameObject.SetActive(false);
        }
    }

    private void OnDestroy()
    {
        TowerBuildSite.OnClickEvent -= MoveToBuildSite;
    }
}
