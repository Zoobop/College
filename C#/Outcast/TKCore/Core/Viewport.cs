using System;
using OpenTK.Windowing.Desktop;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using System.Globalization;

namespace TKCore
{
    public struct ViewportSettings
    {
        public GameWindowSettings gameWindowSettings = GameWindowSettings.Default;
        public NativeWindowSettings nativeWindowSettings = NativeWindowSettings.Default;

        public ViewportSettings(string title = "Game Window", int windowWidth = 1280, int windowHeight = 720, float framerate = 60f, double renderFrequency = 60, double updateFrequency = 60.0)
        {
            gameWindowSettings.IsMultiThreaded = false;
            gameWindowSettings.UpdateFrequency = updateFrequency;
            gameWindowSettings.RenderFrequency = renderFrequency;

            nativeWindowSettings.APIVersion = Version.Parse("4.1.0");
            nativeWindowSettings.Size = new Vector2i(windowWidth, windowHeight);
            nativeWindowSettings.Title = title;
        }
    }

    public class Viewport : GameWindow
    {
        public Viewport(ViewportSettings viewportSettings) : base(viewportSettings.gameWindowSettings, viewportSettings.nativeWindowSettings)
        {
            CultureInfo.CurrentCulture = new CultureInfo("en-US");
        }

        protected override void OnLoad()
        {
            base.OnLoad();
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);
        }

        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            base.OnUpdateFrame(args);
        }
    }
}
