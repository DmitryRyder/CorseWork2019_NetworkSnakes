using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace SnakeGame
{
    public class SnakeGame : Game
    {
        GraphicsDeviceManager graphics;
        Snake snake;
        Direction currentDirection = Direction.Right;

        public SnakeGame()
        {
            graphics = new GraphicsDeviceManager(this);
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            snake = new Snake(GraphicsDevice, Window);
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
            base.Update(gameTime);

            if (keyboardState.IsKeyDown(Keys.Left))
            {
                if (currentDirection == Direction.Right)
                    return;
                currentDirection = Direction.Left;
                snake.Turn(Direction.Left);
                snake.Moving();
                return;
            }
            if (keyboardState.IsKeyDown(Keys.Right))
            {
                if (currentDirection == Direction.Left)
                    return;
                currentDirection = Direction.Right;
                snake.Turn(Direction.Right);
                snake.Moving();
                return;
            }
            if (keyboardState.IsKeyDown(Keys.Up))
            {
                if (currentDirection == Direction.Bottom)
                    return;
                currentDirection = Direction.Top;
                snake.Turn(Direction.Top);
                snake.Moving();
                return;
            }
            if (keyboardState.IsKeyDown(Keys.Down))
            {
                if (currentDirection == Direction.Top)
                    return;
                currentDirection = Direction.Bottom;
                snake.Turn(Direction.Bottom);
                snake.Moving();
                return;
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            snake.Draw();
            base.Draw(gameTime);
        }
    }
}
