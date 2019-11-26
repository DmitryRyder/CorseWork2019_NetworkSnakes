using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;
using System.Net.Sockets;
using System.Text;

namespace SnakeGame
{
    public class Snake
    {
        SpriteBatch spriteBatch;
        Texture2D rectangleBlock;
        GraphicsDevice graphicsDevice;
        SnakeBlock head;
        GameWindow window;
        int count;
        int length;
        int startPositionX;
        int startPositionY;

        public Direction Direction { get; set; }
        public int Count { get { return count; } }

        public Snake(GraphicsDevice graphicsDevice, GameWindow window, int length, int startPositionX, int startPositionY)
        {
            this.window = window;
            this.graphicsDevice = graphicsDevice;
            rectangleBlock = new Texture2D(this.graphicsDevice, 1, 1);
            rectangleBlock.SetData(new[] { Color.White });
            spriteBatch = new SpriteBatch(this.graphicsDevice);
            this.length = length;
            this.startPositionX = startPositionX;
            this.startPositionY = startPositionY;
            BeginInitialise();
        }

        public void Add(SnakeBlock snakeBlock)
        {
            SnakeBlock node = snakeBlock;
            if (head == null)
            {
                head = node;
                head.Next = node;
                head.Previous = node;
            }
            else
            {
                node.Previous = head.Previous;
                node.Next = head;
                head.Previous.Next = node;
                head.Previous = node;
            }
            count++;
        }

        public void Clear()
        {
            head = null;
            count = 0;
            BeginInitialise();
        }

        public void BeginInitialise()
        {
            var shiftForX = 5;
            for (var i = count; i < length; i++)
            {
                Add(new SnakeBlock(new Color(55, 255, 255), new Point(20, 20), new Point(startPositionX - shiftForX, startPositionY), window));
                shiftForX += 20;
            }
            head.Color = Color.Red;
        }

        public void Draw()
        {
            spriteBatch.Begin();
            SnakeBlock tempNode = head;
            for (var i = 0; i <= count; i++)
            {
                spriteBatch.Draw(rectangleBlock, tempNode.ToRectangle(), tempNode.Color);
                tempNode = tempNode.Previous;
            }
            spriteBatch.End();
        }
       
        public void Turn(Direction direction)
        {
            SnakeBlock tempNode = head.Previous;
            Point fixState = new Point(head.Position.X, head.Position.Y);
            head.ToTurn(fixState, direction);
            for (var i = 0; i < count - 1; i++)
            {
                tempNode.ToTurn(fixState, direction);
                tempNode = tempNode.Previous;
            }
        }

        public void Moving()
        {
            SnakeBlock tempNode = head;
            for (var i = 0; i < count; i++)
            {
                tempNode.Moving();
                tempNode = tempNode.Previous;
            }
        }
    }
}
