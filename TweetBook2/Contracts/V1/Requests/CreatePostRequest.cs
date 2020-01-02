using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TweetBook2.Contracts.V1.Requests
{
    public class CreatePostRequest
    {
        //because id auto ren so we don't need id
        //public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
