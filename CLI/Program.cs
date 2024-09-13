using System;
using System.Threading.Tasks;
using CLI.UI;
using Entities;
using EntityRepository;
using Server;

namespace CLI
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Please wait while setting up the application...");
            await Task.Delay(2000);
            var commentRepository = new InMemoryCommentRepository();
            var userRepository = new InMemoryUserRepository();
            var postRepository = new InMemoryPostRepository();

            var app = new CliApp(commentRepository, userRepository, postRepository);
            await app.RunAsync();
        }
    }
}