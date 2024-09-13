using System;
using System.Linq;
using System.Threading.Tasks;
using Entities;
using EntityRepository;

namespace CLI.UI
{
    public class CliApp
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IUserRepo _userRepository;
        private readonly IPostRepo _postRepository;

        public CliApp(ICommentRepository commentRepository,
            IUserRepo userRepository, IPostRepo postRepository)
        {
            _commentRepository = commentRepository ??
                                 throw new ArgumentNullException(
                                     nameof(commentRepository));
            _userRepository = userRepository ??
                              throw new ArgumentNullException(
                                  nameof(userRepository));
            _postRepository = postRepository ??
                              throw new ArgumentNullException(
                                  nameof(postRepository));
        }

        public async Task RunAsync()
        {
            while (true)
            {
                Console.WriteLine("Choose an option:");
                Console.WriteLine("1. Create new user");
                Console.WriteLine("2. Create new post");
                Console.WriteLine("3. Add comment to existing post");
                Console.WriteLine("4. View posts overview");
                Console.WriteLine("5. View specific post");
                Console.WriteLine("0. Exit");

                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        await CreateUserAsync();
                        break;
                    case "2":
                        await CreatePostAsync();
                        break;
                    case "3":
                        await AddCommentAsync();
                        break;
                    case "4":
                        await ViewPostsOverviewAsync();
                        break;
                    case "5":
                        await ViewSpecificPostAsync();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        private async Task CreateUserAsync()
        {
            Console.Write("Enter username: ");
            var username = Console.ReadLine();
            Console.Write("Enter password: ");
            var password = Console.ReadLine();

            var user = new User { UserName = username, Password = password };
            await _userRepository.AddAsync(user);

            Console.WriteLine($"User {username} created successfully.");
        }

        private async Task CreatePostAsync()
        {
            Console.Write("Enter title: ");
            var title = Console.ReadLine();
            Console.Write("Enter body: ");
            var body = Console.ReadLine();
            Console.Write("Enter user ID: ");
            var userId = (Console.ReadLine());

            var post = new Post { Title = title, Body = body, UserId = userId };
            if (post.UserId == null)
                throw new ArgumentException("User ID cannot be null.");

            await _postRepository.AddAsync(post);

            Console.WriteLine($"Post '{title}' created successfully.");
        }


        private async Task AddCommentAsync()
        {
            Console.Write("Enter post ID: ");
            var postId = int.Parse(Console.ReadLine());
            Console.Write("Enter user ID: ");
            // var userId = (Console.ReadLine());
            var userId = int.Parse(Console.ReadLine());
            Console.Write("Enter comment body: ");
            var body = Console.ReadLine();

            var comment = new Comment
                { PostId = postId, UserId = userId, Body = body };
            await _commentRepository.AddAsync(comment);

            Console.WriteLine("Comment added successfully.");
        }

        private async Task ViewPostsOverviewAsync()
        {
            var posts = _postRepository.GetAll().ToList();
            foreach (var post in posts)
            {
                Console.WriteLine($"ID: {post.Id}, Title: {post.Title}");
            }
        }

        private async Task ViewSpecificPostAsync()
        {
            Console.Write("Enter post ID: ");
            var postId = int.Parse(Console.ReadLine());

            var post = await _postRepository.GetSingleAsync(postId);
            Console.WriteLine($"Title: {post.Title}");
            Console.WriteLine($"Body: {post.Body}");

            var comments = _commentRepository.GetMany()
                .Where(c => c.PostId == postId).ToList();
            foreach (var comment in comments)
            {
                Console.WriteLine(
                    $"Comment by User {comment.UserId}: {comment.Body}");
            }
        }
    }
}