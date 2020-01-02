using Cosmonaut;
using Cosmonaut.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TweetBook2.Domain;

namespace TweetBook2.Services
{
    public class CosmosPostService : IPostService
    {
        private readonly ICosmosStore<CosmosPostDto> _cosmosStore;

        public CosmosPostService(ICosmosStore<CosmosPostDto> cosmosStore)
        {
            _cosmosStore = cosmosStore;
        }

        public async Task<bool> CreatePostAsync(Post post)
        {
            var createpost = new CosmosPostDto {
                Id = Guid.NewGuid().ToString(),
                Name = post.Name,
            };
            var create = await _cosmosStore.AddAsync(createpost);
            return create.IsSuccess;
        }

        public async Task<bool> DeletePostAsync(Guid postId)
        {
            var delete = await _cosmosStore.RemoveByIdAsync(postId.ToString());
            return delete.IsSuccess;
        }

        public async Task<Post> GetPostByIdAsync(Guid Id)
        {
            var getpostid = await _cosmosStore.FindAsync(Id.ToString());
            return new Post { ID = Guid.Parse(getpostid.Id), Name = getpostid.Name };
        }

        public async Task<List<Post>> GetPostsAsync()
        {
            var postget = await _cosmosStore.Query().ToListAsync();
            return postget.Select(x => new Post { ID = Guid.Parse(x.Id), Name = x.Name }).ToList();
        }

        public async Task<bool> UpdatePostAsync(Post postToUpdate)
        {
            var updatepost = new CosmosPostDto
            {
                Id = postToUpdate.ID.ToString(),
                Name = postToUpdate.Name
            };
            var update = await _cosmosStore.UpdateAsync(updatepost);
            return update.IsSuccess;
        }
    }
}
