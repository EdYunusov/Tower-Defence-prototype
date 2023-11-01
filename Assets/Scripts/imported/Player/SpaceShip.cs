using System;
using UnityEngine;


namespace SpaseShooter
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class SpaceShip : Destructible
    {
        [Header("Space ship")]
        [SerializeField] private float m_Mass;
        [SerializeField] private float m_Thrust;
        [SerializeField] private float m_Mobility;

        [SerializeField] private float m_MaxLinearVelocity;
        private float m_MaxVelocitryBackup;

        [SerializeField] private float m_MaxAngularVelocity;


        [SerializeField] private Sprite m_PreviewImage;

        public float MaxLinearVelocity => m_MaxLinearVelocity;

        public void HalfMaxLinearVelosity() 
        { 
            m_MaxVelocitryBackup = m_MaxLinearVelocity; 

            if(m_MaxLinearVelocity >= 0.4f)
            {
                m_MaxLinearVelocity /= 2;
            }

            
        }
        public void RecoverfMaxLinearVelosity() { m_MaxLinearVelocity = m_MaxVelocitryBackup; }

        public float MaxAngularVelocity => m_MaxAngularVelocity;
        public Sprite PreviewImage => m_PreviewImage;

        private Rigidbody2D m_Rigid;


        #region Public API

        public float upgradeThrust
        {
            get
            {
                return m_Thrust;
            }
            set
            {
                upgradeThrust = value;
            }
        }

        public float ThrustControl { get; set; }

        public float TorqueControl { get; set; }
        #endregion

        protected override void Start()
        {
            base.Start();

            m_Rigid = GetComponent<Rigidbody2D>();
            m_Rigid.mass = m_Mass;

            m_Rigid.inertia = 1;

            InitOffence();
        }

        private void FixedUpdate()
        {
            UpdateRigidBody();
            UpdateEnergyRegen();
        }

        private void UpdateRigidBody()
        {
            m_Rigid.AddForce(ThrustControl * m_Thrust * transform.up * Time.fixedDeltaTime, ForceMode2D.Force);

            m_Rigid.AddForce(-m_Rigid.velocity * (m_Thrust / m_MaxLinearVelocity) * Time.fixedDeltaTime, ForceMode2D.Force);

            m_Rigid.AddTorque(TorqueControl * m_Mobility * Time.fixedDeltaTime, ForceMode2D.Force);

            m_Rigid.AddTorque(-m_Rigid.angularVelocity * (m_Mobility / m_MaxAngularVelocity) * Time.fixedDeltaTime, ForceMode2D.Force);
        }

        [SerializeField] private Turret[] m_Turrets;

        public void Fire(TurretMode mode)
        {
            for (int i = 0; i<m_Turrets.Length; i++)
            {
                if(m_Turrets[i].Mode == mode)
                {
                    m_Turrets[i].Fire();
                }
            }
        }

        [SerializeField] private int m_MaxEnergy;
        [SerializeField] private int m_MaxAmmo;
        [SerializeField] private int m_EnergyRegenPerSecond;

        private float m_PrimaryEnergy;
        private int m_SecondaryAmmo;

        public void AddEnergy(int energy)
        {
            m_PrimaryEnergy = Mathf.Clamp(m_PrimaryEnergy + energy, 0, m_MaxEnergy);
        }

        public void AddAmmo(int ammo)
        {
            m_SecondaryAmmo = Mathf.Clamp(m_SecondaryAmmo + ammo, 0, m_MaxEnergy);
        }

        private void InitOffence()
        {
            m_PrimaryEnergy = m_MaxEnergy;
            m_SecondaryAmmo = m_MaxAmmo;
        }

        private void UpdateEnergyRegen()
        {
            m_PrimaryEnergy += (float)m_EnergyRegenPerSecond * Time.fixedDeltaTime;
            m_PrimaryEnergy = Mathf.Clamp(m_PrimaryEnergy, 0, m_MaxEnergy );
        }

        public bool DrawAmmo(int count)
        {
            //if (count == 0) return true;

            //if(m_SecondaryAmmo >= count)
            //{
            //    m_SecondaryAmmo -= count;
            //    return true;
            //}


            return false;
        }

        public bool DrawEnrgy(int count)
        {
            //if (count == 0) return true;

            //if (m_PrimaryEnergy >= count)
            //{
            //    m_PrimaryEnergy -= count;
            //    return true;
            //}
            return false;
        }

        public void AssignWeapon(TurretProp props)
        {
            for(int i = 0; i < m_Turrets.Length; i++)
            {
                m_Turrets[i].AssignLoadout(props);
            }
        }


        //private float m_SpeedBeforeUp;
        //private float m_Timer;
        //private float m_Timer_Imortality;
        //private bool m_is_SpeedBust;

        //public void SpeedUp(float BuffForce, float time)
        //{
        //    m_Timer = time;
        //    m_is_SpeedBust = true;

        //    m_SpeedBeforeUp = m_Thrust;
        //    if (time > 0)
        //    {
        //        m_Thrust += BuffForce;
        //    }
        //}

        //public void Imortality(float time)
        //{
        //    m_Timer_Imortality = time;
        //    m_Indestructible = false;

        //    if (time > 0)
        //    {
        //        m_Indestructible = true;
        //    }
        //}

        //private void Update()
        //{
        //    if (m_is_SpeedBust == true)
        //    {
        //        m_Timer -= Time.deltaTime;
        //        if (m_Timer <= 0)
        //        {
        //            m_Thrust = m_SpeedBeforeUp;
        //            m_is_SpeedBust = false;
        //        }
        //    }

        //    if(m_Indestructible == true)
        //    {
        //        m_Timer_Imortality -= Time.deltaTime;
        //        if (m_Timer_Imortality <= 0)
        //        {
        //            m_Indestructible = false;
        //        }
        //    }
        //}

        public void Use(EnemyAssets asset)
        {
            m_MaxLinearVelocity = asset.moveSpeed;
            base.Use(asset);
        }
    }
}

