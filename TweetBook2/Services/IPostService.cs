using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TweetBook2.Domain;

namespace TweetBook2.Services
{
    public interface IPostService
    {
        Task<List<Post>> GetPostsAsync();
        Task<Post> GetPostByIdAsync(Guid Id);
        Task<bool> CreatePostAsync(Post post);
        Task<bool> UpdatePostAsync(Post postToUpdate);
        Task<bool> DeletePostAsync(Guid postId);
    }
}
