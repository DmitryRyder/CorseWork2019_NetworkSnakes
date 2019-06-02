using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SnakeGame
{
    public class Snake
    {
        Color backGround = new Color(108, 113, 128); // default color gray 
        SpriteBatch spriteBatch;
        Texture2D rectangleBlock;
        GraphicsDevice graphicsDevice;
        SnakeBlock head;
        GameWindow window;
        int count;

        public Direction Direction { get; set; }
        public int Count { get { return count; } }

        public Snake(GraphicsDevice graphicsDevice, GameWindow window)
        {
            this.window = window;
            this.graphicsDevice = graphicsDevice;
            rectangleBlock = new Texture2D(this.graphicsDevice, 1, 1);
            rectangleBlock.SetData(new[] { Color.White });
            spriteBatch = new SpriteBatch(this.graphicsDevice);
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
            var b = 5;
            for (var i = count; i < 30; i++)
            {
                Add(new SnakeBlock(new Color(55, 255, 255), new Point(20, 20), new Point(120 - b, 5), window));
                b += 20;
            }
            head.Color = Color.Red;
        }

        public void Draw()
        {
            graphicsDevice.Clear(backGround);
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
            for (var i = 0; i < count-1; i++)
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
