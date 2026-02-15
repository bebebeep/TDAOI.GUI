using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using BepInEx;
using GorillaLocomotion;
using GorillaNetworking;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;
using static OVRPlugin;
using TDAOI.Patches;

namespace TDAOI.Gui
{
    [BepInPlugin("com.rat.tdaoigui","TDAOI GUI", Version)]
    public class TDAOIGui : BaseUnityPlugin
    {
        public const string Version = "1.0.0";



        private static bool AFK1;
        private bool master;
        Font PAGEsansFont;
        Font BUTTONsansFont;



        public string GUIName = "";
        private Color guiColor = Color.red;
        private float colorTimer = 0f;
        private Rect windowRect = new Rect(60, 20, 650, 520); //Size of GUI
        public string GUIText = "TDAOI";
        void Update()
        {
                    float speedMultiplier = 2.0f;
                    float r = Mathf.Lerp(0.0f, 1.0f, Mathf.Abs(Mathf.Sin(colorTimer * 0.4f * speedMultiplier)));
                    float g = Mathf.Lerp(0.0f, 1.0f, Mathf.Abs(Mathf.Sin(colorTimer * 0.4f * speedMultiplier)));
                    float b = Mathf.Lerp(1.0f, 1.0f, Mathf.Abs(Mathf.Sin(colorTimer * 0.4f * speedMultiplier)));
                    guiColor = new Color(r, g, b);
                    colorTimer += Time.deltaTime;
        }
       
        public void Start()
        {
            PAGEsansFont = Font.CreateDynamicFontFromOSFont("Comic Sans MS", 17);
            BUTTONsansFont = Font.CreateDynamicFontFromOSFont("Comic Sans MS", 12);

            ColorUtility.TryParseHtmlString("#fcf2f4", out color1);//uses hex codes
            ColorUtility.TryParseHtmlString("#14199c", out color2);
        }
        public bool youturnnedmeon = false;

