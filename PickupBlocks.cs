using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using UnityEngine;

namespace PickupBlocks
{
    public class ModApi : IModApi
    {
        public void InitMod(Mod _modInstance)
        {
            var harmony = new Harmony("scan_pickupblocks");
            harmony.PatchAll();
            LandClaimBypass.PatchAll(harmony);

            Debug.Log("[PickupBlocks] Loaded.");
        }
    }

    public static class LandClaimBypass
    {
        static readonly MethodInfo Target = AccessTools.Method(
            typeof(WorldBase), "IsMyLandProtectedBlock",
            new[] { typeof(Vector3i), typeof(PersistentPlayerData) });

        static readonly MethodInfo Replacement =
            AccessTools.Method(typeof(LandClaimBypass), nameof(AlwaysTrue));

        static readonly string[] MethodNames = { "GetBlockActivationCommands", "GetActivationText" };

        public static bool AlwaysTrue(WorldBase world, Vector3i pos, PersistentPlayerData ppd) => true;

        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            foreach (CodeInstruction ci in instructions)
            {
                if (ci.Calls(Target))
                    yield return new CodeInstruction(OpCodes.Call, Replacement);
                else
                    yield return ci;
            }
        }

        public static void PatchAll(Harmony harmony)
        {
            var transpiler = new HarmonyMethod(typeof(LandClaimBypass), nameof(Transpiler));

            MethodInfo pickup = AccessTools.Method(typeof(TEFeaturePickup), "AllowBlockActivationCommand");
            if (pickup != null)
            {
                harmony.Patch(pickup, transpiler: transpiler);
            }

            const BindingFlags flags = BindingFlags.Instance | BindingFlags.Public |
                                       BindingFlags.NonPublic | BindingFlags.DeclaredOnly;

            foreach (Type type in typeof(Block).Assembly.GetTypes())
            {
                if (!typeof(Block).IsAssignableFrom(type)) continue;

                foreach (MethodInfo m in type.GetMethods(flags))
                {
                    if (!MethodNames.Contains(m.Name) || m.IsAbstract) continue;
                    try
                    {
                        if (!PatchProcessor.GetOriginalInstructions(m).Any(ci => ci.Calls(Target)))
                            continue;

                        harmony.Patch(m, transpiler: transpiler);
                    }
                    catch (Exception)
                    {
                    }
                }
            }
        }

        [HarmonyPatch(typeof(BlockPowerSource), "OnBlockActivated",
    new[] { typeof(string), typeof(WorldBase), typeof(Vector3i), typeof(BlockValue), typeof(EntityPlayerLocal) })]
        public static class Patch_BlockPowerSource_TakeDelay
        {
            static readonly FieldInfo TakeDelayField = AccessTools.Field(typeof(BlockPowerSource), "TakeDelay");

            public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
            {
                foreach (CodeInstruction ci in instructions)
                {
                    if (ci.opcode == OpCodes.Ldc_R4 && ci.operand is float value && value == 4f)
                    {
                        ci.opcode = OpCodes.Ldarg_0;
                        ci.operand = null;
                        yield return ci;
                        yield return new CodeInstruction(OpCodes.Ldfld, TakeDelayField);
                    }
                    else
                    {
                        yield return ci;
                    }
                }
            }
        }
    }
}