﻿using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using TKCore;

namespace OutcastAPI
{
	public static class Program
	{
		private static GameWindow? _window;
		private static Camera? _camera;


		private static void Init()
		{
			Debug.WriteLine("Hello World!");

			CultureInfo.CurrentCulture = new CultureInfo("en-US");

			GameWindowSettings gws = GameWindowSettings.Default;
			NativeWindowSettings nws = NativeWindowSettings.Default;
			gws.IsMultiThreaded = false;
			gws.RenderFrequency = 60;
			gws.UpdateFrequency = 60;

			nws.APIVersion = Version.Parse("4.1.0");
			nws.Size = new Vector2i(1280, 720);
			nws.Title = "NoNumberGame";

			_window = new GameWindow(gws, nws);
			_camera = new Camera();

			_window.Load += OnWindowLoad;
			_window.KeyDown += OnWindowKeyDown;
			_window.Resize += OnWindowResize;
			_window.MouseMove += OnWindowMouseMove;
			_window.Closing += OnWindowClose;
			_window.RenderFrame += Render;
			_window.UpdateFrame += Update;
		}

		private static void Render(FrameEventArgs args)
		{
			int shaderProgram = Shader.LoadShaderProgram("base.vert", "base.frag");
			int uniformProj = GL.GetUniformLocation(shaderProgram, "proj");
			int uniformCam = GL.GetUniformLocation(shaderProgram, "cam");

			GL.UseProgram(shaderProgram);
			GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

			Matrix4 projMatrix = Matrix4.CreatePerspectiveFieldOfView((float)Math.PI / 3.0f, (float)_window!.Size.X / (float)_window!.Size.Y, 0.1f, 2000.0f);
			Matrix4 camMatrix = _camera!.GetMatrix();

			GL.UniformMatrix4(uniformProj, false, ref projMatrix);
			GL.UniformMatrix4(uniformCam, false, ref camMatrix);

			//draw here

			_window!.SwapBuffers();
			GL.DeleteProgram(shaderProgram);
		}

		private static void Update(FrameEventArgs args)
		{


		}

		private static void Terminate()
		{
			_window!.Dispose();
		}

		private static void OnWindowLoad()
		{
			GL.Enable(EnableCap.DepthTest);
			GL.ClearColor(0.2f, 0.5f, 0.8f, 0);

			//load assets here
		}

		private static void OnWindowKeyDown(KeyboardKeyEventArgs args)
		{

		}

		private static void OnWindowResize(ResizeEventArgs args)
		{
			GL.Viewport(0, 0, args.Width, args.Height);
		}

		private static void OnWindowMouseMove(MouseMoveEventArgs args)
		{
			if (_window!.IsMouseButtonDown(MouseButton.Middle))
			{
				_camera!.AccAngle(args.DeltaY / 100.0f, args.DeltaX / 100.0f, 0.0f);
			}
		}

		private static void OnWindowClose(CancelEventArgs args)
		{
			//delete assets here
		}



		public static void Main(string[] args)
		{
			Console.WriteLine((char)21);
		}
	}
}