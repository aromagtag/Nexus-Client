using AetherTemp.Menu;
using BepInEx;
using ExitGames.Client.Photon;
using HarmonyLib;
using Photon.Pun;
using Photon.Realtime;
using StupidTemplate.Classes;
using StupidTemplate.Notifications;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using static AetherTemp.Menu.Buttons;
using static StupidTemplate.Settings;

namespace StupidTemplate.Menu
{
    [HarmonyPatch(typeof(GorillaLocomotion.GTPlayer))]
    [HarmonyPatch("LateUpdate", MethodType.Normal)]
    public class Main : MonoBehaviour
    {
        // Constant
        public static float num = 2f;

        public static void MenuDeleteTime()
        {
            if (num == 2f)
                num = 5f; // Long
            else if (num == 5f)
                num = 0.01f; // Fast
            else
                num = 2f; // Default
        }


        public static void Prefix()
        {
            // Initialize Menu
                try
                {
                    bool toOpen = (!rightHanded && ControllerInputPoller.instance.leftControllerSecondaryButton) || (rightHanded && ControllerInputPoller.instance.rightControllerSecondaryButton);
                    bool keyboardOpen = UnityInput.Current.GetKey(keyboardButton);

                    if (menu == null)
                    {
                    if (toOpen || keyboardOpen)
                    {
                        CreateMenu();
                        if (reference == null)
                        {
                            CreateReference(rightHanded);
                        }
                    }
                }
                    else
                    {
                        if ((toOpen || keyboardOpen))
                        {
                            RecenterMenu(rightHanded, keyboardOpen);
                        }
                        else
                        {
                        GameObject.Find("Shoulder Camera").transform.Find("CM vcam1").gameObject.SetActive(true);

                        Rigidbody comp = menu.AddComponent(typeof(Rigidbody)) as Rigidbody;

                        // Disable gravity
                        comp.useGravity = true;  // Set gravity to zero

                        if (rightHanded)
                        {
                            comp.velocity = GorillaLocomotion.GTPlayer.Instance.rightHandCenterVelocityTracker.GetAverageVelocity(true, 0);
                        }
                        else
                        {
                            comp.velocity = GorillaLocomotion.GTPlayer.Instance.leftHandCenterVelocityTracker.GetAverageVelocity(true, 0);
                        }

                        UnityEngine.Object.Destroy(menu, num);
                        menu = null;

                        UnityEngine.Object.Destroy(reference);
                        reference = null;

                    }
                }
                }
                catch (Exception exc)
                {
                    UnityEngine.Debug.LogError(string.Format("{0} // Error initializing at {1}: {2}", PluginInfo.Name, exc.StackTrace, exc.Message));
                }

            // Constant
                try
                {
                    // Pre-Execution
                        if (fpsObject != null)
                        {
                            fpsObject.text = "FPS: " + Mathf.Ceil(1f / Time.unscaledDeltaTime).ToString();
                        }

                    // Execute Enabled mods
                        foreach (ButtonInfo[] buttonlist in buttons)
                        {
                            foreach (ButtonInfo v in buttonlist)
                            {
                                if (v.enabled)
                                {
                                    if (v.method != null)
                                    {
                                        try
                                        {
                                            v.method.Invoke();
                                        }
                                        catch (Exception exc)
                                        {
                                            UnityEngine.Debug.LogError(string.Format("{0} // Error with mod {1} at {2}: {3}", PluginInfo.Name, v.buttonText, exc.StackTrace, exc.Message));
                                        }
                                    }
                                }
                            }
                        }
                } catch (Exception exc)
                {
                    UnityEngine.Debug.LogError(string.Format("{0} // Error with executing mods at {1}: {2}", PluginInfo.Name, exc.StackTrace, exc.Message));
                }
        }

        // Functions

       

