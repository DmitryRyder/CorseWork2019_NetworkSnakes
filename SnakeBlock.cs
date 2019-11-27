using Common.Enums;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace SnakeGame
{
    public class SnakeBlock
    {
        public Direction Direction { get; set; } = Direction.Right;
        public Direction currentTurnAwait { get; set; } = Direction.Empty;
        public Point currentTurn { get; set; }
        public Queue<Direction> directions { get; set; }
        public Queue<Point> Turns { get; set; }
        public Point Position { get; set; }
        public Point Size { get; set; }
        public Color Color { get; set; }
        public SnakeBlock Next { get; set; }
        public SnakeBlock Previous { get; set; }
        GameWindow window;
        int speed = 2;
        public SnakeBlock(Color color, Point size, Point position, GameWindow window)
        {
            this.window = window;
            directions = new Queue<Direction>();
            Turns = new Queue<Point>();
            Position = position;
            Size = size;
            Color = color;
        }

        public Rectangle ToRectangle()
        {
            return new Rectangle(Position, Size);
        }

        public void ToTurn(Point toTurn, Direction direction)
        {
            Turns.Enqueue(toTurn);
            directions.Enqueue(direction);
        }

        public void Moving()
        {
           if(directions?.Count != 0 && currentTurnAwait == Direction.Empty)
           {
                currentTurnAwait = directions.Dequeue();
                currentTurn = Turns.Dequeue();
           }

           if(currentTurnAwait != Direction.Empty)
           {
                if(currentTurn.X == Position.X && currentTurn.Y == Position.Y)
                {
                    Direction = currentTurnAwait;
                    currentTurnAwait = Direction.Empty;
                }
           }

            switch (Direction)
            {
                case Direction.Right:
                    {
                        if (Position.X >= window.ClientBounds.Width - Size.X)
                            Position = new Point(Position.X - window.ClientBounds.Width, Position.Y);
                        Position = new Point(Position.X + speed, Position.Y);
                    }
                   break;
               case Direction.Left:
                    {
                        if (Position.X <= 0)
                            Position = new Point(window.ClientBounds.Width, Position.Y);
                        Position = new Point(Position.X - speed, Position.Y);
                    }
                   break;
               case Direction.Top:
                    {
                        if (Position.Y <= 0)
                            Position = new Point(Position.X, window.ClientBounds.Height);
                        Position = new Point(Position.X, Position.Y - speed);
                    }
                   break;
               case Direction.Bottom:
                    {
                        if (Position.Y >= window.ClientBounds.Height - Size.Y)
                            Position = new Point(Position.X, Position.Y - window.ClientBounds.Height);
                        Position = new Point(Position.X, Position.Y + speed);
                    };
                   break;
           }
        }
    }
}
