using static StupidTemplate.Menu.Main;
using static StupidTemplate.Settings;

namespace AetherTemp.Menu
{
    internal class SettingsMods
    {
        public static void EnterSettings()
        {
            buttonsType = 0;
            pageNumber = 0;
        }

        public static void MainSettings()
        {
            buttonsType = 1;
            pageNumber = 0;
        }

        public static void Important()
        {
            buttonsType = 2;
            pageNumber = 0;
        }

        public static void Safety()
        {
            buttonsType = 3;
            pageNumber = 0;
        }

        public static void Room()
        {
            buttonsType = 4;
            pageNumber = 0;
        }

        public static void Advantages()
        {
            buttonsType = 5;
            pageNumber = 0;
        }

        public static void Movement()
        {
            buttonsType = 6;
            pageNumber = 0;
        }

        public static void Visuals()
        {
            buttonsType = 7;
            pageNumber = 0;
        }

        public static void Fun()
        {
            buttonsType = 8;
            pageNumber = 0;
        }

        public static void Player()
        {
            buttonsType = 9;
            pageNumber = 1;
        }

        public static void Overpowered()
        {
            buttonsType = 10;
            pageNumber = 1;
        }

        public static void Spam()
        {
            buttonsType = 11;
            pageNumber = 1;
        }

        public static void Experimental()
        {
            buttonsType = 12;
            pageNumber = 1;
        }

        public static void Misc()
        {
            buttonsType = 13;
            pageNumber = 1;
        }


        public static void GunLib()
        {
            buttonsType = 14;
            pageNumber = 0;
        }

        public static void MenuSettings()
        {
            buttonsType = 15;
            pageNumber = 0;
        }

        public static void Notification()
        {
            buttonsType = 16;
            pageNumber = 0;
        }

        public static void ProjectileSettings()
        {
            buttonsType = 17;
            pageNumber = 0;
        }

        public static void Projectiles()
        {
            buttonsType = 18;
            pageNumber = 0;
        }

        public static void Particles()
        {
            buttonsType = 16;
            pageNumber = 0;
        }



        public static void RightHand()
        {
            rightHanded = true;
        }

        public static void LeftHand()
        {
            rightHanded = false;
        }

        public static void EnableFPSCounter()
        {
            fpsCounter = true;
        }

        public static void DisableFPSCounter()
        {
            fpsCounter = false;
        }

        public static void EnableNotifications()
        {
            disableNotifications = false;
        }

        public static void DisableNotifications()
        {
            disableNotifications = true;
        }

        public static void EnableDisconnectButton()
        {
            disconnectButton = true;
        }

        public static void DisableDisconnectButton()
        {
            disconnectButton = false;
        }
    }
}