        // Functions
        Color peach = new Color(255f / 255f, 229f / 255f, 180f / 255f);
        Color mainc = new Color(176f / 255f, 153f / 255f, 128f / 255f);
        public static void CreateMenu()
        {
            menu = GameObject.CreatePrimitive(PrimitiveType.Cube);
            UnityEngine.Object.Destroy(menu.GetComponent<Rigidbody>());
            UnityEngine.Object.Destroy(menu.GetComponent<BoxCollider>());
            UnityEngine.Object.Destroy(menu.GetComponent<Renderer>());
            menu.transform.localScale = new Vector3(0.1f, 0.3f, 0.3825f);

            GameObject menuBackground = GameObject.CreatePrimitive(PrimitiveType.Cube);
            UnityEngine.Object.Destroy(menuBackground.GetComponent<Rigidbody>());
            UnityEngine.Object.Destroy(menuBackground.GetComponent<BoxCollider>());
            menuBackground.transform.parent = menu.transform;
            menuBackground.transform.rotation = Quaternion.identity;
            menuBackground.transform.localScale = new Vector3(0.1f, 1f, 1f); 
            menuBackground.transform.position = new Vector3(0.05f, 0f, 0f);

            menuBackground.GetComponent<Renderer>().material.color = new Color(0.745f, 0.541f, 0.733f);

            menuBackground.GetComponent<Renderer>().enabled = false;

            float bevel = 0.02f;

            Renderer ToRoundRenderer = menuBackground.GetComponent<Renderer>();


            GameObject BaseA = GameObject.CreatePrimitive(PrimitiveType.Cube);
            BaseA.GetComponent<Renderer>().enabled = true;
            UnityEngine.Object.Destroy(BaseA.GetComponent<Collider>());
            BaseA.transform.parent = menu.transform;
            BaseA.transform.rotation = Quaternion.identity;
            BaseA.transform.localPosition = menuBackground.transform.localPosition;
            BaseA.transform.localScale = menuBackground.transform.localScale + new Vector3(0f, bevel * -2.55f, 0f);

            GameObject BaseB = GameObject.CreatePrimitive(PrimitiveType.Cube);
            BaseB.GetComponent<Renderer>().enabled = true;
            UnityEngine.Object.Destroy(BaseB.GetComponent<Collider>());
            BaseB.transform.parent = menu.transform;
            BaseB.transform.rotation = Quaternion.identity;
            BaseB.transform.localPosition = menuBackground.transform.localPosition;
            BaseB.transform.localScale = menuBackground.transform.localScale + new Vector3(0f, 0f, -bevel * 2f);

            GameObject RoundCornerA = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            RoundCornerA.GetComponent<Renderer>().enabled = true;
            UnityEngine.Object.Destroy(RoundCornerA.GetComponent<Collider>());
            RoundCornerA.transform.parent = menu.transform;
            RoundCornerA.transform.rotation = Quaternion.Euler(0f, 0f, 90f);
            RoundCornerA.transform.localPosition = menuBackground.transform.localPosition + new Vector3(0f, (menuBackground.transform.localScale.y / 2f) - (bevel * 1.275f), (menuBackground.transform.localScale.z / 2f) - bevel);
            RoundCornerA.transform.localScale = new Vector3(bevel * 2.55f, menuBackground.transform.localScale.x / 2f, bevel * 2f);

            GameObject RoundCornerB = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            RoundCornerB.GetComponent<Renderer>().enabled = true;
            UnityEngine.Object.Destroy(RoundCornerB.GetComponent<Collider>());
            RoundCornerB.transform.parent = menu.transform;
            RoundCornerB.transform.rotation = Quaternion.Euler(0f, 0f, 90f);        
            RoundCornerB.transform.localPosition = menuBackground.transform.localPosition + new Vector3(0f, -(menuBackground.transform.localScale.y / 2f) + (bevel * 1.275f), (menuBackground.transform.localScale.z / 2f) - bevel);
            RoundCornerB.transform.localScale = new Vector3(bevel * 2.55f, menuBackground.transform.localScale.x / 2f, bevel * 2f);

            GameObject RoundCornerC = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            RoundCornerC.GetComponent<Renderer>().enabled = true;
            UnityEngine.Object.Destroy(RoundCornerC.GetComponent<Collider>());
            RoundCornerC.transform.parent = menu.transform;
            RoundCornerC.transform.rotation = Quaternion.Euler(0f, 0f, 90f); 
            RoundCornerC.transform.localPosition = menuBackground.transform.localPosition + new Vector3(0f, (menuBackground.transform.localScale.y / 2f) - (bevel * 1.275f), -(menuBackground.transform.localScale.z / 2f) + bevel);
            RoundCornerC.transform.localScale = new Vector3(bevel * 2.55f, menuBackground.transform.localScale.x / 2f, bevel * 2f);

            GameObject RoundCornerD = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            RoundCornerD.GetComponent<Renderer>().enabled = true;
            UnityEngine.Object.Destroy(RoundCornerD.GetComponent<Collider>());
            RoundCornerD.transform.parent = menu.transform;
            RoundCornerD.transform.rotation = Quaternion.Euler(0f, 0f, 90f);
            RoundCornerD.transform.localPosition = menuBackground.transform.localPosition + new Vector3(0f, -(menuBackground.transform.localScale.y / 2f) + (bevel * 1.275f), -(menuBackground.transform.localScale.z / 2f) + bevel);
            RoundCornerD.transform.localScale = new Vector3(bevel * 2.55f, menuBackground.transform.localScale.x / 2f, bevel * 2f);

            GameObject[] ToChange = new GameObject[]
            {
    BaseA,
    BaseB,
    RoundCornerA,
    RoundCornerB,
    RoundCornerC,
    RoundCornerD
            };

            foreach (GameObject obj in ToChange)
            {
                obj.GetComponent<Renderer>().material.color = new Color(0.745f, 0.541f, 0.733f); 
            }


            canvasObject = new GameObject();
            canvasObject.transform.parent = menu.transform;
            Canvas canvas = canvasObject.AddComponent<Canvas>();
            CanvasScaler canvasScaler = canvasObject.AddComponent<CanvasScaler>();
            canvasObject.AddComponent<GraphicRaycaster>();
            canvas.renderMode = RenderMode.WorldSpace;
            canvasScaler.dynamicPixelsPerUnit = 1000f;

            Text text = new GameObject
            {
                transform =
        {
            parent = canvasObject.transform
        }
            }.AddComponent<Text>();
            text.font = currentFont;
            text.text = PluginInfo.Name;
            text.fontSize = 1;
            text.color = textColors[0];
            text.supportRichText = true;
            text.fontStyle = FontStyle.Italic;
            text.alignment = TextAnchor.MiddleCenter;
            text.resizeTextForBestFit = true;
            text.resizeTextMinSize = 0;
            RectTransform component = text.GetComponent<RectTransform>();
            component.localPosition = Vector3.zero;
            component.sizeDelta = new Vector2(0.28f, 0.05f);
            component.position = new Vector3(0.06f, 0f, 0.165f);
            component.rotation = Quaternion.Euler(new Vector3(180f, 90f, 90f));

            if (fpsCounter)
            {
                fpsObject = new GameObject
                {
                    transform =
            {
                parent = canvasObject.transform
            }
                }.AddComponent<Text>();
                fpsObject.font = currentFont;
                fpsObject.text = "FPS: " + Mathf.Ceil(1f / Time.unscaledDeltaTime).ToString();
                fpsObject.color = textColors[0];
                fpsObject.fontSize = 1;
                fpsObject.supportRichText = true;
                fpsObject.fontStyle = FontStyle.Italic;
                fpsObject.alignment = TextAnchor.MiddleCenter;
                fpsObject.horizontalOverflow = UnityEngine.HorizontalWrapMode.Overflow;
                fpsObject.resizeTextForBestFit = true;
                fpsObject.resizeTextMinSize = 0;
                RectTransform component2 = fpsObject.GetComponent<RectTransform>();
                component2.localPosition = Vector3.zero;
                component2.sizeDelta = new Vector2(0.28f, 0.02f);
                component2.position = new Vector3(0.06f, 0f, 0.135f);
                component2.rotation = Quaternion.Euler(new Vector3(180f, 90f, 90f));
            }

                GameObject gameObject12 = GameObject.CreatePrimitive(PrimitiveType.Cube);
                if (!UnityInput.Current.GetKey(KeyCode.Q))
                {
                    gameObject12.layer = 2;
                }
                UnityEngine.Object.Destroy(gameObject12.GetComponent<Rigidbody>());
                gameObject12.GetComponent<BoxCollider>().isTrigger = true;
                gameObject12.transform.parent = menu.transform;
                gameObject12.transform.rotation = Quaternion.identity;
                gameObject12.transform.localScale = new Vector3(0.1f, 0.35f, 0.1f);
                gameObject12.transform.localPosition = new Vector3(0.56f, 0.302f, 0.6f);
                gameObject12.GetComponent<Renderer>().material.color = new Color(0.863f, 0.655f, 0.867f); 
                gameObject12.AddComponent<Classes.Button>().relatedText = "home";

                gameObject12.GetComponent<Renderer>().enabled = false;


      
                Renderer ToRoundRenderer12 = gameObject12.GetComponent<Renderer>();

                GameObject BaseA12 = GameObject.CreatePrimitive(PrimitiveType.Cube);
                BaseA12.GetComponent<Renderer>().enabled = true;
                UnityEngine.Object.Destroy(BaseA12.GetComponent<Collider>());
                BaseA12.transform.parent = menu.transform;
                BaseA12.transform.rotation = Quaternion.identity;
                BaseA12.transform.localPosition = gameObject12.transform.localPosition;
                BaseA12.transform.localScale = gameObject12.transform.localScale + new Vector3(0f, bevel * -2.55f, 0f);
                BaseA12.AddComponent<Classes.Button>().relatedText = "home";


                GameObject BaseB12 = GameObject.CreatePrimitive(PrimitiveType.Cube);
                BaseB12.GetComponent<Renderer>().enabled = true;
                UnityEngine.Object.Destroy(BaseB12.GetComponent<Collider>());
                BaseB12.transform.parent = menu.transform;
                BaseB12.transform.rotation = Quaternion.identity;
                BaseB12.transform.localPosition = gameObject12.transform.localPosition;
                BaseB12.transform.localScale = gameObject12.transform.localScale + new Vector3(0f, 0f, -bevel * 2f);

                GameObject RoundCornerA12 = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
                RoundCornerA12.GetComponent<Renderer>().enabled = true;
                UnityEngine.Object.Destroy(RoundCornerA12.GetComponent<Collider>());
                RoundCornerA12.transform.parent = menu.transform;
                RoundCornerA12.transform.rotation = Quaternion.Euler(0f, 0f, 90f);
                RoundCornerA12.transform.localPosition = gameObject12.transform.localPosition + new Vector3(0f, (gameObject12.transform.localScale.y / 2f) - (bevel * 1.275f), (gameObject12.transform.localScale.z / 2f) - bevel);
                RoundCornerA12.transform.localScale = new Vector3(bevel * 2.55f, gameObject12.transform.localScale.x / 2f, bevel * 2f);

                GameObject RoundCornerB12 = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
                RoundCornerB12.GetComponent<Renderer>().enabled = true;
                UnityEngine.Object.Destroy(RoundCornerB12.GetComponent<Collider>());
                RoundCornerB12.transform.parent = menu.transform;
                RoundCornerB12.transform.rotation = Quaternion.Euler(0f, 0f, 90f);
                RoundCornerB12.transform.localPosition = gameObject12.transform.localPosition + new Vector3(0f, -(gameObject12.transform.localScale.y / 2f) + (bevel * 1.275f), (gameObject12.transform.localScale.z / 2f) - bevel);
                RoundCornerB12.transform.localScale = new Vector3(bevel * 2.55f, gameObject12.transform.localScale.x / 2f, bevel * 2f);

                GameObject RoundCornerC12 = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
                RoundCornerC12.GetComponent<Renderer>().enabled = true;
                UnityEngine.Object.Destroy(RoundCornerC12.GetComponent<Collider>());
                RoundCornerC12.transform.parent = menu.transform;
            RoundCornerC12.transform.rotation = Quaternion.Euler(0f, 0f, 90f);
                RoundCornerC12.transform.localPosition = gameObject12.transform.localPosition + new Vector3(0f, (gameObject12.transform.localScale.y / 2f) - (bevel * 1.275f), -(gameObject12.transform.localScale.z / 2f) + bevel);
                RoundCornerC12.transform.localScale = new Vector3(bevel * 2.55f, gameObject12.transform.localScale.x / 2f, bevel * 2f);

                GameObject RoundCornerD12 = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
                RoundCornerD12.GetComponent<Renderer>().enabled = true;
                UnityEngine.Object.Destroy(RoundCornerD12.GetComponent<Collider>());
                RoundCornerD12.transform.parent = menu.transform;
                RoundCornerD12.transform.rotation = Quaternion.Euler(0f, 0f, 90f);
                RoundCornerD12.transform.localPosition = gameObject12.transform.localPosition + new Vector3(0f, -(gameObject12.transform.localScale.y / 2f) + (bevel * 1.275f), -(gameObject12.transform.localScale.z / 2f) + bevel);
                RoundCornerD12.transform.localScale = new Vector3(bevel * 2.55f, gameObject12.transform.localScale.x / 2f, bevel * 2f);

                GameObject[] ToChange12 = new GameObject[]
                {
    BaseA12,
    BaseB12,
    RoundCornerA12,
    RoundCornerB12,
    RoundCornerC12,
    RoundCornerD12
                };

                foreach (GameObject obj in ToChange12)
                {
                    obj.GetComponent<Renderer>().material.color = new Color(0.745f, 0.541f, 0.733f);
                }

                GameObject gameObject13 = GameObject.CreatePrimitive(PrimitiveType.Cube);
                if (!UnityInput.Current.GetKey(KeyCode.Q))
                {
                    gameObject13.layer = 2;
                }
                UnityEngine.Object.Destroy(gameObject13.GetComponent<Rigidbody>());
                gameObject13.GetComponent<BoxCollider>().isTrigger = true;
                gameObject13.transform.parent = menu.transform;
                gameObject13.transform.rotation = Quaternion.identity;
                gameObject13.transform.localScale = new Vector3(0.095f, 0.37f, 0.105f);
                gameObject13.transform.localPosition = new Vector3(0.56f, 0.302f, 0.6f);
                gameObject13.GetComponent<Renderer>().material.color = new Color(0.745f, 0.541f, 0.733f);
                gameObject13.AddComponent<Classes.Button>().relatedText = "home";

                gameObject13.GetComponent<Renderer>().enabled = false;

                Renderer ToRoundRenderer13 = gameObject13.GetComponent<Renderer>();

                GameObject BaseA13 = GameObject.CreatePrimitive(PrimitiveType.Cube);
                BaseA13.GetComponent<Renderer>().enabled = true;
                UnityEngine.Object.Destroy(BaseA13.GetComponent<Collider>());
                BaseA13.transform.parent = menu.transform;
                BaseA13.transform.rotation = Quaternion.identity;
                BaseA13.transform.localPosition = gameObject13.transform.localPosition;
                BaseA13.transform.localScale = gameObject13.transform.localScale + new Vector3(0f, bevel * -2.55f, 0f);
                BaseA13.AddComponent<Classes.Button>().relatedText = "home";

                GameObject BaseB13 = GameObject.CreatePrimitive(PrimitiveType.Cube);
                BaseB13.GetComponent<Renderer>().enabled = true;
                UnityEngine.Object.Destroy(BaseB13.GetComponent<Collider>());
                BaseB13.transform.parent = menu.transform;
                BaseB13.transform.rotation = Quaternion.identity;
                BaseB13.transform.localPosition = gameObject13.transform.localPosition;
                BaseB13.transform.localScale = gameObject13.transform.localScale + new Vector3(0f, 0f, -bevel * 2f);


                GameObject RoundCornerA13 = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
                RoundCornerA13.GetComponent<Renderer>().enabled = true;
                UnityEngine.Object.Destroy(RoundCornerA13.GetComponent<Collider>());
                RoundCornerA13.transform.parent = menu.transform;
                RoundCornerA13.transform.rotation = Quaternion.Euler(0f, 0f, 90f); 
                RoundCornerA13.transform.localPosition = gameObject13.transform.localPosition + new Vector3(0f, (gameObject13.transform.localScale.y / 2f) - (bevel * 1.275f), (gameObject13.transform.localScale.z / 2f) - bevel);
                RoundCornerA13.transform.localScale = new Vector3(bevel * 2.55f, gameObject13.transform.localScale.x / 2f, bevel * 2f);


                GameObject RoundCornerB13 = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
                RoundCornerB13.GetComponent<Renderer>().enabled = true;
                UnityEngine.Object.Destroy(RoundCornerB13.GetComponent<Collider>());
                RoundCornerB13.transform.parent = menu.transform;
                RoundCornerB13.transform.rotation = Quaternion.Euler(0f, 0f, 90f); 
                RoundCornerB13.transform.localPosition = gameObject13.transform.localPosition + new Vector3(0f, -(gameObject13.transform.localScale.y / 2f) + (bevel * 1.275f), (gameObject13.transform.localScale.z / 2f) - bevel);
                RoundCornerB13.transform.localScale = new Vector3(bevel * 2.55f, gameObject13.transform.localScale.x / 2f, bevel * 2f);

                GameObject RoundCornerC13 = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
                RoundCornerC13.GetComponent<Renderer>().enabled = true;
                UnityEngine.Object.Destroy(RoundCornerC13.GetComponent<Collider>());
                RoundCornerC13.transform.parent = menu.transform;
                RoundCornerC13.transform.rotation = Quaternion.Euler(0f, 0f, 90f); 
                RoundCornerC13.transform.localPosition = gameObject13.transform.localPosition + new Vector3(0f, (gameObject13.transform.localScale.y / 2f) - (bevel * 1.275f), -(gameObject13.transform.localScale.z / 2f) + bevel);
                RoundCornerC13.transform.localScale = new Vector3(bevel * 2.55f, gameObject13.transform.localScale.x / 2f, bevel * 2f);

                GameObject RoundCornerD13 = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
                RoundCornerD13.GetComponent<Renderer>().enabled = true;
                UnityEngine.Object.Destroy(RoundCornerD13.GetComponent<Collider>());
                RoundCornerD13.transform.parent = menu.transform;
                RoundCornerD13.transform.rotation = Quaternion.Euler(0f, 0f, 90f); 
                RoundCornerD13.transform.localPosition = gameObject13.transform.localPosition + new Vector3(0f, -(gameObject13.transform.localScale.y / 2f) + (bevel * 1.275f), -(gameObject13.transform.localScale.z / 2f) + bevel);
                RoundCornerD13.transform.localScale = new Vector3(bevel * 2.55f, gameObject13.transform.localScale.x / 2f, bevel * 2f);

                GameObject[] ToChange13 = new GameObject[]
                {
    BaseA13,
    BaseB13,
    RoundCornerA13,
    RoundCornerB13,
    RoundCornerC13,
    RoundCornerD13
                };

                foreach (GameObject obj in ToChange13)
                {
                    obj.GetComponent<Renderer>().material.color = new Color(0.863f, 0.655f, 0.867f); 
            }
            if (disconnectButton)
            {
                GameObject gameObject14 = GameObject.CreatePrimitive(PrimitiveType.Cube);
                if (!UnityInput.Current.GetKey(KeyCode.Q))
                {
                    gameObject14.layer = 2;
                }
                UnityEngine.Object.Destroy(gameObject14.GetComponent<Rigidbody>());
                gameObject14.GetComponent<BoxCollider>().isTrigger = true;
                gameObject14.transform.parent = menu.transform;
                gameObject14.transform.rotation = Quaternion.identity;
                gameObject14.transform.localScale = new Vector3(0.095f, 0.37f, 0.105f);
                gameObject14.transform.localPosition = new Vector3(0.56f, -0.302f, 0.6f);
                gameObject14.GetComponent<Renderer>().material.color = new Color(0.745f, 0.541f, 0.733f);


                gameObject14.GetComponent<Renderer>().enabled = false;


                Renderer ToRoundRenderer14 = gameObject14.GetComponent<Renderer>();

                GameObject BaseA14 = GameObject.CreatePrimitive(PrimitiveType.Cube);
                BaseA14.GetComponent<Renderer>().enabled = true;
                UnityEngine.Object.Destroy(BaseA14.GetComponent<Collider>());
                BaseA14.transform.parent = menu.transform;
                BaseA14.transform.rotation = Quaternion.identity;
                BaseA14.transform.localPosition = gameObject14.transform.localPosition;
                BaseA14.transform.localScale = gameObject14.transform.localScale + new Vector3(0f, bevel * -2.55f, 0f);
                BaseA14.AddComponent<Classes.Button>().relatedText = "Disconnect";

                GameObject BaseB14 = GameObject.CreatePrimitive(PrimitiveType.Cube);
                BaseB14.GetComponent<Renderer>().enabled = true;
                UnityEngine.Object.Destroy(BaseB14.GetComponent<Collider>());
                BaseB14.transform.parent = menu.transform;
                BaseB14.transform.rotation = Quaternion.identity;
                BaseB14.transform.localPosition = gameObject14.transform.localPosition;
                BaseB14.transform.localScale = gameObject14.transform.localScale + new Vector3(0f, 0f, -bevel * 2f);

                GameObject RoundCornerA14 = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
                RoundCornerA14.GetComponent<Renderer>().enabled = true;
                UnityEngine.Object.Destroy(RoundCornerA14.GetComponent<Collider>());
                RoundCornerA14.transform.parent = menu.transform;
                RoundCornerA14.transform.rotation = Quaternion.Euler(0f, 0f, 90f); 
                RoundCornerA14.transform.localPosition = gameObject14.transform.localPosition + new Vector3(0f, (gameObject14.transform.localScale.y / 2f) - (bevel * 1.275f), (gameObject14.transform.localScale.z / 2f) - bevel);
                RoundCornerA14.transform.localScale = new Vector3(bevel * 2.55f, gameObject14.transform.localScale.x / 2f, bevel * 2f);

                GameObject RoundCornerB14 = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
                RoundCornerB14.GetComponent<Renderer>().enabled = true;
                UnityEngine.Object.Destroy(RoundCornerB14.GetComponent<Collider>());
                RoundCornerB14.transform.parent = menu.transform;
                RoundCornerB14.transform.rotation = Quaternion.Euler(0f, 0f, 90f);
                RoundCornerB14.transform.localPosition = gameObject14.transform.localPosition + new Vector3(0f, -(gameObject14.transform.localScale.y / 2f) + (bevel * 1.275f), (gameObject14.transform.localScale.z / 2f) - bevel);
                RoundCornerB14.transform.localScale = new Vector3(bevel * 2.55f, gameObject14.transform.localScale.x / 2f, bevel * 2f);

                GameObject RoundCornerC14 = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
                RoundCornerC14.GetComponent<Renderer>().enabled = true;
                UnityEngine.Object.Destroy(RoundCornerC14.GetComponent<Collider>());
                RoundCornerC14.transform.parent = menu.transform;
                RoundCornerC14.transform.rotation = Quaternion.Euler(0f, 0f, 90f);
                RoundCornerC14.transform.localPosition = gameObject14.transform.localPosition + new Vector3(0f, (gameObject14.transform.localScale.y / 2f) - (bevel * 1.275f), -(gameObject14.transform.localScale.z / 2f) + bevel);
                RoundCornerC14.transform.localScale = new Vector3(bevel * 2.55f, gameObject14.transform.localScale.x / 2f, bevel * 2f);

                GameObject RoundCornerD14 = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
                RoundCornerD14.GetComponent<Renderer>().enabled = true;
                UnityEngine.Object.Destroy(RoundCornerD14.GetComponent<Collider>());
                RoundCornerD14.transform.parent = menu.transform;
                RoundCornerD14.transform.rotation = Quaternion.Euler(0f, 0f, 90f); 
                RoundCornerD14.transform.localPosition = gameObject14.transform.localPosition + new Vector3(0f, -(gameObject14.transform.localScale.y / 2f) + (bevel * 1.275f), -(gameObject14.transform.localScale.z / 2f) + bevel);
                RoundCornerD14.transform.localScale = new Vector3(bevel * 2.55f, gameObject14.transform.localScale.x / 2f, bevel * 2f);


                GameObject[] ToChange14 = new GameObject[]
                {
    BaseA14,
    BaseB14,
    RoundCornerA14,
    RoundCornerB14,
    RoundCornerC14,
    RoundCornerD14
                };

                foreach (GameObject obj in ToChange14)
                {
                    obj.GetComponent<Renderer>().material.color = new Color(0.863f, 0.655f, 0.867f); 
                }


                GameObject gameObject54 = GameObject.CreatePrimitive(PrimitiveType.Cube);
                if (!UnityInput.Current.GetKey(KeyCode.Q))
                {
                    gameObject54.layer = 2;
                }
                UnityEngine.Object.Destroy(gameObject54.GetComponent<Rigidbody>());
                gameObject54.GetComponent<BoxCollider>().isTrigger = true;
                gameObject54.transform.parent = menu.transform;
                gameObject54.transform.rotation = Quaternion.identity;
                gameObject54.transform.localScale = new Vector3(0.1f, 0.35f, 0.1f);
                gameObject54.transform.localPosition = new Vector3(0.56f, -0.302f, 0.6f);
                gameObject54.GetComponent<Renderer>().material.color = new Color(0.745f, 0.541f, 0.733f);

                gameObject54.AddComponent<Classes.Button>().relatedText = "Disconnect";

                gameObject54.GetComponent<Renderer>().enabled = false;

                Renderer ToRoundRenderer54 = gameObject54.GetComponent<Renderer>();


                GameObject BaseA54 = GameObject.CreatePrimitive(PrimitiveType.Cube);
                BaseA54.GetComponent<Renderer>().enabled = true;
                UnityEngine.Object.Destroy(BaseA54.GetComponent<Collider>());
                BaseA54.transform.parent = menu.transform;
                BaseA54.transform.rotation = Quaternion.identity;
                BaseA54.transform.localPosition = gameObject54.transform.localPosition;
                BaseA54.transform.localScale = gameObject54.transform.localScale + new Vector3(0f, bevel * -2.55f, 0f);

                GameObject BaseB54 = GameObject.CreatePrimitive(PrimitiveType.Cube);
                BaseB54.GetComponent<Renderer>().enabled = true;
                UnityEngine.Object.Destroy(BaseB54.GetComponent<Collider>());
                BaseB54.transform.parent = menu.transform;
                BaseB54.transform.rotation = Quaternion.identity;
                BaseB54.transform.localPosition = gameObject54.transform.localPosition;
                BaseB54.transform.localScale = gameObject54.transform.localScale + new Vector3(0f, 0f, -bevel * 2f);


                GameObject RoundCornerA54 = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
                RoundCornerA54.GetComponent<Renderer>().enabled = true;
                UnityEngine.Object.Destroy(RoundCornerA54.GetComponent<Collider>());
                RoundCornerA54.transform.parent = menu.transform;
                RoundCornerA54.transform.rotation = Quaternion.Euler(0f, 0f, 90f); 
                RoundCornerA54.transform.localPosition = gameObject54.transform.localPosition + new Vector3(0f, (gameObject54.transform.localScale.y / 2f) - (bevel * 1.275f), (gameObject54.transform.localScale.z / 2f) - bevel);
                RoundCornerA54.transform.localScale = new Vector3(bevel * 2.55f, gameObject54.transform.localScale.x / 2f, bevel * 2f);

                GameObject RoundCornerB54 = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
                RoundCornerB54.GetComponent<Renderer>().enabled = true;
                UnityEngine.Object.Destroy(RoundCornerB54.GetComponent<Collider>());
                RoundCornerB54.transform.parent = menu.transform;
                RoundCornerB54.transform.rotation = Quaternion.Euler(0f, 0f, 90f); 
                RoundCornerB54.transform.localPosition = gameObject54.transform.localPosition + new Vector3(0f, -(gameObject54.transform.localScale.y / 2f) + (bevel * 1.275f), (gameObject54.transform.localScale.z / 2f) - bevel);
                RoundCornerB54.transform.localScale = new Vector3(bevel * 2.55f, gameObject54.transform.localScale.x / 2f, bevel * 2f);

                GameObject RoundCornerC54 = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
                RoundCornerC54.GetComponent<Renderer>().enabled = true;
                UnityEngine.Object.Destroy(RoundCornerC54.GetComponent<Collider>());
                RoundCornerC54.transform.parent = menu.transform;
                RoundCornerC54.transform.rotation = Quaternion.Euler(0f, 0f, 90f); 
                RoundCornerC54.transform.localPosition = gameObject54.transform.localPosition + new Vector3(0f, (gameObject54.transform.localScale.y / 2f) - (bevel * 1.275f), -(gameObject54.transform.localScale.z / 2f) + bevel);
                RoundCornerC54.transform.localScale = new Vector3(bevel * 2.55f, gameObject54.transform.localScale.x / 2f, bevel * 2f);

                GameObject RoundCornerD54 = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
                RoundCornerD54.GetComponent<Renderer>().enabled = true;
                UnityEngine.Object.Destroy(RoundCornerD54.GetComponent<Collider>());
                RoundCornerD54.transform.parent = menu.transform;
                RoundCornerD54.transform.rotation = Quaternion.Euler(0f, 0f, 90f);
                RoundCornerD54.transform.localPosition = gameObject54.transform.localPosition + new Vector3(0f, -(gameObject54.transform.localScale.y / 2f) + (bevel * 1.275f), -(gameObject54.transform.localScale.z / 2f) + bevel);
                RoundCornerD54.transform.localScale = new Vector3(bevel * 2.55f, gameObject54.transform.localScale.x / 2f, bevel * 2f);

                GameObject[] ToChange54 = new GameObject[]
                {
    BaseA54,
    BaseB54,
    RoundCornerA54,
    RoundCornerB54,
    RoundCornerC54,
    RoundCornerD54
                };

                foreach (GameObject obj in ToChange54)
                {
                    obj.GetComponent<Renderer>().material.color = new Color(0.745f, 0.541f, 0.733f); 
                }

            }


            GameObject Home = GameObject.CreatePrimitive(PrimitiveType.Cube);
            if (!UnityInput.Current.GetKey(KeyCode.Q))
            {
                Home.layer = 2;
            }
            UnityEngine.Object.Destroy(Home.GetComponent<Rigidbody>());
            Home.GetComponent<BoxCollider>().isTrigger = true;
            Home.transform.parent = menu.transform;
            Home.transform.rotation = Quaternion.identity;
            Home.transform.localScale = new Vector3(0.1f, 0.35f, 0.1f);
            Home.transform.localPosition = new Vector3(0.56f, -0.3f, -0.6f);
            Home.GetComponent<Renderer>().material.color = new Color(0.745f, 0.541f, 0.733f);
            Home.AddComponent<Classes.Button>().relatedText = "PreviousPage";

            Home.GetComponent<Renderer>().enabled = false;
            Renderer ToRoundRendererHome = Home.GetComponent<Renderer>();

            GameObject BaseAHome = GameObject.CreatePrimitive(PrimitiveType.Cube);
            BaseAHome.GetComponent<Renderer>().enabled = true;
            UnityEngine.Object.Destroy(BaseAHome.GetComponent<Collider>());
            BaseAHome.transform.parent = menu.transform;
            BaseAHome.transform.rotation = Quaternion.identity;
            BaseAHome.transform.localPosition = Home.transform.localPosition;
            BaseAHome.transform.localScale = Home.transform.localScale + new Vector3(0f, bevel * -2.55f, 0f);

            GameObject BaseBHome = GameObject.CreatePrimitive(PrimitiveType.Cube);
            BaseBHome.GetComponent<Renderer>().enabled = true;
            UnityEngine.Object.Destroy(BaseBHome.GetComponent<Collider>());
            BaseBHome.transform.parent = menu.transform;
            BaseBHome.transform.rotation = Quaternion.identity;
            BaseBHome.transform.localPosition = Home.transform.localPosition;
            BaseBHome.transform.localScale = Home.transform.localScale + new Vector3(0f, 0f, -bevel * 2f);

            GameObject RoundCornerAHome = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            RoundCornerAHome.GetComponent<Renderer>().enabled = true;
            UnityEngine.Object.Destroy(RoundCornerAHome.GetComponent<Collider>());
            RoundCornerAHome.transform.parent = menu.transform;
            RoundCornerAHome.transform.rotation = Quaternion.Euler(0f, 0f, 90f); 
            RoundCornerAHome.transform.localPosition = Home.transform.localPosition + new Vector3(0f, (Home.transform.localScale.y / 2f) - (bevel * 1.275f), (Home.transform.localScale.z / 2f) - bevel);
            RoundCornerAHome.transform.localScale = new Vector3(bevel * 2.55f, Home.transform.localScale.x / 2f, bevel * 2f);

            GameObject RoundCornerBHome = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            RoundCornerBHome.GetComponent<Renderer>().enabled = true;
            UnityEngine.Object.Destroy(RoundCornerBHome.GetComponent<Collider>());
            RoundCornerBHome.transform.parent = menu.transform;
            RoundCornerBHome.transform.rotation = Quaternion.Euler(0f, 0f, 90f);
            RoundCornerBHome.transform.localPosition = Home.transform.localPosition + new Vector3(0f, -(Home.transform.localScale.y / 2f) + (bevel * 1.275f), (Home.transform.localScale.z / 2f) - bevel);
            RoundCornerBHome.transform.localScale = new Vector3(bevel * 2.55f, Home.transform.localScale.x / 2f, bevel * 2f);

            GameObject RoundCornerCHome = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            RoundCornerCHome.GetComponent<Renderer>().enabled = true;
            UnityEngine.Object.Destroy(RoundCornerCHome.GetComponent<Collider>());
            RoundCornerCHome.transform.parent = menu.transform;
            RoundCornerCHome.transform.rotation = Quaternion.Euler(0f, 0f, 90f);
            RoundCornerCHome.transform.localPosition = Home.transform.localPosition + new Vector3(0f, (Home.transform.localScale.y / 2f) - (bevel * 1.275f), -(Home.transform.localScale.z / 2f) + bevel);
            RoundCornerCHome.transform.localScale = new Vector3(bevel * 2.55f, Home.transform.localScale.x / 2f, bevel * 2f);

            GameObject RoundCornerDHome = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            RoundCornerDHome.GetComponent<Renderer>().enabled = true;
            UnityEngine.Object.Destroy(RoundCornerDHome.GetComponent<Collider>());
            RoundCornerDHome.transform.parent = menu.transform;
            RoundCornerDHome.transform.rotation = Quaternion.Euler(0f, 0f, 90f);
            RoundCornerDHome.transform.localPosition = Home.transform.localPosition + new Vector3(0f, -(Home.transform.localScale.y / 2f) + (bevel * 1.275f), -(Home.transform.localScale.z / 2f) + bevel);
            RoundCornerDHome.transform.localScale = new Vector3(bevel * 2.55f, Home.transform.localScale.x / 2f, bevel * 2f);

            GameObject[] ToChangeHome = new GameObject[]
            {
    BaseAHome,
    BaseBHome,
    RoundCornerAHome,
    RoundCornerBHome,
    RoundCornerCHome,
    RoundCornerDHome
            };

            foreach (GameObject obj in ToChangeHome)
            {
                obj.GetComponent<Renderer>().material.color = new Color(0.745f, 0.541f, 0.733f); 
            }


            GameObject gameObject1 = GameObject.CreatePrimitive(PrimitiveType.Cube);
            if (!UnityInput.Current.GetKey(KeyCode.Q))
            {
                gameObject1.layer = 2;
            }
            UnityEngine.Object.Destroy(gameObject1.GetComponent<Rigidbody>());
            gameObject1.GetComponent<BoxCollider>().isTrigger = true;
            gameObject1.transform.parent = menu.transform;
            gameObject1.transform.rotation = Quaternion.identity;
            gameObject1.transform.localScale = new Vector3(0.095f, 0.37f, 0.105f);
            gameObject1.transform.localPosition = new Vector3(0.56f, -0.3f, -0.6f);
            gameObject1.GetComponent<Renderer>().material.color = new Color(0.863f, 0.655f, 0.867f);

            gameObject1.GetComponent<Renderer>().enabled = false;

            Renderer ToRoundRenderer1 = gameObject1.GetComponent<Renderer>();

            GameObject BaseA1 = GameObject.CreatePrimitive(PrimitiveType.Cube);
            BaseA1.GetComponent<Renderer>().enabled = true;
            UnityEngine.Object.Destroy(BaseA1.GetComponent<Collider>());
            BaseA1.transform.parent = menu.transform;
            BaseA1.transform.rotation = Quaternion.identity;
            BaseA1.transform.localPosition = gameObject1.transform.localPosition;
            BaseA1.transform.localScale = gameObject1.transform.localScale + new Vector3(0f, bevel * -2.55f, 0f);

            GameObject BaseB1 = GameObject.CreatePrimitive(PrimitiveType.Cube);
            BaseB1.GetComponent<Renderer>().enabled = true;
            UnityEngine.Object.Destroy(BaseB1.GetComponent<Collider>());
            BaseB1.transform.parent = menu.transform;
            BaseB1.transform.rotation = Quaternion.identity;
            BaseB1.transform.localPosition = gameObject1.transform.localPosition;
            BaseB1.transform.localScale = gameObject1.transform.localScale + new Vector3(0f, 0f, -bevel * 2f);

            GameObject RoundCornerA1 = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            RoundCornerA1.GetComponent<Renderer>().enabled = true;
            UnityEngine.Object.Destroy(RoundCornerA1.GetComponent<Collider>());
            RoundCornerA1.transform.parent = menu.transform;
            RoundCornerA1.transform.rotation = Quaternion.Euler(0f, 0f, 90f); 
            RoundCornerA1.transform.localPosition = gameObject1.transform.localPosition + new Vector3(0f, (gameObject1.transform.localScale.y / 2f) - (bevel * 1.275f), (gameObject1.transform.localScale.z / 2f) - bevel);
            RoundCornerA1.transform.localScale = new Vector3(bevel * 2.55f, gameObject1.transform.localScale.x / 2f, bevel * 2f);

            GameObject RoundCornerB1 = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            RoundCornerB1.GetComponent<Renderer>().enabled = true;
            UnityEngine.Object.Destroy(RoundCornerB1.GetComponent<Collider>());
            RoundCornerB1.transform.parent = menu.transform;
            RoundCornerB1.transform.rotation = Quaternion.Euler(0f, 0f, 90f);
            RoundCornerB1.transform.localPosition = gameObject1.transform.localPosition + new Vector3(0f, -(gameObject1.transform.localScale.y / 2f) + (bevel * 1.275f), (gameObject1.transform.localScale.z / 2f) - bevel);
            RoundCornerB1.transform.localScale = new Vector3(bevel * 2.55f, gameObject1.transform.localScale.x / 2f, bevel * 2f);

            GameObject RoundCornerC1 = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            RoundCornerC1.GetComponent<Renderer>().enabled = true;
            UnityEngine.Object.Destroy(RoundCornerC1.GetComponent<Collider>());
            RoundCornerC1.transform.parent = menu.transform;
            RoundCornerC1.transform.rotation = Quaternion.Euler(0f, 0f, 90f); 
            RoundCornerC1.transform.localPosition = gameObject1.transform.localPosition + new Vector3(0f, (gameObject1.transform.localScale.y / 2f) - (bevel * 1.275f), -(gameObject1.transform.localScale.z / 2f) + bevel);
            RoundCornerC1.transform.localScale = new Vector3(bevel * 2.55f, gameObject1.transform.localScale.x / 2f, bevel * 2f);

            GameObject RoundCornerD1 = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            RoundCornerD1.GetComponent<Renderer>().enabled = true;
            UnityEngine.Object.Destroy(RoundCornerD1.GetComponent<Collider>());
            RoundCornerD1.transform.parent = menu.transform;
            RoundCornerD1.transform.rotation = Quaternion.Euler(0f, 0f, 90f); 
            RoundCornerD1.transform.localPosition = gameObject1.transform.localPosition + new Vector3(0f, -(gameObject1.transform.localScale.y / 2f) + (bevel * 1.275f), -(gameObject1.transform.localScale.z / 2f) + bevel);
            RoundCornerD1.transform.localScale = new Vector3(bevel * 2.55f, gameObject1.transform.localScale.x / 2f, bevel * 2f);

            GameObject[] ToChange1 = new GameObject[]
            {
    BaseA1,
    BaseB1,
    RoundCornerA1,
    RoundCornerB1,
    RoundCornerC1,
    RoundCornerD1
            };

            foreach (GameObject obj in ToChange1)
            {
                obj.GetComponent<Renderer>().material.color = new Color(0.863f, 0.655f, 0.867f); 
            }

            GameObject Home1 = GameObject.CreatePrimitive(PrimitiveType.Cube);
            if (!UnityInput.Current.GetKey(KeyCode.Q))
            {
                Home1.layer = 2;
            }
            UnityEngine.Object.Destroy(Home1.GetComponent<Rigidbody>());
            Home1.GetComponent<BoxCollider>().isTrigger = true;
            Home1.transform.parent = menu.transform;
            Home1.transform.rotation = Quaternion.identity;
            Home1.transform.localScale = new Vector3(0.1f, 0.35f, 0.1f);
            Home1.transform.localPosition = new Vector3(0.56f, 0.3f, -0.6f);
            Home1.GetComponent<Renderer>().material.color = new Color(0.745f, 0.541f, 0.733f); 

            Home1.GetComponent<Renderer>().enabled = false;

            Renderer ToRoundRendererHome1 = Home1.GetComponent<Renderer>();

            GameObject BaseAHome1 = GameObject.CreatePrimitive(PrimitiveType.Cube);
            BaseAHome1.GetComponent<Renderer>().enabled = true;
            UnityEngine.Object.Destroy(BaseAHome1.GetComponent<Collider>());
            BaseAHome1.transform.parent = menu.transform;
            BaseAHome1.transform.rotation = Quaternion.identity;
            BaseAHome1.transform.localPosition = Home1.transform.localPosition;
            BaseAHome1.transform.localScale = Home1.transform.localScale + new Vector3(0f, bevel * -2.55f, 0f);

            GameObject BaseBHome1 = GameObject.CreatePrimitive(PrimitiveType.Cube);
            BaseBHome1.GetComponent<Renderer>().enabled = true;
            UnityEngine.Object.Destroy(BaseBHome1.GetComponent<Collider>());
            BaseBHome1.transform.parent = menu.transform;
            BaseBHome1.transform.rotation = Quaternion.identity;
            BaseBHome1.transform.localPosition = Home1.transform.localPosition;
            BaseBHome1.transform.localScale = Home1.transform.localScale + new Vector3(0f, 0f, -bevel * 2f);

            GameObject RoundCornerAHome1 = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            RoundCornerAHome1.GetComponent<Renderer>().enabled = true;
            UnityEngine.Object.Destroy(RoundCornerAHome1.GetComponent<Collider>());
            RoundCornerAHome1.transform.parent = menu.transform;
            RoundCornerAHome1.transform.rotation = Quaternion.Euler(0f, 0f, 90f); 
            RoundCornerAHome1.transform.localPosition = Home1.transform.localPosition + new Vector3(0f, (Home1.transform.localScale.y / 2f) - (bevel * 1.275f), (Home1.transform.localScale.z / 2f) - bevel);
            RoundCornerAHome1.transform.localScale = new Vector3(bevel * 2.55f, Home1.transform.localScale.x / 2f, bevel * 2f);

 
            GameObject RoundCornerBHome1 = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            RoundCornerBHome1.GetComponent<Renderer>().enabled = true;
            UnityEngine.Object.Destroy(RoundCornerBHome1.GetComponent<Collider>());
            RoundCornerBHome1.transform.parent = menu.transform;
            RoundCornerBHome1.transform.rotation = Quaternion.Euler(0f, 0f, 90f); 
            RoundCornerBHome1.transform.localPosition = Home1.transform.localPosition + new Vector3(0f, -(Home1.transform.localScale.y / 2f) + (bevel * 1.275f), (Home1.transform.localScale.z / 2f) - bevel);
            RoundCornerBHome1.transform.localScale = new Vector3(bevel * 2.55f, Home1.transform.localScale.x / 2f, bevel * 2f);

            GameObject RoundCornerCHome1 = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            RoundCornerCHome1.GetComponent<Renderer>().enabled = true;
            UnityEngine.Object.Destroy(RoundCornerCHome1.GetComponent<Collider>());
            RoundCornerCHome1.transform.parent = menu.transform;
            RoundCornerCHome1.transform.rotation = Quaternion.Euler(0f, 0f, 90f); 
            RoundCornerCHome1.transform.localPosition = Home1.transform.localPosition + new Vector3(0f, (Home1.transform.localScale.y / 2f) - (bevel * 1.275f), -(Home1.transform.localScale.z / 2f) + bevel);
            RoundCornerCHome1.transform.localScale = new Vector3(bevel * 2.55f, Home1.transform.localScale.x / 2f, bevel * 2f);

            GameObject RoundCornerDHome1 = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            RoundCornerDHome1.GetComponent<Renderer>().enabled = true;
            UnityEngine.Object.Destroy(RoundCornerDHome1.GetComponent<Collider>());
            RoundCornerDHome1.transform.parent = menu.transform;
            RoundCornerDHome1.transform.rotation = Quaternion.Euler(0f, 0f, 90f);
            RoundCornerDHome1.transform.localPosition = Home1.transform.localPosition + new Vector3(0f, -(Home1.transform.localScale.y / 2f) + (bevel * 1.275f), -(Home1.transform.localScale.z / 2f) + bevel);
            RoundCornerDHome1.transform.localScale = new Vector3(bevel * 2.55f, Home1.transform.localScale.x / 2f, bevel * 2f);

            GameObject[] ToChangeHome1 = new GameObject[]
            {
    BaseAHome1,
    BaseBHome1,
    RoundCornerAHome1,
    RoundCornerBHome1,
    RoundCornerCHome1,
    RoundCornerDHome1
            };

            foreach (GameObject obj in ToChangeHome1)
            {
                obj.GetComponent<Renderer>().material.color = new Color(0.745f, 0.541f, 0.733f); 
            }

            // Repeat for gameObject11 (Rounded)
            GameObject gameObject11 = GameObject.CreatePrimitive(PrimitiveType.Cube);
            if (!UnityInput.Current.GetKey(KeyCode.Q))
            {
                gameObject11.layer = 2;
            }
            UnityEngine.Object.Destroy(gameObject11.GetComponent<Rigidbody>());
            gameObject11.GetComponent<BoxCollider>().isTrigger = true;
            gameObject11.transform.parent = menu.transform;
            gameObject11.transform.rotation = Quaternion.identity;
            gameObject11.transform.localScale = new Vector3(0.095f, 0.37f, 0.105f);
            gameObject11.transform.localPosition = new Vector3(0.56f, 0.302f, -0.6f);
            gameObject11.GetComponent<Renderer>().material.color = new Color(0.863f, 0.655f, 0.867f); 
            gameObject11.AddComponent<Classes.Button>().relatedText = "NextPage";


            gameObject11.GetComponent<Renderer>().enabled = false;

            Renderer ToRoundRenderer11 = gameObject11.GetComponent<Renderer>();

            GameObject BaseA11 = GameObject.CreatePrimitive(PrimitiveType.Cube);
            BaseA11.GetComponent<Renderer>().enabled = true;
            UnityEngine.Object.Destroy(BaseA11.GetComponent<Collider>());
            BaseA11.transform.parent = menu.transform;
            BaseA11.transform.rotation = Quaternion.identity;
            BaseA11.transform.localPosition = gameObject11.transform.localPosition;
            BaseA11.transform.localScale = gameObject11.transform.localScale + new Vector3(0f, bevel * -2.55f, 0f);

            GameObject BaseB11 = GameObject.CreatePrimitive(PrimitiveType.Cube);
            BaseB11.GetComponent<Renderer>().enabled = true;
            UnityEngine.Object.Destroy(BaseB11.GetComponent<Collider>());
            BaseB11.transform.parent = menu.transform;
            BaseB11.transform.rotation = Quaternion.identity;
            BaseB11.transform.localPosition = gameObject11.transform.localPosition;
            BaseB11.transform.localScale = gameObject11.transform.localScale + new Vector3(0f, 0f, -bevel * 2f);

            GameObject RoundCornerA11 = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            RoundCornerA11.GetComponent<Renderer>().enabled = true;
            UnityEngine.Object.Destroy(RoundCornerA11.GetComponent<Collider>());
            RoundCornerA11.transform.parent = menu.transform;
            RoundCornerA11.transform.rotation = Quaternion.Euler(0f, 0f, 90f);  
            RoundCornerA11.transform.localPosition = gameObject11.transform.localPosition + new Vector3(0f, (gameObject11.transform.localScale.y / 2f) - (bevel * 1.275f), (gameObject11.transform.localScale.z / 2f) - bevel);
            RoundCornerA11.transform.localScale = new Vector3(bevel * 2.55f, gameObject11.transform.localScale.x / 2f, bevel * 2f);

    
            GameObject RoundCornerB11 = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            RoundCornerB11.GetComponent<Renderer>().enabled = true;
            UnityEngine.Object.Destroy(RoundCornerB11.GetComponent<Collider>());
            RoundCornerB11.transform.parent = menu.transform;
            RoundCornerB11.transform.rotation = Quaternion.Euler(0f, 0f, 90f);  
            RoundCornerB11.transform.localPosition = gameObject11.transform.localPosition + new Vector3(0f, -(gameObject11.transform.localScale.y / 2f) + (bevel * 1.275f), (gameObject11.transform.localScale.z / 2f) - bevel);
            RoundCornerB11.transform.localScale = new Vector3(bevel * 2.55f, gameObject11.transform.localScale.x / 2f, bevel * 2f);

            GameObject RoundCornerC11 = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            RoundCornerC11.GetComponent<Renderer>().enabled = true;
            UnityEngine.Object.Destroy(RoundCornerC11.GetComponent<Collider>());
            RoundCornerC11.transform.parent = menu.transform;
            RoundCornerC11.transform.rotation = Quaternion.Euler(0f, 0f, 90f); 
            RoundCornerC11.transform.localPosition = gameObject11.transform.localPosition + new Vector3(0f, (gameObject11.transform.localScale.y / 2f) - (bevel * 1.275f), -(gameObject11.transform.localScale.z / 2f) + bevel);
            RoundCornerC11.transform.localScale = new Vector3(bevel * 2.55f, gameObject11.transform.localScale.x / 2f, bevel * 2f);

            GameObject RoundCornerD11 = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            RoundCornerD11.GetComponent<Renderer>().enabled = true;
            UnityEngine.Object.Destroy(RoundCornerD11.GetComponent<Collider>());
            RoundCornerD11.transform.parent = menu.transform;
            RoundCornerD11.transform.rotation = Quaternion.Euler(0f, 0f, 90f);
            RoundCornerD11.transform.localPosition = gameObject11.transform.localPosition + new Vector3(0f, -(gameObject11.transform.localScale.y / 2f) + (bevel * 1.275f), -(gameObject11.transform.localScale.z / 2f) + bevel);
            RoundCornerD11.transform.localScale = new Vector3(bevel * 2.55f, gameObject11.transform.localScale.x / 2f, bevel * 2f);

            GameObject[] ToChange11 = new GameObject[]
            {
    BaseA11,
    BaseB11,
    RoundCornerA11,
    RoundCornerB11,
    RoundCornerC11,
    RoundCornerD11
            };

            foreach (GameObject obj in ToChange11)
            {
                obj.GetComponent<Renderer>().material.color = new Color(0.863f, 0.655f, 0.867f); // Light Purple
            }



            Text Homentext3 = new GameObject
            {
                transform =
            {
                parent = canvasObject.transform
            }
            }.AddComponent<Text>();
            Homentext3.text = ">";
            Homentext3.font = currentFont;
            Homentext3.fontSize = 2;
            Homentext3.color = textColors[0];
            Homentext3.alignment = TextAnchor.MiddleCenter;
            Homentext3.resizeTextForBestFit = true;
            Homentext3.resizeTextMinSize = 0;

            RectTransform rectt3 = Homentext3.GetComponent<RectTransform>();
            rectt3.localPosition = Vector3.zero;
            rectt3.sizeDelta = new Vector2(0.2f, 0.02f);
            rectt3.localPosition = new Vector3(0.062f, -0.095f, -0.23f);
            rectt3.rotation = Quaternion.Euler(new Vector3(180f, 90f, 90f));

            Text Homentext4 = new GameObject
            {
                transform =
            {
                parent = canvasObject.transform
            }
            }.AddComponent<Text>();
            Homentext4.text = "<";
            Homentext4.font = currentFont;
            Homentext4.fontSize = 2;
            Homentext4.color = textColors[0];
            Homentext4.alignment = TextAnchor.MiddleCenter;
            Homentext4.resizeTextForBestFit = true;
            Homentext4.resizeTextMinSize = 0;

            RectTransform rectt45 = Homentext4.GetComponent<RectTransform>();
            rectt45.localPosition = Vector3.zero;
            rectt45.sizeDelta = new Vector2(0.2f, 0.02f);
            rectt45.localPosition = new Vector3(0.062f, 0.095f, -0.23f);
            rectt45.rotation = Quaternion.Euler(new Vector3(180f, 90f, 90f));



            Text Homentext = new GameObject
            {
                transform =
            {
                parent = canvasObject.transform
            }
            }.AddComponent<Text>();
            Homentext.text = "home";
            Homentext.font = currentFont;
            Homentext.fontSize = 2;
            Homentext.color = textColors[0];
            Homentext.alignment = TextAnchor.MiddleCenter;
            Homentext.resizeTextForBestFit = true;
            Homentext.resizeTextMinSize = 0;

            RectTransform rectt1 = Homentext.GetComponent<RectTransform>();
            rectt1.localPosition = Vector3.zero;
            rectt1.sizeDelta = new Vector2(0.2f, 0.02f);
            rectt1.localPosition = new Vector3(0.062f, 0.095f, 0.23f);
            rectt1.rotation = Quaternion.Euler(new Vector3(180f, 90f, 90f));

            Text Homentext455 = new GameObject
            {
                transform =
            {
                parent = canvasObject.transform
            }
            }.AddComponent<Text>();
            Homentext455.text = "leave";
            Homentext455.font = currentFont;
            Homentext455.fontSize = 2;
            Homentext455.color = textColors[0];
            Homentext455.alignment = TextAnchor.MiddleCenter;
            Homentext455.resizeTextForBestFit = true;
            Homentext455.resizeTextMinSize = 0;

            RectTransform rectt43 = Homentext455.GetComponent<RectTransform>();
            rectt43.localPosition = Vector3.zero;
            rectt43.sizeDelta = new Vector2(0.2f, 0.02f);
            rectt43.localPosition = new Vector3(0.062f, -0.092f, 0.23f);
            rectt43.rotation = Quaternion.Euler(new Vector3(180f, 90f, 90f));

            // Mod Buttons
            ButtonInfo[] activeButtons = buttons[buttonsType].Skip(pageNumber * buttonsPerPage).Take(buttonsPerPage).ToArray();
            for (int i = 0; i < activeButtons.Length; i++)
            {
                CreateButton(i * 0.105f, activeButtons[i]);
            }
        }




