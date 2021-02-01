using GameDataEditor;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UnityEngine;

namespace Booty_Calls_Cheat
{
    class Main : MonoBehaviour
    {
        bool ShowMenu = true;
        bool CreatedConsole = false;
        public GameManager manager = null;
        public Player player = null;

        [System.Runtime.InteropServices.DllImport("kernel32.dll", SetLastError = true)]
        [return: System.Runtime.InteropServices.MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();

        public void Start()
        {
        }
        public void Update()
        {
            Console.WriteLine("Updating!");
            if (!CreatedConsole)
            {
                AllocConsole();
                Console.WriteLine("Console Created!");
            }

            if (manager == null || player == null)
            {
                Console.WriteLine("Getting Player!");
                manager = UnityEngine.Object.FindObjectOfType<GameManager>();
                player = manager.ActivePlayer;
                Console.Write("Player: ");
                //Console.WriteLine(player);
            }
        }
        public void OnGUI()
        {
            UIHelper.Begin("Aspect Booty Calls by bditt#0001", 4, 0, 250, ShowMenu ? 450 : 100, 4, 20, 2);

            if (UIHelper.Button("Show menu: ", ShowMenu))
                ShowMenu = !ShowMenu;

            if (ShowMenu)
            {
                UIHelper.Label("[Game Hacks]");

                if (UIHelper.Button("Unlock All Girls"))
                {
                    foreach (GDELocationData locationData in GameDataLists.Content.locationData)
                    {
                        foreach (GDEGirlData girlData in locationData.Girls)
                        {
                            if (player.Girls.ContainsKey(girlData.Key))
                            {
                                continue;
                            }
                            Girl girl = new Girl();
                            girl.Id = girlData.Key;
                            girl.CurrentLocationId = locationData.Key;
                            girl.Owner = player;
                            girl.UpdateState(GDEItemKeys.GirlState_AVAILABLE);
                            List<string> cachedCustomizationkeys = (!player.CachedGarments.ContainsKey(girlData.Key)) ? new List<string>() : player.CachedGarments[girlData.Key];
                            List<string> cachedCustomizationkeys2 = (!player.CachedSexPoses.ContainsKey(girlData.Key)) ? new List<string>() : player.CachedSexPoses[girlData.Key];
                            List<string> cachedCustomizationkeys3 = (!player.CachedSexBackgrounds.ContainsKey(girlData.Key)) ? new List<string>() : player.CachedSexBackgrounds[girlData.Key];
                            girl.garmentController.Initialize(cachedCustomizationkeys);
                            girl.sexPoseController.Initialize(cachedCustomizationkeys2);
                            girl.sexBackgroundController.Initialize(cachedCustomizationkeys3);
                            player.Girls.Add(girl.Id, girl);
                            //NotificationCenter.Post(NotificationType.GirlUnlocked, girl);
                            //NotificationCenter.Post(NotificationType.TriggeredMetricEntry, new UnlockedGirlMetricEntry(girl.Id));
                        }
                    }
                }

                if (UIHelper.Button("Unlock All Messages"))
                {
                    foreach (GDEMessageData gdemessageData in GameDataLists.Content.messageData)
                    {
                        if (!player.Phone.HasUnlockedMessage(gdemessageData))
                        {
                            Message message = new Message();
                            message.Id = gdemessageData.Key;
                            message.HasPhotoBeenViewed = false;
                            player.Phone.Messages.Add(message);
                            //NotificationCenter.Post(NotificationType.MessageUnlocked, message);
                        }
                    }
                }

                if (UIHelper.Button("Set Phone Size To 1M"))
                {
                    player.Phone.AdditionalCapacity = 1000000;
                }

                if (UIHelper.Button("Unlock All Locations"))
                {
                    foreach (GDELocationData gdelocationData in GameDataLists.Content.locationData)
                    {
                        Location location = new Location();
                        location.Id = gdelocationData.Key;
                        location.NextReward = -1L;
                        location.Owner = player;
                        player.Locations.Add(location.Id, location);
                        NotificationCenter.Post(NotificationType.LocationUnlocked, location);
                        NotificationCenter.Post(NotificationType.TriggeredMetricEntry, new UnlockedLocationMetricEntry(gdelocationData.Key, player.Date.Day, player.Date.DayPartIdx));
                    }
                }

                if (UIHelper.Button("Add 1k Money"))
                {
                    int CurrentMoney;
                    int CurrentGems;
                    player.Money.TryGetValue("CURRENCY_SOFT", out CurrentMoney);
                    player.Money.TryGetValue("CURRENCY_HARD", out CurrentGems);
                    var new_money = new Dictionary<string, int>();
                    new_money.Add("CURRENCY_SOFT", CurrentMoney + 1000);
                    new_money.Add("CURRENCY_HARD", CurrentGems);
                    player.SetMoney(new_money);
                }

                if (UIHelper.Button("Add 10k Money"))
                {
                    int CurrentMoney;
                    int CurrentGems;
                    player.Money.TryGetValue("CURRENCY_SOFT", out CurrentMoney);
                    player.Money.TryGetValue("CURRENCY_HARD", out CurrentGems);
                    var new_money = new Dictionary<string, int>();
                    new_money.Add("CURRENCY_SOFT", CurrentMoney + 10000);
                    new_money.Add("CURRENCY_HARD", CurrentGems);
                    player.SetMoney(new_money);
                }

                if (UIHelper.Button("Add 100k Money"))
                {
                    int CurrentMoney;
                    int CurrentGems;
                    player.Money.TryGetValue("CURRENCY_SOFT", out CurrentMoney);
                    player.Money.TryGetValue("CURRENCY_HARD", out CurrentGems);
                    var new_money = new Dictionary<string, int>();
                    new_money.Add("CURRENCY_SOFT", CurrentMoney + 100000);
                    new_money.Add("CURRENCY_HARD", CurrentGems);
                    player.SetMoney(new_money);
                }

                //if (UIHelper.Slider())

                if (UIHelper.Button("Add 1M Money"))
                {
                    int CurrentMoney;
                    int CurrentGems;
                    player.Money.TryGetValue("CURRENCY_SOFT", out CurrentMoney);
                    player.Money.TryGetValue("CURRENCY_HARD", out CurrentGems);
                    var new_money = new Dictionary<string, int>();
                    new_money.Add("CURRENCY_SOFT", CurrentMoney + 1000000);
                    new_money.Add("CURRENCY_HARD", CurrentGems);
                    player.SetMoney(new_money);
                }


                if (UIHelper.Button("Add 1k Gems"))
                {
                    int CurrentMoney;
                    int CurrentGems;
                    player.Money.TryGetValue("CURRENCY_SOFT", out CurrentMoney);
                    player.Money.TryGetValue("CURRENCY_HARD", out CurrentGems);
                    var new_money = new Dictionary<string, int>();
                    new_money.Add("CURRENCY_SOFT", CurrentMoney);
                    new_money.Add("CURRENCY_HARD", CurrentGems + 1000);
                    player.SetMoney(new_money);
                }

                if (UIHelper.Button("Add 10k Gems"))
                {
                    int CurrentMoney;
                    int CurrentGems;
                    player.Money.TryGetValue("CURRENCY_SOFT", out CurrentMoney);
                    player.Money.TryGetValue("CURRENCY_HARD", out CurrentGems);
                    var new_money = new Dictionary<string, int>();
                    new_money.Add("CURRENCY_SOFT", CurrentMoney);
                    new_money.Add("CURRENCY_HARD", CurrentGems + 10000);
                    player.SetMoney(new_money);
                }

                if (UIHelper.Button("Add 100k Gems"))
                {
                    int CurrentMoney;
                    int CurrentGems;
                    player.Money.TryGetValue("CURRENCY_SOFT", out CurrentMoney);
                    player.Money.TryGetValue("CURRENCY_HARD", out CurrentGems);
                    var new_money = new Dictionary<string, int>();
                    new_money.Add("CURRENCY_SOFT", CurrentMoney);
                    new_money.Add("CURRENCY_HARD", CurrentGems + 100000);
                    player.SetMoney(new_money);
                }

                if (UIHelper.Button("Add 1M Gems"))
                {
                    int CurrentMoney;
                    int CurrentGems;
                    player.Money.TryGetValue("CURRENCY_SOFT", out CurrentMoney);
                    player.Money.TryGetValue("CURRENCY_HARD", out CurrentGems);
                    var new_money = new Dictionary<string, int>();
                    new_money.Add("CURRENCY_SOFT", CurrentMoney);
                    new_money.Add("CURRENCY_HARD", CurrentGems + 1000000);
                    player.SetMoney(new_money);
                }

                if (UIHelper.Button("Add 1k to inventory."))
                {
                    foreach (KeyValuePair<string, int> entry in player.Inventory)
                    {
                        player.Inventory[entry.Key] = entry.Value + 1000;
                    }
                }

                if (UIHelper.Button("Set Energy to 1B"))
                {
                    player.Data.MaxEnergy = 1000000000;
                    player.Energy = 1000000000;
                }
            }
        }
    }
}
