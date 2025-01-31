using Game.Combat;
using Game.Player;
using UnityEngine;
using UnityEngine.UI;




namespace Game.Ui.Hud
{
    public class Hud : MonoBehaviour
    {
        [SerializeField] private Slider LifeBar;

        [field: SerializeField] public Animator PlayerAnimator { get; private set; }

        [SerializeField] private TMPro.TMP_Text GoldText;
        [SerializeField] private TMPro.TMP_Text LifeText;

        [SerializeField] NpcMenuController NpcMenu;

        [SerializeField] private GameObject pauseScreen, deathScreen;

        [SerializeField] private GameObject commandsCanvas;



        private PlayerDataController PlayerData;
        [SerializeField] Animator CoinAnimator;

        bool menuPauseOpen = false; //IGOR AQUI

        private void Awake()
        {
            PlayerData = FindAnyObjectByType<PlayerDataController>();
            CombatEvents.onSendPlayerData.AddListener(ChangeLifeBar);
            UiEvents<string>.onNpcInteracted.AddListener(OpenNpcMenu);
            UpdateSliderMaxValue(PlayerData.PlayerAttributes.MaxHp);
            LifeBar.value = PlayerData.PlayerAttributes.Hp;
            UpdateLifeTxt(PlayerData.PlayerAttributes.Hp, PlayerData.PlayerAttributes.MaxHp);
            GoldText.text = "0";
        }

        private void Update()
        {
            //if(Input.GetKeyDown(KeyCode.Escape))
            //pauseScreen.SetActive(true);
            if (Input.GetKeyDown(KeyCode.I))
            {
                //NpcMenu.gameObject.SetActive(true);
            }
            if (Input.GetKeyDown(KeyCode.Escape)) 
            {
                if (!menuPauseOpen)
                {
                    pauseScreen.SetActive(true);
                    menuPauseOpen = true; 
                }
                else
                {
                    pauseScreen.SetActive(false);
                    menuPauseOpen = false;
                    commandsCanvas.SetActive(false);
                    pauseScreen.GetComponent<CanvasGroup>().alpha = 1f;
                }
            }
        }

        private void OnEnable()
        {
            StartDelegates();
        }
        private void OnDisable()
        {
            UnlinkDelegates();
        }
        private void Start()
        {
            StartDelegates();
        }
        private void OpenNpcMenu()
        {
            NpcMenu.gameObject.SetActive(true);
        }
        private void TurnDeathScreenOffOn(bool b)
        {
            deathScreen.SetActive(b);
        }
        private void OnDestroy()
        {
            UnlinkDelegates();
        }
        private void UpdateLifeTxt(int actualLife, int maxLife)
        {
            LifeText.text = $"{actualLife}/{maxLife}";
        }
        void UnlinkDelegates()
        {
            HudDelegates.DeathScreen -= TurnDeathScreenOffOn;
            
            HudDelegates.UpdateLife -= UpdateSliderMaxValue;
            HudDelegates.UpdateGold -= UpdateGold;
        }
        void StartDelegates()
        {
            HudDelegates.UpdateLife += UpdateSliderMaxValue;
            HudDelegates.DeathScreen += TurnDeathScreenOffOn;
            HudDelegates.UpdateGold += UpdateGold;
            CombatEvents.onResetLifeBar.AddListener(ResetLifeBar);
        }

        void UpdateSliderMaxValue(int value)
        {
             LifeBar.maxValue = value;
            UpdateLifeTxt(PlayerData.PlayerAttributes.Hp, value);
        }
        private void ResetLifeBar(int life)
        {
            LifeBar.value = life;
            UpdateLifeTxt(life, PlayerData.PlayerAttributes.MaxHp);
        }
        private void ChangeLifeBar(int damage)
        {
            Debug.Log("Changing Lifebar");
            LifeBar.value -= damage;
            UpdateLifeTxt((int)LifeBar.value, PlayerData.PlayerAttributes.MaxHp);
        }
        private void UpdateGold(int value)
        {
            int gold = int.Parse(GoldText.text);
            gold = value;
            GoldText.text = gold.ToString();
            CoinAnimator.SetTrigger("Coin_Anim");
        }
    }
}