        public static GameObject trailObject;
        public static TrailRenderer trail;


        public static void CreateButton(float offset, ButtonInfo method)
        {

            GameObject gameObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
            if (!UnityInput.Current.GetKey(KeyCode.Q))
            {
                gameObject.layer = 2;
            }
            UnityEngine.Object.Destroy(gameObject.GetComponent<Rigidbody>());
            gameObject.GetComponent<BoxCollider>().isTrigger = true;
            gameObject.transform.parent = menu.transform;
            gameObject.transform.rotation = Quaternion.identity;

            gameObject.transform.localScale = new Vector3(0.06f, 0.9f, 0.09f);
            gameObject.transform.localPosition = new Vector3(0.56f, 0f, 0.32f - offset);

            gameObject.GetComponent<Renderer>().enabled = false;

            float Bevel = 0.02f;
            GameObject BaseA = GameObject.CreatePrimitive(PrimitiveType.Cube);
            BaseA.GetComponent<Renderer>().enabled = true;  
            UnityEngine.Object.Destroy(BaseA.GetComponent<Collider>());
            BaseA.transform.parent = menu.transform;
            BaseA.transform.rotation = Quaternion.identity;
            BaseA.transform.localPosition = gameObject.transform.localPosition;
            BaseA.transform.localScale = gameObject.transform.localScale + new Vector3(0f, Bevel * -2.55f, 0f);
            BaseA.GetComponent<Renderer>().material.color = new Color(0.863f, 0.655f, 0.867f);

            GameObject BaseB = GameObject.CreatePrimitive(PrimitiveType.Cube);
            BaseB.GetComponent<Renderer>().enabled = true;  
            UnityEngine.Object.Destroy(BaseB.GetComponent<Collider>());
            BaseB.transform.parent = menu.transform;
            BaseB.transform.rotation = Quaternion.identity;
            BaseB.transform.localPosition = gameObject.transform.localPosition;
            BaseB.transform.localScale = gameObject.transform.localScale + new Vector3(0f, 0f, -Bevel * 2f);
            BaseB.GetComponent<Renderer>().material.color = new Color(0.863f, 0.655f, 0.867f); // Set color

            // Create round corner A
            GameObject RoundCornerA = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            RoundCornerA.GetComponent<Renderer>().enabled = true;  // Explicitly enable the renderer for the round corner
            UnityEngine.Object.Destroy(RoundCornerA.GetComponent<Collider>());
            RoundCornerA.transform.parent = menu.transform;
            RoundCornerA.transform.rotation = Quaternion.identity * Quaternion.Euler(0f, 0f, 90f);
            RoundCornerA.transform.localPosition = gameObject.transform.localPosition + new Vector3(0f, (gameObject.transform.localScale.y / 2f) - (Bevel * 1.275f), (gameObject.transform.localScale.z / 2f) - Bevel);
            RoundCornerA.transform.localScale = new Vector3(Bevel * 2.55f, gameObject.transform.localScale.x / 2f, Bevel * 2f);
            RoundCornerA.GetComponent<Renderer>().material.color = new Color(0.863f, 0.655f, 0.867f); // Set color

            // Create round corner B
            GameObject RoundCornerB = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            RoundCornerB.GetComponent<Renderer>().enabled = true;  // Explicitly enable the renderer for the round corner
            UnityEngine.Object.Destroy(RoundCornerB.GetComponent<Collider>());
            RoundCornerB.transform.parent = menu.transform;
            RoundCornerB.transform.rotation = Quaternion.identity * Quaternion.Euler(0f, 0f, 90f);
            RoundCornerB.transform.localPosition = gameObject.transform.localPosition + new Vector3(0f, -(gameObject.transform.localScale.y / 2f) + (Bevel * 1.275f), (gameObject.transform.localScale.z / 2f) - Bevel);
            RoundCornerB.transform.localScale = new Vector3(Bevel * 2.55f, gameObject.transform.localScale.x / 2f, Bevel * 2f);
            RoundCornerB.GetComponent<Renderer>().material.color = new Color(0.863f, 0.655f, 0.867f); // Set color

            // Create round corner C
            GameObject RoundCornerC = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            RoundCornerC.GetComponent<Renderer>().enabled = true;  // Explicitly enable the renderer for the round corner
            UnityEngine.Object.Destroy(RoundCornerC.GetComponent<Collider>());
            RoundCornerC.transform.parent = menu.transform;
            RoundCornerC.transform.rotation = Quaternion.identity * Quaternion.Euler(0f, 0f, 90f);
            RoundCornerC.transform.localPosition = gameObject.transform.localPosition + new Vector3(0f, (gameObject.transform.localScale.y / 2f) - (Bevel * 1.275f), -(gameObject.transform.localScale.z / 2f) + Bevel);
            RoundCornerC.transform.localScale = new Vector3(Bevel * 2.55f, gameObject.transform.localScale.x / 2f, Bevel * 2f);
            RoundCornerC.GetComponent<Renderer>().material.color = new Color(0.863f, 0.655f, 0.867f); // Set color

            // Create round corner D
            GameObject RoundCornerD = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            RoundCornerD.GetComponent<Renderer>().enabled = true;  // Explicitly enable the renderer for the round corner
            UnityEngine.Object.Destroy(RoundCornerD.GetComponent<Collider>());
            RoundCornerD.transform.parent = menu.transform;
            RoundCornerD.transform.rotation = Quaternion.identity * Quaternion.Euler(0f, 0f, 90f);
            RoundCornerD.transform.localPosition = gameObject.transform.localPosition + new Vector3(0f, -(gameObject.transform.localScale.y / 2f) + (Bevel * 1.275f), -(gameObject.transform.localScale.z / 2f) + Bevel);
            RoundCornerD.transform.localScale = new Vector3(Bevel * 2.55f, gameObject.transform.localScale.x / 2f, Bevel * 2f);
            RoundCornerD.GetComponent<Renderer>().material.color = new Color(0.863f, 0.655f, 0.867f); // Set color

            // Add the button component
            gameObject.AddComponent<Classes.Button>().relatedText = method.buttonText;

            // Set the color for the original object (if you want it to match)
            gameObject.GetComponent<Renderer>().material.color = new Color(0.863f, 0.655f, 0.867f); // Light Purple

            // Group all parts for easy management
            GameObject[] ToChange = new GameObject[]
            {
    BaseA,
    BaseB,
    RoundCornerA,
    RoundCornerB,
    RoundCornerC,
    RoundCornerD
            };




            ColorChanger colorChanger = gameObject.AddComponent<ColorChanger>();
            if (method.enabled)
            {
                colorChanger.colorInfo = buttonColors[1];
            }
            else
            {
                colorChanger.colorInfo = buttonColors[0];
            }
            colorChanger.Start();

            Text text = new GameObject
            {
                transform =
                {
                    parent = canvasObject.transform
                }
            }.AddComponent<Text>();
            text.font = currentFont;
            text.text = method.buttonText;
            if (method.overlapText != null)
            {
                text.text = method.overlapText;
            }
            text.supportRichText = true;
            text.fontSize = 1;
            if (method.enabled)
            {
                text.color = textColors[1];
            }
            else
            {
                text.color = textColors[0];
            }
            text.alignment = TextAnchor.MiddleCenter;
            text.fontStyle = FontStyle.Italic;
            text.resizeTextForBestFit = true;
            text.resizeTextMinSize = 0;
            RectTransform component = text.GetComponent<RectTransform>();
            component.localPosition = Vector3.zero;
            component.sizeDelta = new Vector2(.2f, .03f);
            component.localPosition = new Vector3(0.061f, 0, 0.125f - offset / 2.6f);
            component.rotation = Quaternion.Euler(new Vector3(180f, 90f, 90f));
        }

