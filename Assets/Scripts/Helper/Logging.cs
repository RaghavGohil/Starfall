using System;
using UnityEngine;

namespace Helper 
{
    public class Logging
    {

        static bool canLog = true;

        /// <summary>
        /// Generates debug logs for the system.
        /// </summary>
        /// <param name="message">A message which you want to log.</param>
        /// <param name="level">
        /// Hints the type of log:
        /// 0: info
        /// 1: warning
        /// 2: error
        /// </param>
        /// <param name="on">Do you want to log this?</param>
        public static void Log(string message,byte level=0,bool on=true)
        {
            if (!canLog || !on) return;

            switch (level) 
            {
                case 0:
                    Debug.Log(message);
                    break;
                case 1:
                    Debug.LogWarning(message);
                    break;
                case 2:
                    Debug.LogError(message);
                    break;
                default:
                    Debug.LogError($"State appropriate logging level. Level = {level}.");
                    break;
            }
        }
    }
}