        void OnGUI()//Dont really mess with dis
        {
            GUI.backgroundColor = guiColor;
            GUI.color = guiColor;
            if (GUI.Button(new Rect(20, 20, 100, 20), GUIText))
            {
                if (youturnnedmeon == false)
                {
                    GUIText = "TDAOI";
                    youturnnedmeon = true;
                }
                else
                {
                    GUIText = "TDAOI";
                    youturnnedmeon = false;
                }
            }
            if (youturnnedmeon)
            {
                windowRect = GUI.Window(10000, windowRect, MainGUI, GUIName);//opens GUI
            }
        }
        public int PageNum;
        public Color color1;
        public Color color2;
        public string Codetojoin = "";
        void MainGUI(int windowID)
        {
            GUIStyle buttonStyle = new GUIStyle(GUI.skin.button);
            GUIStyle customStyle = new GUIStyle(GUI.skin.label);
            customStyle.normal.textColor = guiColor;
            buttonStyle.font = BUTTONsansFont;
            GUI.contentColor = color1;//sets text color
            GUI.backgroundColor = color2;//sets button color
            //GUI.Color <- that sets every color
            int PageNumlol = PageNum + 1;//the real page number
            GUI.Label(new Rect(205, -8, 200, 29), "TDAOI GUI", customStyle);//GUI TITLE
            //
            //
            //
            //
            GUI.Label(new Rect(230, 20, 200, 29), "Page: " + PageNumlol, GUI.skin.label);
            GUI.skin.label.font = PAGEsansFont;

            int col1X = 20;
            int col2X = 220;
            int col3X = 420;
            int startY = 50;
            int buttonHeight = 40;
            int spacingY = 50;

            switch (PageNum)
            {
                case 0://Page 1//



                    //Collum 1//
                    Codetojoin = GUI.TextArea(new Rect(col1X, startY, 180, 40), Codetojoin);
                    if (GUI.Button(new Rect(col1X, startY + spacingY, 180, 40), "Join DARKRAT", buttonStyle))
                    {
                        JoinStaticRoom("DARKRAT");
                    }

                    if (GUI.Button(new Rect(col1X, startY + spacingY * 2, 180, 40), "Join Random Rat", buttonStyle))
                    {
                        RandomRAT();
                    }






                    //Collum 2//
                    if (GUI.Button(new Rect(col2X, startY, 180, 40), "Join Random Public", buttonStyle))
                    {
                        PhotonNetworkController.Instance.AttemptToJoinPublicRoom(GorillaNetworkJoinTriggerPatch.LastGorillaNetworkJoinTrigger ?? UnityEngine.Object.FindFirstObjectByType<GorillaNetworkJoinTrigger>(), JoinType.Solo, null, false);
                    }
                    AFK1 = GUI.Toggle(new Rect(col2X, startY + spacingY, 180, 40), AFK1, "Singleplayer", buttonStyle);
                    if (AFK1)
                    {
                        PhotonNetwork.Disconnect();
                    }
                    if (GUI.Button(new Rect(col2X, startY + spacingY * 2, 180, 40), "Rat socials", buttonStyle))
                    {
                        OpenURL("https://guns.lol/darkrat1");
                    }




                    //Collum 3//
                    if (GUI.Button(new Rect(col3X, startY, 180, 40), "Mic: Open", buttonStyle))
                    {
                        UpdateMic("open");
                    }

                    if (GUI.Button(new Rect(col3X, startY + spacingY, 180, 40), "Mic: Push to talk", buttonStyle))
                    {
                        UpdateMic("pushtotalk");
                    }

                    if (GUI.Button(new Rect(col3X, startY + spacingY * 2, 180, 40), "Mic: Push to mute", buttonStyle))
                    {
                        UpdateMic("pushtomute");
                    }



                    if (GUI.Button(new Rect(0, 470, 650, 40), "NEXT >>>"))
                    {
                        PageNum++;
                    }

                    break;
                case 1://Page 2//






                    //Collum 1//
                    if (GUI.Button(new Rect(col1X, startY, 180, 40), "Time: Morning", buttonStyle))
                    {
                        UpdateTime("morning");
                    }
                    if (GUI.Button(new Rect(col1X, startY + spacingY, 180, 40), "Time: Noon", buttonStyle))
                    {
                        UpdateTime("noon");
                    }
                    if (GUI.Button(new Rect(col1X, startY + spacingY * 2, 180, 40), "Time: Night", buttonStyle))
                    {
                        UpdateTime("night");
                    }






                    //Collum 2//
                    if (GUI.Button(new Rect(col2X, startY, 180, 40), "Player 1", buttonStyle))
                    {

                    }

                    if (GUI.Button(new Rect(col2X, startY + spacingY, 180, 40), "Player 2", buttonStyle))
                    {

                    }

                    if (GUI.Button(new Rect(col2X, startY + spacingY * 2, 180, 40), "Player 3", buttonStyle))
                    {

                    }




                    //Collum 3//
                    if (GUI.Button(new Rect(col3X, startY, 180, 40), "Player 4", buttonStyle))
                    {

                    }

                    if (GUI.Button(new Rect(col3X, startY + spacingY, 180, 40), "Player 5", buttonStyle))
                    {

                    }

                    if (GUI.Button(new Rect(col3X, startY + spacingY * 2, 180, 40), "Player 6", buttonStyle))
                    {

                    }




                    if (GUI.Button(new Rect(0, 470, 320, 40), "<<< BACK"))
                    {
                        PageNum--;
                    }

                    if (GUI.Button(new Rect(330, 470, 320, 40), "NEXT >>>"))
                    {
                        PageNum++;
                    }

                    break;
                case 2://Page 3//





                    //Collum 1//
                    if (GUI.Button(new Rect(col1X, startY, 180, 40), "Player 7", buttonStyle))
                    {

                    }
                    if (GUI.Button(new Rect(col1X, startY + spacingY, 180, 40), "Player 8", buttonStyle))
                    {

                    }
                    if (GUI.Button(new Rect(col1X, startY + spacingY * 2, 180, 40), "Player 9", buttonStyle))
                    {

                    }






                    //Collum 2//
                    if (GUI.Button(new Rect(col2X, startY, 180, 40), "Player 10", buttonStyle))
                    {

                    }

                    if (GUI.Button(new Rect(col2X, startY + spacingY, 180, 40), "Player 11", buttonStyle))
                    {

                    }

                    if (GUI.Button(new Rect(col2X, startY + spacingY * 2, 180, 40), "Player 12", buttonStyle))
                    {

                    }




                    //Collum 3//
                    if (GUI.Button(new Rect(col3X, startY, 180, 40), "Player 13", buttonStyle))
                    {

                    }

                    if (GUI.Button(new Rect(col3X, startY + spacingY, 180, 40), "Player 14", buttonStyle))
                    {

                    }

                    if (GUI.Button(new Rect(col3X, startY + spacingY * 2, 180, 40), "Player 15", buttonStyle))
                    {

                    }




                    if (GUI.Button(new Rect(0, 470, 320, 40), "<<< BACK"))
                    {
                        PageNum--;
                    }

                    if (GUI.Button(new Rect(330, 470, 320, 40), "NEXT >>>"))
                    {
                        PageNum++;
                    }


                    break;

                case 3://Page 4//


                    //Collum 1//
                    if (GUI.Button(new Rect(col1X, startY, 180, 40), "Player 16", buttonStyle))
                    {

                    }
                    if (GUI.Button(new Rect(col1X, startY + spacingY, 180, 40), "Player 17", buttonStyle))
                    {

                    }
                    if (GUI.Button(new Rect(col1X, startY + spacingY * 2, 180, 40), "Player 18", buttonStyle))
                    {

                    }






                    //Collum 2//
                    if (GUI.Button(new Rect(col2X, startY, 180, 40), "Player 19", buttonStyle))
                    {

                    }

                    if (GUI.Button(new Rect(col2X, startY + spacingY, 180, 40), "Player 20", buttonStyle))
                    {

                    }

                    if (GUI.Button(new Rect(col2X, startY + spacingY * 2, 180, 40), "EMPTY", buttonStyle))
                    {

                    }




                    //Collum 3//
                    if (GUI.Button(new Rect(col3X, startY, 180, 40), "EMPTY", buttonStyle))
                    {

                    }

                    if (GUI.Button(new Rect(col3X, startY + spacingY, 180, 40), "EMPTY", buttonStyle))
                    {

                    }

                    if (GUI.Button(new Rect(col3X, startY + spacingY * 2, 180, 40), "EMPTY", buttonStyle))
                    {

                    }





                    if (GUI.Button(new Rect(0, 470, 650, 40), "<<< BACK"))
                    {
                        PageNum--;
                    }


                    break;

            }
            GUI.DragWindow();
        }
        #region modstuff