        public static void RecreateMenu()
        {
            if (menu != null)
            {
                UnityEngine.Object.Destroy(menu);
                menu = null;

                CreateMenu();
                RecenterMenu(rightHanded, UnityInput.Current.GetKey(keyboardButton));
            }
        }

        public static void RecenterMenu(bool isRightHanded, bool isKeyboardCondition)
        {
            if (!isKeyboardCondition)
            {
                if (!isRightHanded)
                {
                    menu.transform.position = GorillaTagger.Instance.leftHandTransform.position;
                    menu.transform.rotation = GorillaTagger.Instance.leftHandTransform.rotation;
                }
                else
                {
                    menu.transform.position = GorillaTagger.Instance.rightHandTransform.position;
                    Vector3 rotation = GorillaTagger.Instance.rightHandTransform.rotation.eulerAngles;
                    rotation += new Vector3(0f, 0f, 180f);
                    menu.transform.rotation = Quaternion.Euler(rotation);
                }
            }
            else
            {
                try
                {
                    TPC = GameObject.Find("Player Objects/Third Person Camera/Shoulder Camera").GetComponent<Camera>();
                }
                catch { }

                GameObject.Find("Shoulder Camera").transform.Find("CM vcam1").gameObject.SetActive(false);

                if (TPC != null)
                {
                    TPC.transform.position = new Vector3(-999f, -999f, -999f);
                    TPC.transform.rotation = Quaternion.identity;
                    GameObject bg = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    bg.transform.localScale = new Vector3(10f, 10f, 0.01f);
                    bg.transform.transform.position = TPC.transform.position + TPC.transform.forward;
                    bg.GetComponent<Renderer>().material.color = new Color32((byte)(backgroundColor.colors[0].color.r * 50), (byte)(backgroundColor.colors[0].color.g * 50), (byte)(backgroundColor.colors[0].color.b * 50), 255);
                    GameObject.Destroy(bg, Time.deltaTime);
                    menu.transform.parent = TPC.transform;
                    menu.transform.position = (TPC.transform.position + (Vector3.Scale(TPC.transform.forward, new Vector3(0.5f, 0.5f, 0.5f)))) + (Vector3.Scale(TPC.transform.up, new Vector3(-0.02f, -0.02f, -0.02f)));
                    Vector3 rot = TPC.transform.rotation.eulerAngles;
                    rot = new Vector3(rot.x - 90, rot.y + 90, rot.z);
                    menu.transform.rotation = Quaternion.Euler(rot);

                    if (reference != null)
                    {
                        if (Mouse.current.leftButton.isPressed)
                        {
                            Ray ray = TPC.ScreenPointToRay(Mouse.current.position.ReadValue());
                            RaycastHit hit;
                            bool worked = Physics.Raycast(ray, out hit, 100);
                            if (worked)
                            {
                                Classes.Button collide = hit.transform.gameObject.GetComponent<Classes.Button>();
                                if (collide != null)
                                {
                                    collide.OnTriggerEnter(buttonCollider);
                                }
                            }
                        }
                        else
                        {
                            reference.transform.position = new Vector3(999f, -999f, -999f);
                        }
                    }
                }
            }
        }

