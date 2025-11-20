using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuilderApp
{
    public class SocialMediaPostBuilder
    {
        private readonly SocialMediaPost _post = new SocialMediaPost();

        public SocialMediaPostBuilder AddTitle(string title)
        {
            _post.Title = title;
            return this;
        }

        public SocialMediaPostBuilder AddContent(string content)
        {
            _post.Content = content;
            return this;
        }

        public SocialMediaPostBuilder AddAuther(string auther)
        {
            _post.Author = auther;
            return this;
        }

        public SocialMediaPostBuilder SetPostDate(DateTime datePosted)
        {
            // Here can be added some validation logic
            _post.DatePosted = datePosted;
            return this;
        }

        public SocialMediaPostBuilder AddTag(string tag)
        {
            if (_post.Tags == null)
            {
                _post.Tags = new List<string>();
            }
            _post.Tags.Add(tag);    
            return this;
        }


        public SocialMediaPostBuilder AddImage(Uri imageUri)
        {
            _post.ImageUri = imageUri;
            return this;
        }

        public SocialMediaPostBuilder AddLinks(Uri link)
        {
            if (_post.Links == null)
            {
                _post.Links = new List<Uri>();
            }
            _post.Links.Add(link);
            return this;
        }



        // Build method is the last mehtod that will be envoked for contracting a complex object
        public SocialMediaPost Build()
        {
            return _post;
        }
    }
}
