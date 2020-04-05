using System;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

namespace Pong
{
    class Program : GameWindow
    {
        int xBall = 0;
        int yBall = 0;
        int ballLength = 20;
        int ballSpeedX = 3;
        int ballSpeedY = 3;

        int yPlayer1 = 0;
        int yPlayer2 = 0;
        int playerSpeed = 3;
        int xPlayer1()
        {
            return -ClientSize.Width / 2 + playersWidth() / 2;
        }
        int xPlayer2()
        {
            return ClientSize.Width / 2 - playersWidth() / 2;
        }
        int playersWidth()
        {
            return ballLength;
        }
        int playersHeight()
        {
            return 3 * ballLength;
        }
        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            xBall += ballSpeedX;
            yBall += ballSpeedY;

            if (xBall + ballLength / 2 > xPlayer2() - playersWidth() / 2
                && yBall - ballLength / 2 < yPlayer2 + playersHeight() / 2
                && yBall + ballLength / 2 > yPlayer2 - playersHeight() / 2)
            {
                ballSpeedX = ballSpeedX * -1;
            } else if (xBall - ballLength / 2 < xPlayer1() + playersWidth() / 2
                && yBall - ballLength / 2 < yPlayer1 + playersHeight() / 2
                && yBall + ballLength / 2 > yPlayer1 - playersHeight() / 2)
            {
                ballSpeedX = ballSpeedX * -1;
            }

            if (xBall > ClientSize.Width / 2 || xBall < -ClientSize.Width / 2)
            {
                xBall = 0;
                yBall = 0;
            }

            if (yBall + ballLength / 2 > ClientSize.Height / 2)
            {
                ballSpeedY = ballSpeedY * -1;
            }
            else if (yBall - ballLength / 2 < -ClientSize.Height / 2)
            {
                ballSpeedY = ballSpeedY * -1;
            }

            if (Keyboard.GetState().IsKeyDown(Key.W))
            {
                yPlayer1 += playerSpeed;
            }
            if (Keyboard.GetState().IsKeyDown(Key.S))
            {
                yPlayer1 -= playerSpeed;
            }
            if (Keyboard.GetState().IsKeyDown(Key.Up))
            {
                yPlayer2 += playerSpeed;
            }
            if (Keyboard.GetState().IsKeyDown(Key.Down))
            {
                yPlayer2 -= playerSpeed;
            }
        }
        protected override void OnRenderFrame(FrameEventArgs e)
        {
            GL.Viewport(0, 0, ClientSize.Width, ClientSize.Height);

            Matrix4 projection = Matrix4.CreateOrthographic(ClientSize.Width, ClientSize.Height, 0.0f, 1.0f);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref projection);
            
            GL.Clear(ClearBufferMask.ColorBufferBit);

            DrawSquare(xBall, yBall, ballLength, ballLength, 255, 255, 255);
            DrawSquare(xPlayer1(), yPlayer1, playersWidth(), playersHeight(), 255, 255, 255);
            DrawSquare(xPlayer2(), yPlayer2, playersWidth(), playersHeight(), 255, 255, 255);

            SwapBuffers();
        }

        void DrawSquare(int x, int y, int width, int height, float r, float g, float b)
        {
            GL.Color3(r, g, b);

            GL.Begin(PrimitiveType.Quads);
            GL.Vertex2(-0.5f * width + x, -0.5f * height + y);
            GL.Vertex2(0.5f * width + x, -0.5f * height + y);
            GL.Vertex2(0.5f * width + x, 0.5f * height + y);
            GL.Vertex2(-0.5f * width + x, 0.5f * height + y);
            GL.End();
        }
        static void Main()
        {
            new Program().Run();
        }
    }
}