        public static void CreateReference(bool isRightHanded)
        {
            reference = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            if (isRightHanded)
            {
                reference.transform.parent = GorillaTagger.Instance.leftHandTransform;
            }
            else
            {
                reference.transform.parent = GorillaTagger.Instance.rightHandTransform;
            }
            reference.GetComponent<Renderer>().material.color = backgroundColor.colors[0].color;
            reference.transform.localPosition = new Vector3(0f, -0.1f, 0f);
            reference.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
            buttonCollider = reference.GetComponent<SphereCollider>();

            ColorChanger colorChanger = reference.AddComponent<ColorChanger>();
            colorChanger.colorInfo = backgroundColor;
            colorChanger.Start();
        }

        public static void Toggle(string buttonText)
        {
            int lastPage = ((buttons[buttonsType].Length + buttonsPerPage - 1) / buttonsPerPage) - 1;
            if (buttonText == "PreviousPage")
            {
                pageNumber--;
                if (pageNumber < 0)
                {
                    pageNumber = lastPage;
                }
            } else
            {
                if (buttonText == "NextPage")
                {
                    pageNumber++;
                    if (pageNumber > lastPage)
                    {
                        pageNumber = 0;
                    }
                } else
                {
                    ButtonInfo target = GetIndex(buttonText);
                    if (target != null)
                    {
                        if (target.isTogglable)
                        {
                            target.enabled = !target.enabled;
                            if (target.enabled)
                            {
                                NotifiLib.SendNotification("<color=grey>[</color><color=green>ENABLE</color><color=grey>]</color> " + target.toolTip);
                                if (target.enableMethod != null)
                                {
                                    try { target.enableMethod.Invoke(); } catch { }
                                }
                            }
                            else
                            {
                                NotifiLib.SendNotification("<color=grey>[</color><color=red>DISABLE</color><color=grey>]</color> " + target.toolTip);
                                if (target.disableMethod != null)
                                {
                                    try { target.disableMethod.Invoke(); } catch { }
                                }
                            }
                        }
                        else
                        {
                            NotifiLib.SendNotification("<color=grey>[</color><color=green>ENABLE</color><color=grey>]</color> " + target.toolTip);
                            if (target.method != null)
                            {
                                try { target.method.Invoke(); } catch { }
                            }
                        }
                    }
                    else
                    {
                        UnityEngine.Debug.LogError(buttonText + " does not exist");
                    }
                }
            }
            RecreateMenu();
        }