        public static void UpdateTime(string time)
        {
            if(time == "morning")
            {
                BetterDayNightManager.instance.SetTimeOfDay(1);
            }
            if (time == "noon")
            {
                BetterDayNightManager.instance.SetTimeOfDay(3);
            }
            if (time == "evening")
            {
                BetterDayNightManager.instance.SetTimeOfDay(7);
            }
            if (time == "night")
            {
                BetterDayNightManager.instance.SetTimeOfDay(0);
            }
        }
        public static void OpenURL(string url)
        {
            Application.OpenURL(url);
        }

        public static void UpdateMic(string MicType)
        {
            if (MicType == "open")
            {
                micState = MicState.OpenMic;
                UpdateMicState();
            }
            if (MicType == "pushtotalk")
            {
                micState = MicState.PushToTalk;
                UpdateMicState();
            }
            if (MicType == "pushtomute")
            {
                micState = MicState.PushToMute;
                UpdateMicState();
            }
        }
        private static void UpdateMicState()
        {
            string text;
            switch (TDAOIGui.micState)
            {
                case TDAOIGui.MicState.PushToTalk:
                    text = "PUSH TO TALK";
                    break;
                case TDAOIGui.MicState.OpenMic:
                    text = "OPEN MIC";
                    break;
                case TDAOIGui.MicState.PushToMute:
                    text = "PUSH TO MUTE";
                    break;
                default:
                    text = "OPEN MIC";
                    break;
            }
            string text2 = text;
            GorillaComputer.instance.pttType = text2;
            PlayerPrefs.SetString("pttType", text2);
            PlayerPrefs.Save();
        }
        private static TDAOIGui.MicState micState;
        private enum MicState
        {
            PushToTalk,
            OpenMic,
            PushToMute
        }

        public static void RandomRAT()
        {
            string text = "RAT" + UnityEngine.Random.Range(123, 987).ToString();
            PhotonNetworkController.Instance.AttemptToAutoJoinSpecificRoom(text, JoinType.Solo);
            UnityEngine.Debug.Log("Joining: " + text);
        }

        // Token: 0x0600001E RID: 30 RVA: 0x000033E8 File Offset: 0x000015E8
        private string GenerateRandomCode(string prefix, int minDigits, int maxDigits)
        {
            int num = UnityEngine.Random.Range(minDigits, maxDigits + 1);
            int minInclusive = (int)Mathf.Pow(10f, (float)(num - 1));
            int maxExclusive = (int)Mathf.Pow(10f, (float)num) - 1;
            return prefix + UnityEngine.Random.Range(minInclusive, maxExclusive).ToString();
        }
        public static void JoinStaticRoom(string Code)
        {
            PhotonNetworkController.Instance.AttemptToAutoJoinSpecificRoom(Code, JoinType.Solo);
        }
        public static void JoinSpecificRoom(string TextBoxContent)
        {
            JoinStaticRoom(TextBoxContent);
        }
        #endregion
    }
}
