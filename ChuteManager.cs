using Network;
using Oxide.Core;
using System.Collections.Generic;
using UnityEngine;

namespace Oxide.Plugins
{
    [Info("ChuteManager", "RFC1920", "1.0.3")]
    [Description("Manage parachute speed and backpack pickup, etc.")]
    internal class ChuteManager : RustPlugin
    {
        private ConfigData configData;
        private const string zipper = "assets/prefabs/deployable/locker/sound/equip_zipper.prefab";
        private const string permFastFlight = "chutemanager.fast";
        private const string permFastPickup = "chutemanager.pickup";

        private void OnServerInitialized()
        {
            LoadConfigValues();
            permission.RegisterPermission(permFastPickup, this);
            permission.RegisterPermission(permFastFlight, this);
        }

        private void OnPlayerInput(BasePlayer player, InputState input)
        {
            if (player == null) return;
            if (input == null) return;

            // Allow fast pickup of an unpacked chute
            if (input.IsDown(BUTTON.USE))
            {
                if (configData.Settings.RequirePermissionForFastPickup && !permission.UserHasPermission(player.UserIDString, permFastPickup)) { return; }
                List<ParachuteUnpacked> chutes = new List<ParachuteUnpacked>();
                Vis.Entities(player.transform.position, 5f, chutes);
                foreach (ParachuteUnpacked chuteuteunpacked in chutes)
                {
                    if (chuteuteunpacked.OwnerID == 0)
                    {
                        Puts(chuteuteunpacked.net.ID.ToString());
                        Item ch = ItemManager.CreateByItemID(602628465);
                        ch.condition = chuteuteunpacked.health;
                        ch.MoveToContainer(player.inventory.containerMain);
                        chuteuteunpacked.Kill();
                        if (configData.Settings.PlaySoundOnPickup)
                        {
                            SendEffectTo(zipper, player);
                        }
                        return;
                    }
                }
                return;
            }

            // Make a deployed chute move faster with SPRINT key
            if (configData.Settings.RequirePermissionForFastFlight && !permission.UserHasPermission(player.UserIDString, permFastFlight)) { return; }
            BaseMountable what = player?.GetMounted();
            if (what?.name.Equals("assets/prefabs/vehicle/seats/parachuteseat.prefab") == true)
            {
                Parachute chute = what.gameObject.GetComponentInParent<Parachute>();
                if (input.IsDown(BUTTON.BACKWARD))
                {
                    if (input.IsDown(BUTTON.SPRINT) && chute != null)
                    {
                        chute.BackInputForceMultiplier = configData.Settings.revMultiplier > 0 ? configData.Settings.revMultiplier : 2f;
                    }
                    else if (chute != null)
                    {
                        chute.BackInputForceMultiplier = 0.2f; // Default from Facepunch
                    }
                    return;
                }
                if (input.IsDown(BUTTON.FORWARD))
                {
                    if (input.IsDown(BUTTON.SPRINT) && chute != null)
                    {
                        chute.ForwardTiltAcceleration = configData.Settings.fwdMultiplier > 0 ? configData.Settings.fwdMultiplier : 40f;
                    }
                    else if (chute != null)
                    {
                        chute.ForwardTiltAcceleration = 2f; // Default from Facepunch
                    }
                }
            }
        }

        private void SendEffectTo(string effect, BasePlayer player)
        {
            if (player == null) return;
            if (effect == null) return;

            var EffectInstance = new Effect();
            EffectInstance.Init(Effect.Type.Generic, player, 0, Vector3.up, Vector3.zero);
            EffectInstance.pooledstringid = StringPool.Get(effect);
            NetWrite writer = Net.sv.StartWrite();
            writer.PacketID(Message.Type.Effect);
            EffectInstance.WriteToStream(writer);
            writer.Send(new SendInfo(player.net.connection));
            EffectInstance.Clear();
        }

        #region config
        private class ConfigData
        {
            public Settings Settings;
            public VersionNumber Version;
        }

        private class Settings
        {
            public float fwdMultiplier;
            public float revMultiplier;
            public bool AllowFastPickup;
            public bool RequirePermissionForFastPickup;
            public bool RequirePermissionForFastFlight;
            public bool PlaySoundOnPickup;
        }

        protected override void LoadDefaultConfig()
        {
            Puts("Creating new config file.");
            ConfigData config = new ConfigData
            {
                Settings = new Settings()
                {
                    fwdMultiplier = 40f,
                    revMultiplier = 2f,
                    AllowFastPickup = true,
                    RequirePermissionForFastFlight = false,
                    RequirePermissionForFastPickup = false,
                    PlaySoundOnPickup = true
                },
                Version = Version
            };
            SaveConfig(config);
        }

        private void LoadConfigValues()
        {
            configData = Config.ReadObject<ConfigData>();
            configData.Version = Version;

            SaveConfig(configData);
        }

        private void SaveConfig(ConfigData config)
        {
            Config.WriteObject(config, true);
        }
        #endregion
    }
}
