using System;

namespace SnakeGame
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            AutofacConfig.Config();
            var connection = AutofacConfig.Resolve<IConnection>();
            using (var game = new SnakeGame(connection))
                game.Run();
        }
    }
}
