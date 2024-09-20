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
            await RecentPostsAsync();

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

        public async Task RecentPostsAsync()
        {
            var post1 = new Post
            {
                Title = "The game last night",
                Body =
                    "I didnt like the way England play so terrible in tournaments",
                UserId = "1267",
            };
            var post2 = new Post
            {
                Title = "AFTV", Body = "He has to go blud!", UserId = "1075",
            };
            var post3 = new Post
            {
                Title = "Football talk",
                Body = "When will Arsenal win the Champions league??",
                UserId = "2234",
            };
            var post4 = new Post
            {
                Title = "Election results",
                Body = "How many votes did Boris get??", UserId = "1568",
            };
            var post5 = new Post
            {
                Title = "Climate change", Body = "We need to act now!!",
                UserId = "3321",
            };
            var post6 = new Post
            {
                Title = "Coronavirus", Body = "It's a pandemic!!",
                UserId = "4456",
            };
            var post7 = new Post
            {
                Title = "Music trends", Body = "Why is Taylor Swift a thing??",
                UserId = "5567",
            };
            await _postRepository.AddAsync(post1);
            await _postRepository.AddAsync(post2);
            await _postRepository.AddAsync(post3);
            await _postRepository.AddAsync(post4);
            await _postRepository.AddAsync(post5);
            await _postRepository.AddAsync(post6);
            await _postRepository.AddAsync(post7);
            Console.WriteLine("Recent posts:");
            var recentPosts = new[]
                { post1, post2, post3, post4, post5, post6, post7 };
            foreach (var post in recentPosts)
            {
                Console.WriteLine(
                    $"Title: {post.Title}, Body: {post.Body}, UserId: {post.UserId}");
            }
        }
    }
}