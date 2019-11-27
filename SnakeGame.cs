using Common.Enums;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace SnakeGame
{
    public class SnakeGame : Game
    {
        private readonly IConnection _connection;
        Color backGround = new Color(108, 113, 128); // default color gray 
        GraphicsDeviceManager graphics;
        Snake snake;
        Snake enemySnake;
        Direction currentDirection = Direction.Right;

        public SnakeGame(IConnection connection)
        {
            graphics = new GraphicsDeviceManager(this);
            _connection = connection;
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            snake = new Snake(GraphicsDevice, Window, 10, 120, 5);
            enemySnake = new Snake(GraphicsDevice, Window, 10, 20, 250);
            _connection.Connect("127.0.0.1", 8888, "client1");
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            KeyboardState keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.Escape))
                Exit();

            snake.Moving();
            enemySnake.Moving();
            base.Update(gameTime);

            if (keyboardState.IsKeyDown(Keys.Left))
            {
                if (currentDirection == Direction.Right)
                    return;
                currentDirection = Direction.Left;
                _connection.SendData(Direction.Left);
                Direction direction = _connection.RecieveData();
                enemySnake.Turn(direction);
                snake.Turn(Direction.Left);
                snake.Moving();
                return;
            }
            if (keyboardState.IsKeyDown(Keys.Right))
            {
                if (currentDirection == Direction.Left)
                    return;
                currentDirection = Direction.Right;
                _connection.SendData(Direction.Right);
                Direction direction = _connection.RecieveData();
                enemySnake.Turn(direction);
                snake.Turn(Direction.Right);
                snake.Moving();
                return;
            }
            if (keyboardState.IsKeyDown(Keys.Up))
            {
                if (currentDirection == Direction.Bottom)
                    return;
                currentDirection = Direction.Top;
                _connection.SendData(Direction.Top);
                Direction direction = _connection.RecieveData();
                enemySnake.Turn(direction);
                snake.Turn(Direction.Top);
                snake.Moving();
                return;
            }
            if (keyboardState.IsKeyDown(Keys.Down))
            {
                if (currentDirection == Direction.Top)
                    return;
                currentDirection = Direction.Bottom;
                _connection.SendData(Direction.Bottom);
                Direction direction = _connection.RecieveData();
                enemySnake.Turn(direction);
                snake.Turn(Direction.Bottom);
                snake.Moving();
                return;
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(backGround);
            snake.Draw();
            enemySnake.Draw();
            base.Draw(gameTime);
        }
    }
}