        public static GradientColorKey[] GetSolidGradient(Color color)
        {
            return new GradientColorKey[] { new GradientColorKey(color, 0f), new GradientColorKey(color, 1f) };
        }

        public static ButtonInfo GetIndex(string buttonText)
        {
            foreach (ButtonInfo[] buttons in Buttons.buttons)
            {
                foreach (ButtonInfo button in buttons)
                {
                    if (button.buttonText == buttonText)
                    {
                        return button;
                    }
                }
            }

            return null;
        }

        internal static void ChangeName(string v)
        {
            throw new NotImplementedException();
        }

        public static void RPCProtection()
        {
            try
            {
                if (hasRemovedThisFrame == false)
                {
                    if (NoOverlapRPCs)
                        hasRemovedThisFrame = true;

                    if (GetIndex("Experimental RPC Protection").enabled)
                    {
                        PhotonNetwork.RaiseEvent(0, null, new RaiseEventOptions
                        {
                            CachingOption = EventCaching.DoNotCache,
                            TargetActors = new int[]
                            {
                                PhotonNetwork.LocalPlayer.ActorNumber
                            }
                        }, SendOptions.SendReliable);
                    }
                    else
                    {
                        GorillaNot.instance.rpcErrorMax = int.MaxValue;
                        GorillaNot.instance.rpcCallLimit = int.MaxValue;
                        GorillaNot.instance.logErrorMax = int.MaxValue;

                        PhotonNetwork.MaxResendsBeforeDisconnect = int.MaxValue;
                        PhotonNetwork.QuickResends = int.MaxValue;

                        //PhotonNetwork.OpCleanRpcBuffer(GorillaTagger.Instance.myVRRig.GetView);
                        PhotonNetwork.OpCleanActorRpcBuffer(PhotonNetwork.LocalPlayer.ActorNumber);
                        PhotonNetwork.SendAllOutgoingCommands();

                        GorillaNot.instance.OnPlayerLeftRoom(PhotonNetwork.LocalPlayer);
                    }
                }
            }
            catch { UnityEngine.Debug.Log("RPC protection failed, are you in a lobby?"); }
        }

        // Variables
        // Important
        // Objects
        public static GameObject menu;
                    public static GameObject menuBackground;   
                    public static GameObject reference;
                    public static GameObject canvasObject;

                    public static SphereCollider buttonCollider;
                    public static Camera TPC;
                    public static Text fpsObject;

        // Data
            public static int pageNumber = 0;
            public static int buttonsType = 0;
        internal static float nameCycleDelay;
        internal static int nameCycleIndex;
        internal static bool isCopying;
        internal static VRRig whoCopy;
        private static bool hasRemovedThisFrame;
        private static bool NoOverlapRPCs;
    }
}
