using OpenTK.Graphics.OpenGL4;

namespace TKCore
{
    public class Shader
    {
        public int ID { get; private set; }

        private Shader(int shaderID)
        {
            ID = shaderID;
        }

        public static Shader LoadShader(string path, ShaderType type)
        {
            int id = GL.CreateShader(type);
            GL.ShaderSource(id, File.ReadAllText(path));
            GL.CompileShader(id);

            string infoLog = GL.GetShaderInfoLog(id);
            if (!string.IsNullOrEmpty(infoLog))
            {
                throw new Exception(infoLog);
            }

            return new Shader(id);
        }

        public static int LoadShaderProgram(string vertexShaderPath, string fragmentShaderPath)
        {
            int shaderProgram = GL.CreateProgram();

            Shader vertexShader = LoadShader("C:\\Users\\Brandon\\OneDrive\\Desktop\\Code\\C#\\Outcast\\TKCore\\Resources\\" + vertexShaderPath, ShaderType.VertexShader);
            Shader fragmentShader = LoadShader("C:\\Users\\Brandon\\OneDrive\\Desktop\\Code\\C#\\Outcast\\TKCore\\Resources\\" + fragmentShaderPath, ShaderType.FragmentShader);

            GL.ActiveShaderProgram(shaderProgram, vertexShader.ID);
            GL.ActiveShaderProgram(shaderProgram, fragmentShader.ID);
            GL.LinkProgram(shaderProgram);
            GL.DetachShader(shaderProgram, vertexShader.ID);
            GL.DetachShader(shaderProgram, fragmentShader.ID);
            GL.DeleteShader(vertexShader.ID);
            GL.DeleteShader(fragmentShader.ID);

            string infoLog = GL.GetProgramInfoLog(vertexShader.ID);
            if (!string.IsNullOrEmpty(infoLog))
            {
                throw new Exception(infoLog);
            }

            return 0;
        }
    }
}
