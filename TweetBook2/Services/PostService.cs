using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TweetBook2.Data;
using TweetBook2.Domain;

namespace TweetBook2.Services
{
    public class PostService : IPostService
    {
        private readonly DataContext _dataContext;

        public PostService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<bool> DeletePostAsync(Guid postId)
        {
            var lookpostid = await GetPostByIdAsync(postId);
            if (lookpostid == null) return false;
            _dataContext.Posts.Remove(lookpostid);
            var delete = await _dataContext.SaveChangesAsync();
            return delete > 0;
        }

        public async Task<bool> CreatePostAsync(Post post)
        {
            await _dataContext.Posts.AddAsync(post);
            var create = await _dataContext.SaveChangesAsync();
            return create > 0;
        }

        public async Task<Post> GetPostByIdAsync(Guid Id)
        {
            var postId = await _dataContext.Posts.SingleOrDefaultAsync(x => x.ID == Id);
            return postId;
        }

        public async Task<List<Post>> GetPostsAsync()
        {
            return await _dataContext.Posts.ToListAsync();
        }

        public async Task<bool> UpdatePostAsync(Post postToUpdate)
        {
            _dataContext.Posts.Update(postToUpdate);
            var update = await _dataContext.SaveChangesAsync();
            return update > 0;
        }
    }
}
