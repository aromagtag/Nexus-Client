using GorillaTag.Cosmetics.Summer;
using StupidTemplate.Classes;
using StupidTemplate.Menu;
using StupidTemplate.Mods;
using static StupidTemplate.Settings;
using UnityEngine;
using GorillaTag.Cosmetics;
using StupidTemplate;
using AetherTemp.Mods;
using Oculus.Interaction;
using Photon.Realtime;


namespace AetherTemp.Menu
{
    internal class Buttons
    {
        public static ButtonInfo[][] buttons = new ButtonInfo[][]
        {
            new ButtonInfo[] { // Main Mods
                new ButtonInfo { buttonText = "•Join The Discord•", method =() => DiscordMod.JoinDiscord(), isTogglable = false, toolTip = "Makes You Join The Discord."},
                new ButtonInfo { buttonText = "•Settings•", method =() => SettingsMods.MenuSettings(), isTogglable = false, toolTip = "Opens the main settings page for the menu."},
                new ButtonInfo { buttonText = "•Important Mods•", method =() => SettingsMods.Important(), isTogglable = false, toolTip = "Opens the important mods page for the menu."},
                new ButtonInfo { buttonText = "•Safety Mods•", method =() => SettingsMods.Safety(), isTogglable = false, toolTip = "Opens the safety mods page for the menu."},
                new ButtonInfo { buttonText = "•Room Mods•", method =() => SettingsMods.Room(), isTogglable = false, toolTip = "Opens the important mods page for the menu."},
                new ButtonInfo { buttonText = "•Advantages•", method =() => SettingsMods.Advantages(), isTogglable = false, toolTip = "Opens the advantages mods page for the menu."},
                new ButtonInfo { buttonText = "•Movement Mods•", method =() => SettingsMods.Movement(), isTogglable = false, toolTip = "Opens the movement mods page for the menu."},
                new ButtonInfo { buttonText = "•Visual Mods•", method =() => SettingsMods.Visuals(), isTogglable = false, toolTip = "Opens the visual mods page for the menu."},
                new ButtonInfo { buttonText = "•Fun Mods•", method =() => SettingsMods.Fun(), isTogglable = false, toolTip = "Opens the fun mods page for the menu."},
                new ButtonInfo { buttonText = "•Player Mods•", method =() => SettingsMods.Player(), isTogglable = false, toolTip = "Opens the player mods page for the menu."},
                new ButtonInfo { buttonText = "•Overpowered Mods•", method =() => SettingsMods.Overpowered(), isTogglable = false, toolTip = "Opens the overpowered mods for the menu."},
                new ButtonInfo { buttonText = "•Spam Mods•", method =() => SettingsMods.Spam(), isTogglable = false, toolTip = "Opens the spam mods page for the menu."},
                new ButtonInfo { buttonText = "•Experimental Mods•", method =() => SettingsMods.Experimental(), isTogglable = false, toolTip = "Opens the experimental mods page for the menu."},
                new ButtonInfo { buttonText = "•Miscellaneous Mods•", method =() => SettingsMods.Misc(), isTogglable = false, toolTip = "Opens the miscellaneous mods page for the menu."},
            },

            new ButtonInfo[] { // Main Settings
                new ButtonInfo { buttonText = "Menu Settings", method =() => SettingsMods.MenuSettings(), isTogglable = false, toolTip = "Opens the Menu settings for the menu."},
                new ButtonInfo { buttonText = "GunLib Settings", method =() => SettingsMods.GunLib(), isTogglable = false, toolTip = "Opens the GunLib settings for the menu."},
                new ButtonInfo { buttonText = "Notifications Settings", method =() => SettingsMods.Notification(), isTogglable = false, toolTip = "Opens the GunLib settings for the menu."},
                new ButtonInfo { buttonText = "Projectile Settings", method =() => SettingsMods.ProjectileSettings(), isTogglable = false, toolTip = "Opens the GunLib settings for the menu."},
            },

            new ButtonInfo[] { // Important Mods
                new ButtonInfo { buttonText = "Quit Game", method =() => Important.QuitGame(), isTogglable = false, toolTip = "Quits The Game."},
                new ButtonInfo { buttonText = "Restart Game (STEAMVR ONLY)", method =() => Important.RestartGame(), isTogglable = false, toolTip = "Restarts The Game."},
                new ButtonInfo { buttonText = "Disable TOS", method =() => Important.DisableTOS(), isTogglable = false, toolTip = "Disables The Ingame TOS."},
                new ButtonInfo { buttonText = "Disable Quitbox", method =() => Important.DisableQuitBox(), isTogglable = false, toolTip = "Disables The Quitbox."},
                new ButtonInfo { buttonText = "Enable Quitbox", method =() => Important.EnableQuitBox(), isTogglable = false, toolTip = "Enables The Quitbox."},
            },

            new ButtonInfo[] { // Safety Mods
                new ButtonInfo { buttonText = "Anti-Report", method =() => Safety.AntiReport(), isTogglable = true, toolTip = "Disconnects You When Reported."},
                new ButtonInfo { buttonText = "Flush RPCs", method =() => Main.RPCProtection(), isTogglable = false, toolTip = "Flushes all RPC calls, good after you stop spamming." },
            },

             new ButtonInfo[] { // Room Mods
                new ButtonInfo { buttonText = "Disconnect", method =() => Important.Disconnect(), isTogglable = false, toolTip = "Disconnects You From The Lobby."},
                new ButtonInfo { buttonText = "Disconnect On Grip (RG)", method =() => Mods.Room.DisconnectOnGrip(), isTogglable = true, toolTip = "Disconnects You When You Put Pressure On Your Right Controller's Grip."},
                new ButtonInfo { buttonText = "Reconnect", method =() => Mods.Room.Reconnect(), isTogglable = false, toolTip = "Reconnects You To A Lobby."},
                new ButtonInfo { buttonText = "Join Last Room", method =() => Mods.Room.JoinLastRoom(), isTogglable = false, toolTip = "Reconnects You To Your Previous Lobby."},
                new ButtonInfo { buttonText = "Join Random Public", method =() => Mods.Room.JoinRandomPublic(), isTogglable = false, toolTip = "Connects You To A Random Pub."},
                new ButtonInfo { buttonText = "Create Room", method =() => Mods.Room.CreateRoom(), isTogglable = false, toolTip = "Creates A Room For Ya."},
                new ButtonInfo { buttonText = "Create Public Room", method =() => Mods.Room.CreatePublic(), isTogglable = false, toolTip = "Creats A Public Room For Ya."},
                new ButtonInfo { buttonText = "Disable Network Triggers", method =() => Mods.Room.DisableNetworkTriggers(), isTogglable = true, toolTip = "Disables Network Triggers."},
                new ButtonInfo { buttonText = "Enable Network Triggers", method =() => Mods.Room.EnableNetworkTriggers(), isTogglable = false, toolTip = "Enables Network Triggers."},
             },

             new ButtonInfo[] { // Advantages
                new ButtonInfo { buttonText = "Untag Self", method =() => Advantages.UntagSelf(), isTogglable = true, toolTip = "Untags Self."},
                new ButtonInfo { buttonText = "No Tag On Join", method =() => Advantages.NoTagOnJoin(), isTogglable = true, toolTip = "Not Infected When You Join A Lobby."},
                new ButtonInfo { buttonText = "Anti Tag", method =() => Advantages.AntiTag(), isTogglable = true, toolTip = "Untags Self When Tagged."},
             },

            new ButtonInfo[] { // Movement Mods
                new ButtonInfo { buttonText = "Platforms (LG & RG)", method =() => Movement.PlatformMod(), isTogglable = true, toolTip = "Basic ."},
                new ButtonInfo { buttonText = "Fly (A)", method =() => Movement.Fly(), isTogglable = true, toolTip = "Soar Like The Wind."},
                new ButtonInfo { buttonText = "Noclip", method =() => Movement.Noclip(), isTogglable = true, toolTip = "Disables Colliders."},
                new ButtonInfo { buttonText = "Slow Speed Boost", method =() => Movement.SlowSpeedboost(), isTogglable = true, toolTip = "You Are Fast 𝑙𝑖𝑘𝑒 𝑛𝑖𝑛𝑗𝑎."},
                new ButtonInfo { buttonText = "Normal Speed Boost", method =() => Movement.NormalSpeedboost(), isTogglable = true, toolTip = "You Are Fast 𝑙𝑖𝑘𝑒 𝑛𝑖𝑛𝑗𝑎."},
                new ButtonInfo { buttonText = "Mosa Speed Boost", method =() => Movement.MosaSpeedboost(), isTogglable = true, toolTip = "You Are Fast 𝑙𝑖𝑘𝑒 𝑛𝑖𝑛𝑗𝑎."},
                new ButtonInfo { buttonText = "Fast Speed Boost", method =() => Movement.FastSpeedboost(), isTogglable = true, toolTip = "You Are Fast 𝑙𝑖𝑘𝑒 𝑛𝑖𝑛𝑗𝑎."},
                new ButtonInfo { buttonText = "Super Speed Boost", method =() => Movement.SuperSpeedboost(), isTogglable = true, toolTip = "You Are Fast 𝑙𝑖𝑘𝑒 𝑛𝑖𝑛𝑗𝑎."},
                new ButtonInfo { buttonText = "Mega Speed Boost", method =() => Movement.MegaSpeedboost(), isTogglable = true, toolTip = "You Are Fast 𝑙𝑖𝑘𝑒 𝑛𝑖𝑛𝑗𝑎."},
                new ButtonInfo { buttonText = "Ultra Speed Boost", method =() => Movement.UltraSpeedboost(), isTogglable = true, toolTip = "You Are Fast 𝑙𝑖𝑘𝑒 𝑛𝑖𝑛𝑗𝑎."},
                new ButtonInfo { buttonText = "Hyper Speed Boost", method =() => Movement.HyperSpeedboost(), isTogglable = true, toolTip = "You Are Fast 𝑙𝑖𝑘𝑒 𝑛𝑖𝑛𝑗𝑎."},
                new ButtonInfo { buttonText = "God Speed Boost", method =() => Movement.GodSpeedboost(), isTogglable = true, toolTip = "You Are Fast 𝑙𝑖𝑘𝑒 𝑛𝑖𝑛𝑗𝑎."},
                new ButtonInfo { buttonText = "Speed Of LIGHT Boost", method =() => Movement.SpeedOfLightSpeedboost(), isTogglable = true, toolTip = "You Are Fast 𝑙𝑖𝑘𝑒 𝑛𝑖𝑛𝑗𝑎."},
            },

            new ButtonInfo[] { // Visual Mods
                 new ButtonInfo { buttonText = "Tracers", method =() => Visuals.Tracer(), isTogglable = true, toolTip = "Enables Tracers."},
                 new ButtonInfo { buttonText = "Smoke Bomb Spammer", method =() => Visuals.SmokeBombSpammer(), isTogglable = true, toolTip = "Spams Smoke Bombs."},
                 new ButtonInfo { buttonText = "J3VU's Name Cycle", method =() => Visuals.J3VUNameCycle(), isTogglable = true, toolTip = "Starts J3VU's Name Cycle."},
                 new ButtonInfo { buttonText = "PBBV's Name Cycle", method =() => Visuals.PBBVNameCycle(), isTogglable = true, toolTip = "Starts PBBV's Name Cycle."},
            },

              new ButtonInfo[] { // Fun Mods
                 new ButtonInfo { buttonText = "placeholder", method =() => mods.placeholder(), isTogglable = true, toolTip = "placeholder."},


              },

               new ButtonInfo[] { // Player Mods
                   new ButtonInfo { buttonText = "Fix Head", method =() => Mods.Player.FixHead(), isTogglable = false, toolTip = "Fixes Head."},
                   new ButtonInfo { buttonText = "Upside-Down Head", method =() => Mods.Player.UpsideDownHead(), isTogglable = true, toolTip = "Head Faces Upside-Down Like An Omelete."},
                   new ButtonInfo { buttonText = "Backwards Head", method =() => Mods.Player.BackwardHead(), isTogglable = true, toolTip = "Head Faces Backwards."},
                   new ButtonInfo { buttonText = "Broken Neck", method =() => Mods.Player.BrokenNeck(), isTogglable = true, toolTip = "Head Faces Sideways."},
                   new ButtonInfo { buttonText = "Spin Head X", method =() => Mods.Player.SpinHeadX(), isTogglable = true, toolTip = "Spins Head In The X Axis."},
                   new ButtonInfo { buttonText = "Spin Head Y", method =() => Mods.Player.SpinHeadY(), isTogglable = true, toolTip = "Spins Head In The Y Axis."},
                   new ButtonInfo { buttonText = "Spin Head Z", method =() => Mods.Player.SpinHeadZ(), isTogglable = true, toolTip = "Spins Head In The Z Axis."},
                   new ButtonInfo { buttonText = "Head Bang", method =() => Mods.Player.HeadBang(), isTogglable = true, toolTip = "placeholder."},
                   new ButtonInfo { buttonText = "Ghost Monke (B)", method =() => Mods.Player.GhostMonke(), isTogglable = true, toolTip = "Makes You A Spooky Ghost."},
                   new ButtonInfo { buttonText = "Steam Long Arms", method =() => Mods.Player.SteamLongArms(), isTogglable = true, toolTip = "Longer Arms Than Usual You Shorty."},
               },

              new ButtonInfo[] { // Overpowered Mods
                 new ButtonInfo { buttonText = "Lag All (MIGHT BE INSTA-BAN)", method =() => Overpowered.LagAll(), isTogglable = true, toolTip = "Lags Everyone in Your Server."},
 },

 new ButtonInfo[] { // Spam Mods
      new ButtonInfo { buttonText = "•Projectile Mods•", method =() => SettingsMods.Projectiles(), isTogglable = false, toolTip = "Opens the projectile mods page for the menu."},
 },

 new ButtonInfo[] { // Experimental Mods
     new ButtonInfo { buttonText = "placeholder", method =() => mods.placeholder(), isTogglable = true, toolTip = "placeholder."},
 },

 new ButtonInfo[] { // Miscellaneous Mods
     new ButtonInfo { buttonText = "placeholder", method =() => mods.placeholder(), isTogglable = true, toolTip = "placeholder."},
 },



            new ButtonInfo[] { // GunL
                new ButtonInfo { buttonText = "Equip Gun", method =() => mods.GunTemplate(), isTogglable = true, toolTip = "Equips a gun."},
                new ButtonInfo { buttonText = $"Smoothness: {(mods.num == 5f ? "Very Fast" : mods.num == 10f ? "Normal" : "Super Smooth")}", method = () => { mods.GunSmoothNess(); foreach (var category in Buttons.buttons) foreach (var button in category) if (button.buttonText.StartsWith("Smoothness")) button.buttonText = $"Smoothness: {(mods.num == 5f ? "Super Smooth" : mods.num == 10f ? "Normal" : "No Smooth")}"; }, isTogglable = false, toolTip = "Changes gun smoothness." },
                new ButtonInfo { buttonText = $"Gun Color: {mods.currentGunColor.name}", method = () => { mods.CycleGunColor(); Buttons.buttons.ForEach(category => category.ForEach(button => { if (button.buttonText.StartsWith("Gun Color")) button.buttonText = $"Gun Color: {mods.currentGunColor.name}"; })); }, isTogglable = false, toolTip = "Cycles through gun colors." },
                new ButtonInfo { buttonText = $"Toggle Sphere: {(mods.isSphereEnabled ? "Enabled" : "Disabled")}", method = () => { mods.isSphereEnabled = !mods.isSphereEnabled; if (mods.GunSphere != null) mods.GunSphere.transform.localScale = mods.isSphereEnabled ? new Vector3(0.1f, 0.1f, 0.1f) : new Vector3(0f, 0f, 0f); foreach (var category in Buttons.buttons) foreach (var button in category) if (button.buttonText.StartsWith("Toggle Sphere Size")) button.buttonText = $"Toggle Sphere Size: {(mods.isSphereEnabled ? "Enabled" : "Disabled")}"; }, isTogglable = false, toolTip = "Toggles the size of the gun sphere." }
            },

            new ButtonInfo[] { // MenuSettings
                new ButtonInfo { buttonText = "Right/Left Hand", enableMethod =() => SettingsMods.RightHand(), disableMethod =() => SettingsMods.LeftHand(), toolTip = "Puts the menu on your right hand."},
                new ButtonInfo { buttonText = "FPS Counter", enableMethod =() => SettingsMods.EnableFPSCounter(), disableMethod =() => SettingsMods.DisableFPSCounter(), enabled = fpsCounter, toolTip = "Toggles the FPS counter."},
                new ButtonInfo { buttonText = "Disconnect Button", enableMethod =() => SettingsMods.EnableDisconnectButton(), disableMethod =() => SettingsMods.DisableDisconnectButton(), enabled = disconnectButton, toolTip = "Toggles the disconnect button."},
                new ButtonInfo { buttonText = $"Delete Time: {(Main.num == 2f ? "Default" : Main.num == 5f ? "Long" : "Fast")}", method = () => { Main.MenuDeleteTime(); foreach (var category in Buttons.buttons) foreach (var button in category) if (button.buttonText.StartsWith("Delete Time")) button.buttonText = $"Delete Time: {(Main.num == 2f ? "Default" : Main.num == 5f ? "Long" : "Fast")}"; }, isTogglable = false, toolTip = "Changes menu delete time." },
    },

            new ButtonInfo[] { // Notifications
                new ButtonInfo { buttonText = "Notifications", enableMethod =() => SettingsMods.EnableNotifications(), disableMethod =() => SettingsMods.DisableNotifications(), enabled = !disableNotifications, toolTip = "Toggles the notifications."},
            },

            new ButtonInfo[] { // ProjectileSettings
                new ButtonInfo { buttonText = "placeholder", method =() => mods.placeholder(), isTogglable = true, toolTip = "placeholder."},


            },


            new ButtonInfo[] { // projectiles
                new ButtonInfo { buttonText = "•Projectile Settings•", method =() => SettingsMods.ProjectileSettings(), isTogglable = false, toolTip = "Opens the projectile mods page for the menu."},
                new ButtonInfo { buttonText = "Explosion", method =() => Particles.CreateDomain(), isTogglable = true, toolTip = "makes a Explosion."},
                new ButtonInfo { buttonText = "Sukuna's domain", method =() => Particles.CreateDomain2(), isTogglable = true, toolTip = "Sukuna's domain."},
                new ButtonInfo { buttonText = "Fire Fists", method =() => Particles.CreateFireEffect(), isTogglable = true, toolTip = "gives you Fire Fists."},
                new ButtonInfo { buttonText = "Black Hole", method =() => Particles.CreateBlackHole(), isTogglable = true, toolTip = "makes a Black Hole."},
                new ButtonInfo { buttonText = "White Hole", method =() => Particles.CreateWhiteHole(), isTogglable = true, toolTip = "makes a White Hole."},
                new ButtonInfo { buttonText = "Lighting", method =() => Particles.CreateLightningEffect(), isTogglable = true, toolTip = "Lighting."},
                new ButtonInfo { buttonText = "Magic Spell", method =() => Particles.CastMagicSpell(), isTogglable = true, toolTip = "cast a Magic Spell."},
                new ButtonInfo { buttonText = "Spark Magic Spell", method =() => Particles.CastSparkMagic(), isTogglable = true, toolTip = "cast a Spark Magic Spell."},
                new ButtonInfo { buttonText = "Light Magic Spell", method =() => Particles.CastLightMagic(), isTogglable = true, toolTip = "cast a Light Magic Spell."},
                new ButtonInfo { buttonText = "FireBall Magic Spell", method =() => Particles.CastFireballMagic(), isTogglable = true, toolTip = "cast a FireBall."},
                new ButtonInfo { buttonText = "Sword Slash", method =() => Particles.SwordSlash(), isTogglable = true, toolTip = "make a Sword Slash."},
                new ButtonInfo { buttonText = "Lighting Bolt Magic", method =() => Particles.CastLightningBolt(), isTogglable = true, toolTip = "Lighting Bolt Magic."},
                new ButtonInfo { buttonText = "Rift Magic", method =() => Particles.CastVoidRift(), isTogglable = true, toolTip = "Rift Magic."},
                new ButtonInfo { buttonText = "Frost Orb Magic", method =() => Particles.CastFrostOrb(), isTogglable = true, toolTip = "Frost Orb Magic."},
                new ButtonInfo { buttonText = "Nebula Storm", method =() => Particles.CreateNebulaStorm(), isTogglable = true, toolTip = "Nebula Storm."},
                new ButtonInfo { buttonText = "Draw", method =() => Particles.Draw(), isTogglable = true, toolTip = "Draw."},
            },



            //always keep this at the bottom if you add another tab (by going to categories) make sure you put that section above this one:

             new ButtonInfo[] {
                 new ButtonInfo { buttonText = "Disconnect", method =() => mods.Disconnect(), isTogglable = false, toolTip = "Disconnectes You From The Lobby"},
             },
            new ButtonInfo[] {
                new ButtonInfo { buttonText = "Return To Main", method =() => Global.ReturnHome(), isTogglable = false, toolTip = "Opens The Home Page For The Menu."},
            },

        };
    }
}
