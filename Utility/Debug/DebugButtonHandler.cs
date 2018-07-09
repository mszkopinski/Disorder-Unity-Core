using System;
using UnityEngine;

namespace UnityCore
{
    public class DebugButtonHandler
    {
        readonly Settings settings;

        int buttonVCount;
        int buttonHCount;

        public DebugButtonHandler(Settings settings)
        {
            this.settings = settings;
        }

        public void Restart()
        {
            buttonVCount = 0;
            buttonHCount = 0;
        }

        public bool Display(string text)
        {
            if (buttonVCount > settings.NumPerColumn)
            {
                buttonHCount++;
                buttonVCount = 0;
            }

            var result = GUI.Button(
                new Rect(
                    settings.HorizontalMargin + buttonHCount * (settings.ButtonWidth + settings.HorizontalSpacing),
                    settings.VerticalMargin + buttonVCount * (settings.ButtonHeight + settings.VerticalSpacing),
                    settings.ButtonWidth, settings.ButtonHeight), text);

            buttonVCount++;

            return result;
        }

        [Serializable]
        public class Settings
        {
            public int NumPerColumn = 6;
            public float VerticalMargin = 50;
            public float VerticalSpacing = 50;
            public float HorizontalSpacing = 50;
            public float HorizontalMargin = 50;
            public float ButtonWidth = 50;
            public float ButtonHeight = 50;
        }
    }
}