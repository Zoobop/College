using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using EngineAPI;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;
using TKCore;

namespace OutcastAPI
{
    class Launcher : EngineBase
    {
        public Launcher(string title, int screenWidth, int screenHeight) : base(title, screenWidth, screenHeight)
        {
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();
        }

        protected override void OnRender(Timestep ts)
        {
            base.OnRender(ts);

            int shaderProgram = Shader.LoadShaderProgram("base.vert", "base.frag");
            int uniformProj = GL.GetUniformLocation(shaderProgram, "proj");
            int uniformCam = GL.GetUniformLocation(shaderProgram, "cam");

            GL.UseProgram(shaderProgram);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            Matrix4 projMatrix = Matrix4.CreatePerspectiveFieldOfView((float)Math.PI / 3.0f, (float)Viewport!.Size.X / (float)Viewport!.Size.Y, 0.1f, 2000.0f);
            Matrix4 camMatrix = Camera!.GetMatrix();

            GL.UniformMatrix4(uniformProj, false, ref projMatrix);
            GL.UniformMatrix4(uniformCam, false, ref camMatrix);

            //draw here

            Viewport!.SwapBuffers();
            GL.DeleteProgram(shaderProgram);
        }

        protected override void OnUpdate(Timestep ts)
        {
            base.OnUpdate(ts);

            if (Viewport!.KeyboardState.IsKeyDown(Keys.W)) Camera!.AccDir(0.2f, Camera.GetDirection());
            if (Viewport!.KeyboardState.IsKeyDown(Keys.S)) Camera!.AccDir(-0.2f, Camera.GetDirection());
            if (Viewport!.KeyboardState.IsKeyDown(Keys.A)) Camera!.AccDir(0.2f, Vector3.Cross(Camera.GetDirection(), Vector3.UnitY));
            if (Viewport!.KeyboardState.IsKeyDown(Keys.D)) Camera!.AccDir(-0.2f, Vector3.Cross(Camera.GetDirection(), Vector3.UnitY));
            if (Viewport!.KeyboardState.IsKeyDown(Keys.Space)) Camera!.AccDir(0.2f, Vector3.UnitY);
            if (Viewport!.KeyboardState.IsKeyDown(Keys.LeftShift)) Camera!.AccDir(-0.2f, Vector3.UnitY);

            //update here
        }

        protected override void OnTerminate()
        {
            base.OnTerminate();
        }
    };
}
