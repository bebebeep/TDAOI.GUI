using System;
using GorillaNetworking;
using HarmonyLib;

namespace TDAOI.Patches
{
    [HarmonyPatch(typeof(GorillaNetworkJoinTrigger))]
    [HarmonyPatch("OnBoxTriggered")]
    public static class GorillaNetworkJoinTriggerPatch
    {
        private static void Postfix(GorillaNetworkJoinTrigger __instance)
        {
            GorillaNetworkJoinTriggerPatch.LastGorillaNetworkJoinTrigger = __instance;
        }
        public static GorillaNetworkJoinTrigger LastGorillaNetworkJoinTrigger;
    }
}
